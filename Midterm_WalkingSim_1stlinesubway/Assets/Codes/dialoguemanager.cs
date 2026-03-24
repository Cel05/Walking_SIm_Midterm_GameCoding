using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI displayName;
    public TextMeshProUGUI lineText;
    public Transform choicesContainer;
    public Button choiceButtonPrefab;

    private NPCData currentNode;
    private int lineIndex;
    private bool isActive;

    private CCPlayer player;

    private void Awake()
    {
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        ClearChoices();
        player = FindFirstObjectByType<CCPlayer>();
    }

    private void OnEnable()
    {
        CCPlayer.OnDialogueRequested += StartDialogue;
    }

    private void OnDisable()
    {
        CCPlayer.OnDialogueRequested -= StartDialogue;
    }

    private void Update()
    {
        if (!isActive) return;

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (ChoicesAreShowing()) return;
            Advance();
        }
    }

    void StartDialogue(NPCData npcData)
    {
        if (npcData == null)
        {
            Debug.Log("NPC Data is Null");
            return;
        }

        if (player != null) player.SetControlsLocked(true);

        currentNode = npcData;
        lineIndex = 0;
        isActive = true;

        if (dialoguePanel != null) dialoguePanel.SetActive(true);
        ShowLine();
    }

    bool HasChoices(NPCData node)
    {
        return node != null && node.choices != null && node.choices.Length > 0;
    }

    void Advance()
    {
        if (currentNode == null)
        {
            EndDialogue();
            return;
        }

        lineIndex++;

        if (currentNode.lines != null && lineIndex < currentNode.lines.Length)
        {
            if (lineText != null)
            {
                lineText.text = currentNode.lines[lineIndex];
                return;
            }
        }

        FinishNode();
    }

    void ShowChoices(DialogueChoice[] choices)
    {
        ClearChoices();

        if (choicesContainer == null || choiceButtonPrefab == null)
        {
            Debug.Log("choices are not wired");
            return;
        }

        foreach (DialogueChoice choice in choices)
        {
            Button bttn = Instantiate(choiceButtonPrefab, choicesContainer);

            bttn.gameObject.SetActive(true);

            TextMeshProUGUI tmp = bttn.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = choice.choiceText;
            }

            NPCData next = choice.nextNode;

            bttn.onClick.RemoveAllListeners();
            bttn.onClick.AddListener(() =>
            {
                Choose(next);
            });
        }
    }

    void FinishNode()
    {
        if (HasChoices(currentNode))
        {
            ShowChoices(currentNode.choices);
            return;
        }

        if (currentNode.nextNode != null)
        {
            currentNode = currentNode.nextNode;
            lineIndex = 0;
            ShowLine();
            return;
        }

        EndDialogue();
    }

    void ShowLine()
    {
        ClearChoices();

        if (currentNode == null)
        {
            EndDialogue();
            return;
        }

        if (displayName != null) displayName.text = currentNode.displayName;

        if (currentNode.lines == null || currentNode.lines.Length == 0)
        {
            FinishNode();
            return;
        }

        lineIndex = Mathf.Clamp(lineIndex, 0, currentNode.lines.Length - 1);

        if (lineText != null) lineText.text = currentNode.lines[lineIndex];
    }

    void Choose(NPCData nextNode)
    {
        ClearChoices();

        if (nextNode == null)
        {
            EndDialogue();
            return;
        }

        currentNode = nextNode;
        lineIndex = 0;
        ShowLine();
    }

    bool ChoicesAreShowing()
    {
        return choicesContainer != null && choicesContainer.childCount > 0;
    }

    void ClearChoices()
    {
        if (choicesContainer == null) return;

        for (int i = choicesContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(choicesContainer.GetChild(i).gameObject);
        }
    }

    void EndDialogue()
    {
        if (player != null) player.SetControlsLocked(false);

        isActive = false;
        currentNode = null;
        lineIndex = 0;

        ClearChoices();

        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }
}
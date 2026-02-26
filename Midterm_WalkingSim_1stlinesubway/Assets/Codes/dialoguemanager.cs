using TMPro;
using UnityEngine;

public class dialoguemanager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI displayName;
    public TextMeshProUGUI placeHolderOpeningLine;

    private void OnEnable()
    {
        
    }
    
    void StartDialogue(NPCData npcData)
    {
        if(npcData == null)
        {
            Debug.Log("NPC Data is null");
            return;
        }

        if (dialoguePanel != null) dialoguePanel.SetActive(true);
        if (displayName != null) displayName.text = npcData.displayName;
        if (placeHolderOpeningLine != null) placeHolderOpeningLine.text = npcData.placeHolderOpeningLine;
        Debug.Log($"DIalogue start with {npcData.displayName}; {npcData.placeHolderOpeningLine}");

    }
}

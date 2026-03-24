using UnityEngine;
using TMPro;

public class EndingTrigger : MonoBehaviour
{
    public GameObject completePanel;
    public string targetTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag))
            return;

        if (completePanel != null)
            completePanel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
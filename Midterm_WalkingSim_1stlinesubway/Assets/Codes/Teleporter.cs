using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    public Transform destination;
    public string targetTag = "Player";
    public float yOffset = 0.5f;
    public bool matchDestinationRotation = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag))
            return;

        if (destination == null)
            return;

        CharacterController controller = other.GetComponent<CharacterController>();

        if (controller != null)
            controller.enabled = false;

        Vector3 newPosition = destination.position;
        newPosition.y += yOffset;

        other.transform.position = newPosition;

        if (matchDestinationRotation)
            other.transform.rotation = destination.rotation;

        if (controller != null)
            controller.enabled = true;
    }
}
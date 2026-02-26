using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private float pickupRange = 3f;

    //when i press F
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryPickup();
        }
    }

    //check if the pickup obj is in the ray then destroys when it's picked up
    void TryPickup()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
using UnityEngine;

public class WASD : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        transform.Translate(movement * speed * Time.deltaTime);
    }
    
    
}

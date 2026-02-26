using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }

        else

        {
            Destroy(gameObject);
        }
    }



    public void OnReLoad(InputAction.CallbackContext context)
    {
        if(context.performed)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("reload");
    }
}

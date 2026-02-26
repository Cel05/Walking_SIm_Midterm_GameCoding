using System;
using UnityEngine;
using TMPro;

public class UIlisten : MonoBehaviour
{
    public TextMeshProUGUI statusText;

    public void OnEnable()
    {
        ButtonEvent.onButtonPressed += UpdateText;
    }

    

    private void OnDisable()
    {
        throw new NotImplementedException();
    }

    void UpdateText()
    {
        statusText.text = "Button pressed";
    }
}

using System;
using TMPro;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    public static event Action onButtonPressed;

    public void OnButtonPressed()
    {
        //?. means only do this if it isnt null (if someone is listening)
        onButtonPressed?.Invoke();
    }
    /*private void PressMe()
    {
        Light Light = GetCOmponent<Light>();
        TextMeshProUGUI statusText = GetComponentInChildren<TextMeshProUGUI>();
        
        Light clolor = Color white;
        statusText.text = "Pressed";
    }*/
}
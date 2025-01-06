using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    public string buttonValue; // Valoarea tastei (ex.: "1", "2")

    private void OnMouseDown()
    {
        // Apelează funcția de adăugare a valorii în KeypadManager
        KeypadManager.Instance.AddToCode(buttonValue);
        Debug.Log("Button pressed: " + buttonValue);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadManager : MonoBehaviour
{
    public static KeypadManager Instance; // Singleton pentru acces global

    private string enteredCode = ""; 
    public string correctCode = "9723"; // Codul corect

    public GameObject door; // Referința către ușă

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddToCode(string value)
    {
        enteredCode += value;
        Debug.Log("Current code: " + enteredCode);

        // Verifică dacă codul introdus este complet
        if (enteredCode.Length >= correctCode.Length)
        {
            CheckCode();
        }
    }

    private void CheckCode()
    {
        if (enteredCode == correctCode)
        {
            Debug.Log("Access Granted!");
            OpenDoor();
        }
        else
        {
            Debug.Log("Access Denied!");
            ResetCode();
        }
    }

    private void OpenDoor()
    {
        // Logica pentru deschiderea ușii
        if (door != null)
        {
            door.SetActive(false); // Exemplu: Dezactivează ușa
        }
        Debug.Log("Door opened!");
    }

    private void ResetCode()
    {
        enteredCode = ""; // Resetează codul introdus
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeypadManager : MonoBehaviour
{
    public string password = "1234";

    private string userInput = "";

    public AudioClip clickSound;
    public AudioClip openSound;
    public AudioClip noSound;

    AudioSource audioSource;

    public UnityEvent OnEntryAllowed;

    private void Start()
    {
        userInput = "";
        audioSource = GetComponent<AudioSource>();
    }

    public void ButtonClicked(string number)
    {
        audioSource.PlayOneShot(clickSound);
        userInput += number;
        //Debug.Log(userInput);
        if (userInput.Length >= 4)
        {
            //CHECK PASSWORD
            if(userInput == password)
            {
                // to do - invoke the event play a sound
                Debug.Log("Entry Allowed");
                audioSource.PlayOneShot(openSound);
                OnEntryAllowed.Invoke();
            }
            else
            {
                Debug.Log("Not this time");
                //to do play sound
                userInput = "";
                audioSource.PlayOneShot(noSound);
            }

        }
    }
}

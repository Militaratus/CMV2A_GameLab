using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public string keyCode = "0000";
    private string enteredCode = "";

    public UnityEvent unlockedEvents;

    // Component References
    Text guiDisplay;

    private void Awake()
    {
        UpdateDisplay(enteredCode);
        transform.parent = null;
    }

    public void AddKey(string text)
    {
        if (enteredCode.Length >= keyCode.Length)
        {
            return;
        }

        enteredCode = enteredCode + text;
        UpdateDisplay(enteredCode);
    }

    public void SubmitKey()
    {
        if (enteredCode != keyCode)
        {
            return;
        }

        UpdateDisplay("UNLOCKED");
        unlockedEvents.Invoke();
    }

    public void ClearKey()
    {
        enteredCode = "";
        UpdateDisplay(enteredCode);
    }

    public void UpdateDisplay(string text)
    {
        if (!guiDisplay)
        {
            guiDisplay = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        }

        guiDisplay.text = text;
    }
}

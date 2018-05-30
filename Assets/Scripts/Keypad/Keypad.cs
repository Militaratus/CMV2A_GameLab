using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public string keyCode = "0000";
    private string enteredCode = "";

    public AudioClip audioPress;
    public AudioClip audioClear;
    public AudioClip audioInvalid;
    public AudioClip audioCorrect;
    private AudioSource audioPlayer;

    public UnityEvent unlockedEvents;

    // Component References
    Text guiDisplay;

    private void Awake()
    {
        UpdateDisplay(enteredCode);
        audioPlayer = GetComponent<AudioSource>();
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
        PlaySound(audioPress);
    }

    public void SubmitKey()
    {
        if (enteredCode != keyCode)
        {
            PlaySound(audioInvalid);
            return;
        }

        UpdateDisplay("UNLOCKED");
        PlaySound(audioCorrect);
        unlockedEvents.Invoke();
    }

    public void ClearKey()
    {
        enteredCode = "";
        UpdateDisplay(enteredCode);
        PlaySound(audioClear);
    }

    public void UpdateDisplay(string text)
    {
        if (!guiDisplay)
        {
            guiDisplay = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        }

        guiDisplay.text = text;
    }

    void PlaySound(AudioClip chosenAudio)
    {
        if (!chosenAudio)
        {
            return;
        }

        audioPlayer.clip = chosenAudio;
        audioPlayer.Play();
    }
}

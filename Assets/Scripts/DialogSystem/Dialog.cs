using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    // Required
    public string text = "";
    public AudioClip voice;
    public bool playerIsTalking = false;

    // New Stuff unlocked during conversation
    public Topic newTopic;
    public Evidence newEvidence;
    public Suspect newSuspect;
}


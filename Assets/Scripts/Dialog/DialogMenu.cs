using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogMenu : ScriptableObject
{
    public string openingDialog;
    public Dialog[] availableDialog;
}

[System.Serializable]
public class Dialog
{
    public bool topicAvailable = true;
    public string topicName = "";
    public string[] topicResponse;
    public DialogChoice goodCop;
    public DialogChoice badCop;
    public DialogAccuse accuseCop;
}

[System.Serializable]
public class DialogChoice
{
    public string[] choiceResponse;
    public Evidence newEvidence;
    public int[] newTopics;
}

[System.Serializable]
public class DialogAccuse
{
    public string[] choiceResponse;
    public Evidence choiceEvidence;
    public Evidence newEvidence;
    public int[] newTopics;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogMenu : ScriptableObject
{
    public Dialog[] openingDialog;
    public Topic[] availableTopics;
}

[System.Serializable]
public class Topic
{
    public bool topicAvailable = true;
    public string topicName = "";
    public Dialog[] topicResponse;
    public DialogChoice goodCop;
    public DialogChoice badCop;
    public DialogAccuse accuseCop;
}

[System.Serializable]
public class DialogChoice
{
    public Dialog[] choiceResponse;
    public Evidence newEvidence;
    public int[] newTopics;
}

[System.Serializable]
public class DialogAccuse : DialogChoice
{
    public Evidence choiceEvidence;
    public Dialog[] choiceSuccess;
    public Dialog[] choiceFail;
}

[System.Serializable]
public class Dialog
{
    public bool playerIsTalking = false;
    public string text = "";
    public AudioClip voice; 
}
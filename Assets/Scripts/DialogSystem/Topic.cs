using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Topic : ScriptableObject
{
    public bool topicAvailable = true;
    public string topicName = "";
    public Dialog[] topicResponse;
    public DialogChoice goodCop;
    public DialogChoice badCop;
    public DialogAccuse accuseCop;
    public bool topicCompleted = false;

    // Runtime Settings
    internal bool rt_topicAvailable;
    internal bool rt_topicCompleted;

    private void OnEnable()
    {
        rt_topicAvailable = topicAvailable;
        rt_topicCompleted = topicCompleted;
    }
}

[System.Serializable]
public class DialogChoice
{
    public Dialog[] choiceResponse;
}

[System.Serializable]
public class DialogAccuse : DialogChoice
{
    public Evidence requiredEvidence;
    public Dialog[] choiceSuccess;
    public Dialog[] choiceFail;
}
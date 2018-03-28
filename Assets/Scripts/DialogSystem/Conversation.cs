using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Conversation : ScriptableObject
{
    public Dialog[] openingDialog;
    public Topic[] availableTopics;
    public Dialog[] closingDialog;

    public int AvailableTopics()
    {
        int topicCount = 0;

        for (int i = 0; i < availableTopics.Length; i++)
        {
            if (availableTopics[i].rt_topicAvailable && !availableTopics[i].rt_topicCompleted)
            {
                topicCount++;
            }
        }

        return topicCount;
    }
}
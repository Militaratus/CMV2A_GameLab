using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PT_Crime : MonoBehaviour
{
    public DialogPhoneSystem playerPhone;

    public Evidence newsTablet;
    public Evidence postIt;
    public Evidence emailTablet;
    public Evidence workTablet;
    public Evidence CrumbledEvidence;

    // Use this for initialization
    void Awake()
    {
        playerPhone = GameObject.FindGameObjectWithTag("Player").GetComponent<DialogPhoneSystem>();
    }

    // Update is called once per frame
    void Update ()
    {
        int scanCount = 0;

        if (newsTablet.amScanned)
        {
            scanCount++;
        }

        if (postIt.amScanned)
        {
            scanCount++;
        }

        if (emailTablet.amScanned)
        {
            scanCount++;
        }

        if (workTablet.amScanned)
        {
            scanCount++;
        }

        if (CrumbledEvidence.amScanned)
        {
            scanCount++;
        }

        if (scanCount > 3)
        {
            StartPhoneCall();
        }
    }

    void StartPhoneCall()
    {
        Conversation newConversation = Resources.Load("Conversations/PT_Arrest") as Conversation;
        playerPhone.StartPhoneCall(newConversation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PT_Appartment : MonoBehaviour
{
    public DialogPhoneSystem playerPhone;

    IEnumerator startCountdown;

    // Use this for initialization
    void Awake()
    {
        playerPhone = GameObject.FindGameObjectWithTag("Player").GetComponent<DialogPhoneSystem>();
        HandleStageChange();
    }

    void HandleStageChange()
    {
        // Stop Silly CoRoutine
        if (startCountdown != null)
        {
            StopCoroutine(startCountdown);
            startCountdown = null;
        }

        // Start the Start Countdown
        startCountdown = StartCountdown();
        StartCoroutine(startCountdown);

            
    }

    void StartPhoneCall()
    {
        Conversation newConversation = Resources.Load("Conversations/PT_OpeningCall") as Conversation;
        playerPhone.StartPhoneCall(newConversation);
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(10);
        StartPhoneCall();
    }
}

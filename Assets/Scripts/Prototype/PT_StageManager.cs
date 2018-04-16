using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class PT_StageManager : MonoBehaviour
{
    public enum StagePhase { START, PHONE, ALLEY, CRIME, ACCUSE }
    public StagePhase currentStage = StagePhase.START;

    public DialogPhoneSystem playerPhone;

    public GameObject stageOneRemoval;
    public GameObject stageTwoRemoval;
    public GameObject stageThreeRemoval;
    public GameObject stageFourRemoval;

    IEnumerator startCountdown;

	// Use this for initialization
	void Awake ()
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
        if (currentStage == StagePhase.START)
        {
            startCountdown = StartCountdown();
            StartCoroutine(startCountdown);
        }

        if (currentStage == StagePhase.PHONE)
        {
            Conversation newConversation = Resources.Load("Conversations/PT_OpeningCall") as Conversation;
            playerPhone.StartPhoneCall(newConversation);
        }

        if (currentStage == StagePhase.ALLEY)
        {
            stageOneRemoval.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update ()
    {
		if (currentStage == StagePhase.PHONE)
        {
            if (!playerPhone.inConversation)
            {
                currentStage = StagePhase.ALLEY;
                HandleStageChange();
            }
        }
	}

    void SendPhoneCall()
    {
        
        
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(10);
        currentStage = StagePhase.PHONE;
        HandleStageChange();
    }
}

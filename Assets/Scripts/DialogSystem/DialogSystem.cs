﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public static bool Dialog = true;
    public static bool Accuse = true;

    // Privates
    bool alreadyOpened = false;
    bool talkingPaused = false;
    bool talkingTopic = false;
    bool talkingChoice = false;
    bool talkingAccuse = false;
    bool talkingFinished = false;
    bool alreadyClosed = false;
    bool inConversation;
    bool inDialog;

    float conversationCooldown = 0;

    int activeTopic = -1;
    int dialogStage;
    int dialogStages;

    string characterName;

    Dialog[] currentDialog;

    Transform player;
    Transform canvasHead;

    // Private Components
    AudioSource audioPlayer;
    public GameObject[] topics;
    GameObject topicPanel;
    GameObject choicePanel;
    GameObject dialogPanel;
    GameObject evidencePanel;

    // Publics Scriptable Objects
    public Suspect mySuspect;
    public Conversation myConversation;

    // Public Components
    public Transform npcHead;

    // Game Manager References
    GameManager managerGame;
    DialogSubtitles dialogSubtitles;

    private void Awake()
    {
        GrabComponents();
        UpdateTopics();
        EndConversation();

        // Save Name
        if (mySuspect != null)
        {
            characterName = mySuspect.suspectName;
        }
        else
        {
            characterName = gameObject.name;
        }
    }

    void GrabComponents()
    {
        // CanvasHead
        canvasHead = transform.GetChild(0);

        // Grab Panels
        topicPanel = canvasHead.GetChild(0).gameObject;
        choicePanel = canvasHead.GetChild(1).gameObject;
        evidencePanel = canvasHead.GetChild(2).gameObject;

        // Grab Topics
        topics = new GameObject[topicPanel.transform.childCount];
        for (int i = 0; i < topics.Length; i++)
        {
            topics[i] = topicPanel.transform.GetChild(i).gameObject;
            topics[i].SetActive(false);
        }

        // Grab Audio Player
        audioPlayer = canvasHead.GetComponent<AudioSource>();

        // Grab Game Manager
        if (GameObject.Find("GameManager"))
        {
            managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        else
        {
            GameObject managerGamePrefab = Resources.Load("GameManager") as GameObject;
            GameObject managerGameInstant = Instantiate(managerGamePrefab);
            managerGameInstant.name = "GameManager";
            managerGame = managerGameInstant.GetComponent<GameManager>();
        }

        dialogSubtitles = GameObject.FindGameObjectWithTag("HeadCanvas").GetComponent<DialogSubtitles>();
    }

    void UpdateTopics()
    {
        if (myConversation != null)
        {
            for (int i = 0; i < myConversation.availableTopics.Length; i++)
            {
                if (myConversation.availableTopics[i].rt_topicAvailable)
                {
                    topics[i].transform.GetChild(0).GetComponent<Text>().text = myConversation.availableTopics[i].topicName;
                    topics[i].SetActive(true);
                }
            }
        }
    }

    void UpdateEvidences()
    {
        managerGame.RefreshBleepBloop();
    }

    void StartConversation()
    {
        currentDialog = myConversation.openingDialog;
        dialogStage = 0;
        dialogStages = currentDialog.Length;
        UpdateConversation();
    }

    void CloseConversation()
    {
        currentDialog = myConversation.closingDialog;
        dialogStage = 0;
        dialogStages = currentDialog.Length;
        UpdateConversation();
    }

    void ContinueConversation()
    {
        if (talkingPaused)
        {
            EndConversation();
            return;
        }

        dialogStage++;
        if (dialogStage >= dialogStages)
        {
            inDialog = false;

            // Ensure we skip this when finished once
            if (currentDialog == myConversation.openingDialog)
            {
                alreadyOpened = true;
                //talkingFinished = true;
            }
            else if (currentDialog == myConversation.closingDialog)
            {
                alreadyClosed = true;
                //talkingFinished = true;
            }

            // Are we already talking about a topic, if not show topics if available
            if (!talkingTopic)
            {
                // Check Available Topics
                if (myConversation.AvailableTopics() > 0)
                {
                    SwitchPanel("topic");
                    return;
                }
                else
                {
                    if (!alreadyClosed)
                    {
                        CloseConversation();
                        return;
                    }
                    else
                    {
                        EndConversation();
                        return;
                    }
                }
            }
            else
            {
                // Have we already made a choice about a topic, if not show choices
                if (!talkingChoice && !talkingAccuse)
                {
                    SwitchPanel("choice");
                    return;
                }
                else
                {
                    if (talkingAccuse && !talkingFinished)
                    {
                        if (managerGame.gatheredEvidence != null && managerGame.gatheredEvidence.Count > 0)
                        {
                            SwitchPanel("evidence");
                            managerGame.SetBleepBloopMode(BleepBloop.Mode.Accuse);
                            return;
                        }
                        else
                        {
                            AccuseFail();
                            return;
                        }

                    }
                }
            }

            // Have we exhausted the topic?
            if (talkingFinished)
            {
                // Reset Variables
                talkingTopic = false;
                talkingChoice = false;
                talkingAccuse = false;
                talkingFinished = false;

                topics[activeTopic].GetComponent<Button>().interactable = false;
                myConversation.availableTopics[activeTopic].rt_topicCompleted = true;

                // Check Available Topics
                if (myConversation.AvailableTopics() > 0)
                {
                    SwitchPanel("topic");
                    return;
                }
                else
                {
                    if (!alreadyClosed)
                    {
                        CloseConversation();
                        return;
                    }
                    else
                    {
                        EndConversation();
                        return;
                    }
                }
            }
        }
        UpdateConversation();
    }

    void EndConversation()
    {
        player = null;
        inConversation = false;
        SwitchPanel("none");
        dialogSubtitles.HideText();

        // Set Mode
        managerGame.SetBleepBloopMode(BleepBloop.Mode.View);
    }

    public void ActivateTopic(int topicID)
    {
        activeTopic = topicID;
        currentDialog = myConversation.availableTopics[activeTopic].topicResponse;
        dialogStage = 0;
        dialogStages = currentDialog.Length;
        talkingTopic = true;
        UpdateConversation();
    }

    public void ChoiceBelieve()
    {
        DialogChoice activeResponse = myConversation.availableTopics[activeTopic].goodCop;
        currentDialog = activeResponse.choiceResponse;
        dialogStage = 0;
        dialogStages = currentDialog.Length;
        talkingChoice = true;
        talkingFinished = true;
        Dialog = false;
        UpdateConversation();
        

    }

    public void ChoicePressure()
    {
        DialogChoice activeResponse = myConversation.availableTopics[activeTopic].badCop;
        currentDialog = activeResponse.choiceResponse;
        dialogStage = 0;
        dialogStages = currentDialog.Length;
        talkingChoice = true;
        talkingFinished = true;
        Dialog = false;
        UpdateConversation();
      
    }

    public void ChoiceAccuse()
    {
        DialogAccuse activeResponse = myConversation.availableTopics[activeTopic].accuseCop;
        currentDialog = activeResponse.choiceResponse;
        dialogStage = 0;
        dialogStages = currentDialog.Length;
        talkingAccuse = true;
        Dialog = false;
        UpdateConversation();
    }

    public void AccuseAttempt(Evidence chosenEvidence)
    {
        DialogAccuse activeResponse = myConversation.availableTopics[activeTopic].accuseCop;
        if (chosenEvidence.evidenceName == activeResponse.requiredEvidence.evidenceName)
        {
            Accuse = false;
            AccuseSuccess();
        }
        else
        {
            Accuse = false;
            AccuseFail();
        }
    }

    void AccuseSuccess()
    {
        DialogAccuse activeResponse = myConversation.availableTopics[activeTopic].accuseCop;
        currentDialog = activeResponse.choiceSuccess;
        dialogStage = 0;
        dialogStages = currentDialog.Length;
        talkingFinished = true;
        UpdateConversation();

        // Set Mode
        managerGame.SetBleepBloopMode(BleepBloop.Mode.View);
    }

    void AccuseFail()
    {
        DialogAccuse activeResponse = myConversation.availableTopics[activeTopic].accuseCop;
        currentDialog = activeResponse.choiceFail;
        dialogStage = 0;
        dialogStages = currentDialog.Length;
        talkingFinished = true;
        UpdateConversation();

        // Set Mode
        managerGame.SetBleepBloopMode(BleepBloop.Mode.View);
    }

    void SwitchPanel(string newPanel)
    {
        topicPanel.SetActive(false);
        choicePanel.SetActive(false);
        evidencePanel.SetActive(false);

        switch (newPanel)
        {
            case "topic":
                topicPanel.SetActive(true); break;
            case "choice":
                choicePanel.SetActive(true); break;
            case "evidence":
                evidencePanel.SetActive(true); break;
            default:
                break;
        }
    }
    
    // Update is called once per frame
	void Update ()
    {
        // At least have the decency to look at the player when talking to them!!!
		if (inConversation)
        {
            canvasHead.LookAt(player.position);
            if (npcHead)
            {
                npcHead.LookAt(player.position);
            }
            

            if (inDialog && Time.time > conversationCooldown)
            {
                conversationCooldown = Time.time + 3;
                if (audioPlayer.isPlaying == false)
                {
                    ContinueConversation();
                }
            }
        }

        if (!inConversation && !audioPlayer.isPlaying && inDialog)
        {
            inDialog = false;
            dialogSubtitles.HideText();
            player = null;
        }
	}

    void UpdateConversation()
    {
        conversationCooldown = Time.time + 3;
        SwitchPanel("none");
        inDialog = true;

        if (currentDialog[dialogStage] != null)
        {
            audioPlayer.clip = currentDialog[dialogStage].voice;
            audioPlayer.Play();
        }
        else
        {
            Debug.LogError("ERROR: Voice of Dialog Stage " + dialogStage + " is missing!");
        }

        string headsetDialogName = "";
        headsetDialogName = characterName;
        if (currentDialog[dialogStage].playerIsTalking)
        {
            headsetDialogName = "Me";
        }

        string headsetDialogText = currentDialog[dialogStage].text;
        dialogSubtitles.UpdateText(headsetDialogText, headsetDialogName);

        // Did we unlock anything?
        // Suspects
        if (currentDialog[dialogStage].newSuspect != null)
        {
            dialogSubtitles.UpdateSuspect(currentDialog[dialogStage].newSuspect.suspectName);
            managerGame.AddSuspect(currentDialog[dialogStage].newSuspect);
        }
        else
        {
            dialogSubtitles.HideSuspect();
        }

        // Evidence
        if (currentDialog[dialogStage].newEvidence != null)
        {
            dialogSubtitles.UpdateEvidence(currentDialog[dialogStage].newEvidence.evidenceName);
            managerGame.AddEvidence(currentDialog[dialogStage].newEvidence);
        }
        else
        {
            dialogSubtitles.HideEvidence();
        }

        // Topics
        if (currentDialog[dialogStage].newTopic != null)
        {
            currentDialog[dialogStage].newTopic.rt_topicAvailable = true;
        }

        // Refresh the amount of evidence & topics
        UpdateTopics();
        UpdateEvidences();
    }
    
    public void PlayerEnter(Transform newPlayer)
    {
        player = newPlayer;
        inConversation = true;

        // store the conversation reference in GameManager
        managerGame.SetConversation(this);

        if (myConversation != null)
        {
            // Was I still talking?
            if (talkingPaused)
            {
                talkingPaused = false;
                UpdateConversation();
            }
            else
            {
                if (!alreadyOpened)
                {
                    StartConversation();
                }
                else
                {
                    if (myConversation.AvailableTopics() > 0)
                    {
                        SwitchPanel("topic");
                    }
                    else
                    {
                        player = null;
                        inConversation = false;
                    }
                }
            }
        }
    }

    public void PlayerExit()
    {
        if (inDialog)
        {
            talkingPaused = true;
        }
        else
        {
            EndConversation();
        }
    }
}

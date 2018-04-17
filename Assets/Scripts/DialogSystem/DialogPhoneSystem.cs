using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPhoneSystem : MonoBehaviour
{
    public string callerName;
    Dialog[] currentDialog;

    public bool inConversation = false;
    bool inDialog = false;

    int dialogStage;
    int dialogStages;
    float conversationCooldown = 0;

    string characterName;

    // Publics Scriptable Objects
    public Conversation myConversation;

    // Private Components
    AudioSource audioPlayer;

    // Game Manager References
    GameManager managerGame;
    DialogSubtitles dialogSubtitles;

    // Use this for initialization
    private void Awake()
    {
        GrabComponents();
        EndConversation();

        characterName = "Caller";
    }

    void GrabComponents()
    {
        // Grab Audio Player
        audioPlayer = GetComponent<AudioSource>();

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

    public void StartPhoneCall(Conversation newConversation)
    {
        myConversation = newConversation;
        inConversation = true;
        StartConversation();
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
        dialogStage++;
        if (dialogStage >= dialogStages)
        {
            inDialog = false;

            EndConversation();
            return;
        }
        UpdateConversation();
    }

    void EndConversation()
    {
        inConversation = false;
        dialogSubtitles.HideText();
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        // At least have the decency to look at the player when talking to them!!!
        if (inConversation)
        {
            if (inDialog && Time.time > conversationCooldown)
            {
                conversationCooldown = Time.time + 3;
                if (audioPlayer.isPlaying == false)
                {
                    ContinueConversation();
                }
            }
        }
    }

    void UpdateConversation()
    {
        conversationCooldown = Time.time + 3;
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
    }
}

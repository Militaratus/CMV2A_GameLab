using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    int activeTopic = -1;
    bool amAccusing = false;
    Topic activeDialog;
    public DialogMenu testDialog;

    Text personName;
    Text personDialog;

    GameObject[] topics;
    GameObject choice1;
    GameObject choice2;
    GameObject choice3;
    GameObject[] evidences;
    Evidence[] availableEvidence;

    GameObject topicPanel;
    GameObject choicePanel;
    GameObject dialogPanel;
    GameObject evidencePanel;

    float conversationCooldown = 0;
    bool inConversation;
    bool inDialog;
    Dialog[] currentDialog;
    int dialogStage;
    int dialogStages;
    AudioSource audioPlayer;
    Transform player;

    // GameManager reference
    GameManager managerGame;

    private void Awake()
    {
        GrabComponents();
        UpdateTopics();
        StopTalking();
    }

    void GrabComponents()
    {
        // Grab Panels
        topicPanel = transform.GetChild(0).gameObject;
        choicePanel = transform.GetChild(1).gameObject;
        evidencePanel = transform.GetChild(2).gameObject;
        dialogPanel = transform.GetChild(3).gameObject;

        // Grab Topics
        topics = new GameObject[topicPanel.transform.childCount];
        for (int i = 0; i < topics.Length; i++)
        {
            topics[i] = topicPanel.transform.GetChild(i).gameObject;
            topics[i].SetActive(false);
        }

        // Grab Choices
        choice1 = choicePanel.transform.GetChild(0).gameObject;
        choice2 = choicePanel.transform.GetChild(1).gameObject;
        choice3 = choicePanel.transform.GetChild(2).gameObject;

        // Grab Text References
        personName = dialogPanel.transform.GetChild(1).GetComponent<Text>();
        personDialog = dialogPanel.transform.GetChild(2).GetComponent<Text>();

        // Grab Evidence
        evidences = new GameObject[evidencePanel.transform.childCount];
        for (int i = 0; i < evidences.Length; i++)
        {
            evidences[i] = evidencePanel.transform.GetChild(i).gameObject;
            evidences[i].SetActive(false);
        }

        // Grab Audio Player
        audioPlayer = GetComponent<AudioSource>();

        // Grab Game Manager
        managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void UpdateTopics()
    {
        for (int i = 0; i < testDialog.availableTopics.Length; i++)
        {
            if (testDialog.availableTopics[i].topicAvailable)
            {
                topics[i].transform.GetChild(0).GetComponent<Text>().text = testDialog.availableTopics[i].topicName;
                topics[i].SetActive(true);
            }
        }
    }

    void UpdateEvidences()
    {
        availableEvidence = managerGame.GetEvidence(1);
        for (int i = 0; i < availableEvidence.Length; i++)
        {
            if (availableEvidence[i])
            {
                evidences[i].transform.GetChild(0).GetComponent<Text>().text = availableEvidence[i].evidenceName;
                evidences[i].SetActive(true);
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // At least have the decency to look at the player when talking to them!!!
		if (inConversation)
        {
            transform.LookAt(player.position);

            if (inDialog && Time.time > conversationCooldown)
            {
                conversationCooldown = Time.time + 1;
                if (audioPlayer.isPlaying == false)
                {
                    ContinueTalking();
                }
            }
        }
	}

    void StartTalking()
    {
        // Refresh the amount of evidence
        UpdateTopics();
        UpdateEvidences();

        inConversation = true;
        //topicPanel.SetActive(true);
        dialogPanel.SetActive(true);

        personName.text = gameObject.name;
        currentDialog = testDialog.openingDialog;
        dialogStage = 0;
        dialogStages = currentDialog.Length;
        UpdateConversation();
    }

    void ContinueTalking()
    {
        dialogStage++;
        Debug.Log("DIALOGSTAGE:" + dialogStage);
        Debug.Log("DIALOGSTAGES:" + dialogStages);
        if (dialogStage >= dialogStages)
        {
            inDialog = false;

            // Refresh the amount of evidence & topics
            UpdateTopics();
            UpdateEvidences();

            if (activeTopic < 0)
            {
                topicPanel.SetActive(true);
                choicePanel.SetActive(false);
                evidencePanel.SetActive(false);
            }
            else if (amAccusing)
            {
                topicPanel.SetActive(false);
                choicePanel.SetActive(false);
                evidencePanel.SetActive(true);

                // Do I even have any evidence?
                if (availableEvidence[0] == null)
                {
                    ChooseEvidence(0);
                }
            }
            else
            {
                topicPanel.SetActive(false);
                choicePanel.SetActive(true);
                evidencePanel.SetActive(false);
            }

            return;
        }

        UpdateConversation();
    }

    void UpdateConversation()
    {
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

        if (currentDialog[dialogStage].playerIsTalking)
        {
            personName.text = "Morgan";
        }
        else
        {
            // [OLDCODE] Bartender fix
            if (!transform.parent)
            {
                personName.text = gameObject.name;
            }
            else
            {
                personName.text = transform.parent.name;
            }
        }

        personDialog.text = currentDialog[dialogStage].text;
    }

    void StopTalking()
    {
        inConversation = false;
        topicPanel.SetActive(false);
        choicePanel.SetActive(false);
        dialogPanel.SetActive(false);
        evidencePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.transform;
            StartTalking();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = null;
            StopTalking();
        }
    }

    public void ActivateTopic(int topicID)
    {
        activeTopic = topicID;
        currentDialog = testDialog.availableTopics[activeTopic].topicResponse;
        dialogStage = 0;
        dialogStages = currentDialog.Length;
        UpdateConversation();

        topicPanel.SetActive(false);
        choicePanel.SetActive(false);
        evidencePanel.SetActive(false);
    }

    public void ChooseGoodCop()
    {
        DialogChoice activeRespone = testDialog.availableTopics[activeTopic].goodCop;
        currentDialog = activeRespone.choiceResponse;
        dialogStage = 0;
        dialogStages = currentDialog.Length;
        UpdateConversation();
        //topics[activeTopic].GetComponent<Button>().interactable = false;
        activeTopic = -1;

        topicPanel.SetActive(false);
        choicePanel.SetActive(false);
    }

    public void ChooseBadCop()
    {
        DialogChoice activeRespone = testDialog.availableTopics[activeTopic].badCop;
        currentDialog = activeRespone.choiceResponse;
        dialogStage = 0;
        dialogStages = currentDialog.Length;
        UpdateConversation();
        //topics[activeTopic].GetComponent<Button>().interactable = false;
        activeTopic = -1;

        topicPanel.SetActive(false);
        choicePanel.SetActive(false);
    }

    public void ChooseAccuseCop()
    {
        DialogAccuse activeRespone = testDialog.availableTopics[activeTopic].accuseCop;
        currentDialog = activeRespone.choiceResponse;
        dialogStage = 0;
        dialogStages = currentDialog.Length;
        UpdateConversation();
        //topics[activeTopic].GetComponent<Button>().interactable = false;
        amAccusing = true;

        topicPanel.SetActive(false);
        choicePanel.SetActive(false);
    }

    public void ChooseEvidence(int chosenEvidence)
    {
        DialogAccuse activeRespone = testDialog.availableTopics[activeTopic].accuseCop;
        if (availableEvidence[0] != null && availableEvidence[chosenEvidence].evidenceName == activeRespone.choiceEvidence.evidenceName)
        {
            currentDialog = activeRespone.choiceSuccess;
        }
        else
        {
            currentDialog = activeRespone.choiceFail;
        }
        dialogStage = 0;
        dialogStages = currentDialog.Length;
        UpdateConversation();
        //topics[activeTopic].GetComponent<Button>().interactable = false;
        activeTopic = -1;
        amAccusing = false;

        topicPanel.SetActive(false);
        choicePanel.SetActive(false);
        evidencePanel.SetActive(false);
    }
}

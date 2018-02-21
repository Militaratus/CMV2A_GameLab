using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    int activeTopic = -1;
    Topic activeDialog;
    public DialogMenu testDialog;

    Text personName;
    Text personDialog;

    GameObject[] topics;
    GameObject choice1;
    GameObject choice2;
    GameObject choice3;

    GameObject topicPanel;
    GameObject choicePanel;
    GameObject dialogPanel;

    bool inConversation;
    bool inDialog;
    Dialog[] currentDialog;
    int dialogStage;
    int dialogStages;
    AudioSource audioPlayer;
    Transform player;

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
        dialogPanel = transform.GetChild(2).gameObject;

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
        personName = dialogPanel.transform.GetChild(0).GetComponent<Text>();
        personDialog = dialogPanel.transform.GetChild(1).GetComponent<Text>();

        // Grab Audio Player
        audioPlayer = GetComponent<AudioSource>();
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
            transform.LookAt(player.position + player.up);

            if (inDialog)
            {
                Debug.Log(audioPlayer.isPlaying);
                if (audioPlayer.isPlaying == false)
                {
                    ContinueTalking();
                }
            }
        }
	}

    void StartTalking()
    {
        inConversation = true;
        //topicPanel.SetActive(true);
        dialogPanel.SetActive(true);

        personName.text = gameObject.name;
        currentDialog = testDialog.openingDialog;
        dialogStages = currentDialog.Length;
        UpdateConversation();
    }

    void ContinueTalking()
    {
        dialogStage++;
        Debug.Log(dialogStage);
        Debug.Log(dialogStages);
        if (dialogStage > dialogStages)
        {
            inDialog = false;

            if (activeTopic < 0)
            {
                topicPanel.SetActive(true);
                choicePanel.SetActive(false);
            }
            else
            {
                topicPanel.SetActive(false);
                choicePanel.SetActive(true);
            }

            return;
        }

        UpdateConversation();
    }

    void UpdateConversation()
    {
        inDialog = true;
        audioPlayer.clip = currentDialog[dialogStage].voice;
        audioPlayer.Play();

        if (currentDialog[dialogStage].playerIsTalking)
        {
            personName.text = "Morgan";
        }
        else
        {
            personName.text = gameObject.name;
        }

        personDialog.text = currentDialog[dialogStage].text;
    }

    void StopTalking()
    {
        inConversation = false;
        topicPanel.SetActive(false);
        choicePanel.SetActive(false);
        dialogPanel.SetActive(false);
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
        activeTopic = -1;

        topicPanel.SetActive(false);
        choicePanel.SetActive(false);
    }
}

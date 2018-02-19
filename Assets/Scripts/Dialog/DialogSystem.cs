using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    int activeTopic;
    Dialog activeDialog;
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
    }

    void UpdateTopics()
    {
        for (int i = 0; i < testDialog.availableDialog.Length; i++)
        {
            if (testDialog.availableDialog[i].topicAvailable)
            {
                topics[i].transform.GetChild(0).GetComponent<Text>().text = testDialog.availableDialog[i].topicName;
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
        }
	}

    void StartTalking()
    {
        inConversation = true;
        topicPanel.SetActive(true);
        dialogPanel.SetActive(true);

        personName.text = gameObject.name;
        personDialog.text = testDialog.openingDialog;
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
        activeDialog = testDialog.availableDialog[activeTopic];

        if (activeDialog.topicResponse.Length == 1)
        {
            personDialog.text = activeDialog.topicResponse[0];
        }
        topicPanel.SetActive(false);
        choicePanel.SetActive(true);
    }

    public void ChooseGoodCop()
    {
        DialogChoice activeRespone = activeDialog.goodCop;

        if (activeRespone.choiceResponse.Length == 1)
        {
            personDialog.text = activeRespone.choiceResponse[0];
        }
        topics[activeTopic].GetComponent<Button>().interactable = false;
        topicPanel.SetActive(true);
        choicePanel.SetActive(false);
    }

    public void ChooseBadCop()
    {
        DialogChoice activeRespone = activeDialog.badCop;

        if (activeRespone.choiceResponse.Length == 1)
        {
            personDialog.text = activeRespone.choiceResponse[0];
        }
        topics[activeTopic].GetComponent<Button>().interactable = false;
        topicPanel.SetActive(true);
        choicePanel.SetActive(false);
    }

    public void ChooseAccuseCop()
    {
        DialogAccuse activeRespone = activeDialog.accuseCop;

        if (activeRespone.choiceResponse.Length == 1)
        {
            personDialog.text = activeRespone.choiceResponse[0];
        }
        topics[activeTopic].GetComponent<Button>().interactable = false;
        topicPanel.SetActive(true);
        choicePanel.SetActive(false);
    }
}

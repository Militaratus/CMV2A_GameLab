using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    Text personName;
    Text personDialog;

    GameObject choice1;
    GameObject choice2;
    GameObject choice3;

    GameObject choicePanel;
    GameObject dialogPanel;

    bool inConversation;
    Transform player;

    private void Awake()
    {
        GrabComponents();
        StopTalking();
    }

    void GrabComponents()
    {
        // Grab Panels
        choicePanel = transform.GetChild(0).gameObject;
        dialogPanel = transform.GetChild(1).gameObject;

        // Grab Choices
        choice1 = choicePanel.transform.GetChild(0).gameObject;
        choice2 = choicePanel.transform.GetChild(1).gameObject;
        choice3 = choicePanel.transform.GetChild(2).gameObject;

        // Grab Text References
        personName = dialogPanel.transform.GetChild(0).GetComponent<Text>();
        personDialog = dialogPanel.transform.GetChild(1).GetComponent<Text>();
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
        choicePanel.SetActive(true);
        dialogPanel.SetActive(true);

        personName.text = gameObject.name;
        personDialog.text = "Oh hai Detective!";
    }

    void StopTalking()
    {
        inConversation = false;
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
}

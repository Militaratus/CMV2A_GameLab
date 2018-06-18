using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TutorialText : MonoBehaviour {

    public GameObject Teleport;
    public GameObject PickUP;
    public GameObject Intro;
    float timer = 1f;
    bool startTimer = true;
	int tutorialDone = 0;

    // Use this for initialization
    void Start () {
		//AnalyticsEvent.TutorialStart("Appartment");
    }
	
	// Update is called once per frame
	void Update () {
        if (startTimer == true)
        {
            timer = timer - Time.deltaTime;
            Debug.Log(timer);
        }
        
        if(timer <= 0 && startTimer == true)
        {
			
			Intro.SetActive(false);
            Teleport.SetActive(true);
            startTimer = false;
        }
		if(VRTK.VRTK_ControllerEvents.TeleportActive == false)
        {
            Teleport.SetActive(false);
            PickUP.SetActive(true);
        }
        if(StandardObject.PickUPActive == false && VRTK.VRTK_ControllerEvents.TeleportActive == false && tutorialDone == 0)
        {
            PickUP.SetActive(false);
			//AnalyticsEvent.TutorialComplete("Appartment");
			tutorialDone = 1;
        }
	}
}

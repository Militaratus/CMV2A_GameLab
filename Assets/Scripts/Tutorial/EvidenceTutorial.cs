using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceTutorial : MonoBehaviour {

    public GameObject Evidence;
	int tutorialDone = 0;

    // Use this for initialization
    void Start () {
		//AnalyticsEvent.TutorialStart("Policedepartment");
	}
	
	// Update is called once per frame
	void Update () {
		if(DialogSystem.Accuse == false && tutorialDone == 0)
        {
            Evidence.SetActive(false);
			//AnalyticsEvent.TutorialComplete("Policedepartment");
			tutorialDone = 1;
        }
	}
}

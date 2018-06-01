using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics.Experimental;

public class EvidenceTutorial : MonoBehaviour {

    public GameObject Evidence;

    // Use this for initialization
    void Start () {
		AnalyticsEvent.TutorialStart("Policedepartment");
	}
	
	// Update is called once per frame
	void Update () {
		if(DialogSystem.Accuse == false)
        {
            Evidence.SetActive(false);
			AnalyticsEvent.TutorialComplete("Policedepartment");
        }
	}
}

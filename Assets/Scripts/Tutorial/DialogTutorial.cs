using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics.Experimental;

public class DialogTutorial : MonoBehaviour {

    public GameObject Dialog;
	int tutorialDone = 0;

    // Use this for initialization
    void Start () {
		AnalyticsEvent.TutorialStart("Alley");
	}
	
	// Update is called once per frame
	void Update () {
		if(DialogSystem.Dialog == false && tutorialDone == 0)
        {
            Dialog.SetActive(false);
			AnalyticsEvent.TutorialComplete("Alley");
			tutorialDone = 1;
        }
	}
}

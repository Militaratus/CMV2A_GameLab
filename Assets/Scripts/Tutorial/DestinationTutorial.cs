using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestinationTutorial : MonoBehaviour {

    public GameObject destinationCanvas;

	int tutorialDone = 0;

    // Use this for initialization
    void Start () {
		//AnalyticsEvent.TutorialStart("Loading");
	}
	
	// Update is called once per frame
	void Update () {
		if (LoadingManager.destinationChosen == true && tutorialDone == 0)
        {
            destinationCanvas.SetActive(false);
			//AnalyticsEvent.TutorialComplete("Loading");
			tutorialDone = 1;
        }
	}
}

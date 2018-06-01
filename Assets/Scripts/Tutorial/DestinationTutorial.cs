using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics.Experimental;

public class DestinationTutorial : MonoBehaviour {

    public GameObject destinationCanvas;

    // Use this for initialization
    void Start () {
		AnalyticsEvent.TutorialStart("Loading");
	}
	
	// Update is called once per frame
	void Update () {
        if (LoadingManager.destinationChosen == true)
        {
            destinationCanvas.SetActive(false);
			AnalyticsEvent.TutorialComplete("Loading");
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics.Experimental;

public class BleepBloopTutorial : MonoBehaviour {

    public GameObject bleepBloopCanvas;
    public GameObject evidenceCanvas;
    public GameObject clueBotCanvas;
	int tutorialDone = 0;

    // Use this for initialization
    void Start () {
		AnalyticsEvent.TutorialStart("CrimesceneAppartment");
	}
	
	// Update is called once per frame
	void Update () {
        if (BleepBloop.bleepBloopActive == true)
        {
            bleepBloopCanvas.SetActive(false);
            clueBotCanvas.SetActive(true);
        }
        if (BleepBloop.clueBotSpawned == true && BleepBloop.bleepBloopActive == true)
        {
            clueBotCanvas.SetActive(false);
            evidenceCanvas.SetActive(true);
        }
        if(BleepBloop.evidenceChecked == true && BleepBloop.clueBotSpawned == true && BleepBloop.bleepBloopActive == true && tutorialDone == 0)
        {
            evidenceCanvas.SetActive(false);
			AnalyticsEvent.TutorialComplete("CrimesceneAppartment");
			tutorialDone = 1;
        }

    }
}

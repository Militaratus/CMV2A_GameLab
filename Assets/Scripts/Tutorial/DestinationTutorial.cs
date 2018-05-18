using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestinationTutorial : MonoBehaviour {

    public GameObject destinationCanvas;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (LoadingManager.destinationChosen == true)
        {
            destinationCanvas.SetActive(false);
        }
	}
}

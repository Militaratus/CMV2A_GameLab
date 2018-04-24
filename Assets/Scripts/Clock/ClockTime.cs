using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockTime : MonoBehaviour {

    string time;

    public Text clockText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        time = System.DateTime.Now.ToString("HH:mm");
        Debug.Log(time);
        clockText.text = time;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAnalyticsHeatmap;

public class MakeHeatmap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UnityAnalyticsHeatmap.HeatmapEvent.Send("PlayerDeath", transform, Time.time);
	}
}

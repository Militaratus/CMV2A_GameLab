using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class TimeSpentOnLevel : MonoBehaviour
{
    float timeStart;

	void OnEnable ()
    {
        timeStart = Time.time;
	}
	
	void OnDisable ()
    {
        float timeEnd = Time.time;
        float timeSpent = timeEnd - timeStart;
        // TimeSpent Analytics
        Analytics.CustomEvent("TimeSpent", new Dictionary<string, object>
        {
            { "Seconds", timeSpent }
        });
    }
}

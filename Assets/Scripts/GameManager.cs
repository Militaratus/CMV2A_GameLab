using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Evidence[] gatheredEvidence;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddEvidence(Evidence newEvidence)
    {
        if (gatheredEvidence.Length < 1)
        {
            gatheredEvidence = new Evidence[1];
        }
        else
        {
            gatheredEvidence = new Evidence[gatheredEvidence.Length + 1];
        }

        gatheredEvidence[gatheredEvidence.Length - 1] = newEvidence;
    }

    public bool CheckAddedEvidence(Evidence newEvidence)
    {
        bool alreadyAdded = false;

        if (gatheredEvidence.Length < 1)    // You have no power (and stuff) here!
        {
            return false;
        }

        for (int i = 0; i < gatheredEvidence.Length; i++)
        {
            if (gatheredEvidence[i] == newEvidence) // Found it, stop checking!
            {
                alreadyAdded = true;
                break;
            }
        }

        return alreadyAdded;
    }

    public Evidence[] GetEvidence(int page)
    {
        Evidence[] availableEvidence = new Evidence[10];

        for (int i = 0; i < availableEvidence.Length; i++)
        {
            if ((i * page) < gatheredEvidence .Length)
            {
                availableEvidence[i] = gatheredEvidence[i * page];
            }
        }

        return availableEvidence;
    }
}

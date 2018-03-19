using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Evidence> gatheredEvidence = new List<Evidence>();
    public List<Suspect> foundSuspects = new List<Suspect>();

	// Use this for initialization
	void Awake ()
    {
        ProtectMeFromDeath();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddEvidence(Evidence newEvidence)
    {
        if (!CheckAddedEvidence(newEvidence))   // Double check
        {
            newEvidence.amScanned = false; // Reset the Scriptable Object
            gatheredEvidence.Add(newEvidence);
        }
    }

    public bool CheckAddedEvidence(Evidence newEvidence)
    {
        bool alreadyAdded = false;

        if (gatheredEvidence.Count < 1)    // You have no power (and stuff) here!
        {
            return false;
        }

        for (int i = 0; i < gatheredEvidence.Count; i++)
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
            if ((i * page) < gatheredEvidence.Count)
            {
                availableEvidence[i] = gatheredEvidence[i * page];
            }
        }

        return availableEvidence;
    }

    public List<Evidence> GetNewEvidence()
    {
        return gatheredEvidence;
    }

    public void AddSuspect(Suspect newSuspect)
    {
        if (!CheckAddedSuspects(newSuspect))
        {
            foundSuspects.Add(newSuspect);
        }
    }

    public bool CheckAddedSuspects(Suspect newSuspect)
    {
        bool alreadyAdded = false;

        if (foundSuspects.Count < 1)    // You have no power (and stuff) here!
        {
            return false;
        }

        for (int i = 0; i < foundSuspects.Count; i++)
        {
            if (foundSuspects[i] == newSuspect) // Found it, stop checking!
            {
                alreadyAdded = true;
                break;
            }
        }

        return alreadyAdded;
    }

    public List<Suspect> GetSuspects()
    {
        return foundSuspects;
    }

    void ProtectMeFromDeath()
    {
        DontDestroyOnLoad(gameObject);
    }
}

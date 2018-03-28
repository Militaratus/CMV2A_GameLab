using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Evidence> gatheredEvidence = new List<Evidence>();
    public List<Suspect> foundSuspects = new List<Suspect>();

    public GameData gameData;
    private string gameDataProjectFilePath = "/StreamingAssets/data.json";

    // Use this for initialization
    void Awake ()
    {
        ProtectMeFromDeath();
        AutoLoad();
        gatheredEvidence = gameData.gatheredEvidence;
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
            gameData.gatheredEvidence = gatheredEvidence;
            AutoSave();
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

    public List<Evidence> GetEvidence()
    {
        return gatheredEvidence;
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
            gameData.foundSuspects = foundSuspects;
            AutoSave();
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

    public void AutoSave()
    {
        string dataAsJson = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);
    }

    public void AutoLoad()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;

        if (File.Exists (filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            gameData = new GameData();
        }
    }
}

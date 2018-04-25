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

    public BleepBloop bleepBloop;
    public DialogSystem currentConversation;

    // Use this for initialization
    void Awake ()
    {
        ProtectMeFromDeath();
        AutoLoad();
        gatheredEvidence = gameData.gatheredEvidence;
    }

    public void SetBleepBloop(BleepBloop newBleepBloop)
    {
        if (bleepBloop == null)
        {
            bleepBloop = GameObject.FindGameObjectWithTag("BleepBloop").GetComponent<BleepBloop>();
        }

        bleepBloop = newBleepBloop;
    }

    public void SetConversation(DialogSystem newConversation)
    {
        currentConversation = newConversation;
    }

    public void SetBleepBloopMode(BleepBloop.Mode newMode)
    {
        if (bleepBloop == null)
        {
            bleepBloop = GameObject.FindGameObjectWithTag("BleepBloop").GetComponent<BleepBloop>();
        }

        bleepBloop.ChangeMode(newMode);

        if (newMode == BleepBloop.Mode.View)
        {
            currentConversation = null;
        }
    }

    public void RefreshBleepBloop()
    {
        if (bleepBloop == null)
        {
            bleepBloop = GameObject.FindGameObjectWithTag("BleepBloop").GetComponent<BleepBloop>();
        }

        bleepBloop.UpdateContent();
    }

    public void PassEvidenceConversation(int evidenceID)
    {
        currentConversation.AccuseAttempt(gatheredEvidence[evidenceID]);
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        
    }

    public void AddEvidence(Evidence newEvidence)
    {
        if (!CheckAddedEvidence(newEvidence))   // Double check
        {
            if (gatheredEvidence == null)
            {
                gatheredEvidence = new List<Evidence>();
            }

            newEvidence.amScanned = false; // Reset the Scriptable Object
            gatheredEvidence.Add(newEvidence);
            gameData.gatheredEvidence = gatheredEvidence;
            AutoSave();
            RefreshBleepBloop();
        }
    }

    public bool CheckAddedEvidence(Evidence newEvidence)
    {
        bool alreadyAdded = false;

        if (gatheredEvidence == null || gatheredEvidence.Count < 1)    // You have no power (and stuff) here!
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
            RefreshBleepBloop();
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

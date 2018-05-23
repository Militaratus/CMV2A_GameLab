using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour { 

    private static string lastLevel;

    Vector3 loadingSpawn;
    Vector3 apartmentSpawn;

    GameObject SDKManager;

    void Start()
    {
        SDKManager = GameObject.Find("[VRTK_SDKManager]");
    }

    public static void setLastLevel(string level)
    {
        lastLevel = level;
    }

    public static string getLastLevel()
    {
        return lastLevel;
    }

    void Update()
    {
        if(lastLevel == "Loading")
        {
            SDKManager.transform.position = new Vector3(-5.55f, -2.53f, 28.74f);
        }
        if(lastLevel == "Crimesceneappartment")
        {
            SDKManager.transform.position = new Vector3(-2.28f, -2.53f, 41.36f);
        }
    }
}
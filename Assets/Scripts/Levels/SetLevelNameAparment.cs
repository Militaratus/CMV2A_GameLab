using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLevelNameAparment : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        LevelManager.setLastLevel(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadInitialLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadNewScene());
    }
	
    // Loading Async from Unity Script Reference/Manual, slightly adapted for this project
    IEnumerator LoadNewScene()
    {
        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

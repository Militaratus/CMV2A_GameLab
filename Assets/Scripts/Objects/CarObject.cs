using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class CarObject : VRTK_InteractableObject
{

    public override void StartUsing(VRTK.VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        StartCoroutine(LoadNewScene());
    }

    // Loading Async from Unity Script Reference/Manual, slightly adapted for this project
    IEnumerator LoadNewScene()
    {
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Loading");

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

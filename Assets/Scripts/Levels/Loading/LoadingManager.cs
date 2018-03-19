using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    // Managers
    GameManager managerGame;

    public GameObject destinationPanel;
    public GameObject travelingPanel;

    string sceneToLoad = "";

    // Use this for initialization
    private void Awake()
    {
#if UNITY_EDITOR
        if (GameObject.Find("GameManager"))
        {
            managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        else
        {
            GameObject newGameManagerPrefab = Resources.Load("GameManager") as GameObject;
            GameObject newGameNager = Instantiate(newGameManagerPrefab);
            newGameNager.name = "GameManager";
            managerGame = newGameNager.GetComponent<GameManager>();
        }
#else
        managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
#endif
        destinationPanel.SetActive(true);
        travelingPanel.SetActive(false);
    }

    public void SelectDestination(string destination)
    {
        destinationPanel.SetActive(false);
        travelingPanel.SetActive(true);

        sceneToLoad = destination;
        StartCoroutine(LoadNewScene());
    }

    // Loading Async from Unity Script Reference/Manual, slightly adapted for this project
    IEnumerator LoadNewScene()
    {
        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

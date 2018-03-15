using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class ButtonManager : MonoBehaviour
{

    GameObject LineUp1Canvas;
    GameObject LineUp2Canvas;
    GameObject LineUp3Canvas;

    void Start()
    {
        LineUp1Canvas = GameObject.Find("Lineup1");
        LineUp2Canvas = GameObject.Find("Lineup2");
        LineUp3Canvas = GameObject.Find("Lineup3");
    }

    //If button is pressed load scene.
    public void MenuButton(string MainMenu)
    {
        Debug.Log("Pressed");
    }
    //If button is pressed quit game.
    public void ExitButton()
    {
        Application.Quit();
    }

    public void LineUp1()
    {
        Debug.Log("LineUP1");
        LineUp1Canvas.SetActive(false);

    }

    public void LineUp2()
    {
        Debug.Log("LineUP2");
    }

    public void LineUp3()
    {
        Debug.Log("LineUP3");
    }
}

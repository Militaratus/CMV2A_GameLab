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
    GameObject Dropdown1Canvas;
    GameObject Dropdown2Canvas;
    GameObject Dropdown3Canvas;
    GameObject BackButton;

    public AudioSource audio;


    void Start()
    {
        /*LineUp1Canvas = GameObject.Find("Lineup1");
        LineUp2Canvas = GameObject.Find("Lineup2");
        LineUp3Canvas = GameObject.Find("Lineup3");
        Dropdown1Canvas = GameObject.Find("Dropdown1");
        Dropdown2Canvas = GameObject.Find("Dropdown2");
        Dropdown3Canvas = GameObject.Find("Dropdown3");
        BackButton = GameObject.Find("Back");
        Dropdown1Canvas.SetActive(false);
        Dropdown2Canvas.SetActive(false);
        Dropdown3Canvas.SetActive(false);
        BackButton.SetActive(false);*/
    }

    //If button is pressed load scene.
    public void MenuButton(string MainMenu)
    {
        audio.Play();
        Debug.Log("Pressed");
    
    }
    //If button is pressed quit game.
    public void ExitButton()
    {
        audio.Play();
        Application.Quit();
    }

    public void LineUp1()
    {
        Debug.Log("LineUP1");
        LineUp1Canvas.SetActive(false);
        LineUp2Canvas.SetActive(false);
        LineUp3Canvas.SetActive(false);
        Dropdown1Canvas.SetActive(true);
        Dropdown2Canvas.SetActive(false);
        Dropdown3Canvas.SetActive(false);
        BackButton.SetActive(true);

    }

    public void LineUp2()
    {
        Debug.Log("LineUP2");
        LineUp1Canvas.SetActive(false);
        LineUp2Canvas.SetActive(false);
        LineUp3Canvas.SetActive(false);
        Dropdown2Canvas.SetActive(true);
        Dropdown1Canvas.SetActive(false);
        
        BackButton.SetActive(true);
    }

    public void LineUp3()
    {
        Debug.Log("LineUP3");
        LineUp1Canvas.SetActive(false);
        LineUp2Canvas.SetActive(false);
        LineUp3Canvas.SetActive(false);
        Dropdown1Canvas.SetActive(false);
        Dropdown2Canvas.SetActive(false);
        Dropdown3Canvas.SetActive(true);
        BackButton.SetActive(true);
    }

    public void Back()
    {
        LineUp1Canvas.SetActive(true);
        LineUp2Canvas.SetActive(true);
        LineUp3Canvas.SetActive(true);
        Dropdown1Canvas.SetActive(false);
        Dropdown2Canvas.SetActive(false);
        Dropdown3Canvas.SetActive(false);
        BackButton.SetActive(false);
    }

    public void Quit()
    {
        audio.Play();
        Application.Quit();
    }

    public void StartGame()
    {
        audio.Play();
        SceneManager.LoadScene("Appartment");
    }

    public void Credits()
    {
        audio.Play();
        SceneManager.LoadScene("Credits");
    }

    public void BackToStart()
    {
        audio.Play();
        SceneManager.LoadScene("StartMenu");
    }
}

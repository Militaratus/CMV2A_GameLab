using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class NextScene : MonoBehaviour
{

    public AudioClip Bleep;
    //private AudioSource audioPlayer;

    private VideoPlayer videoPlayer;

    GameObject tv;

    float Timer = 110f;

    // Use this for initialization
    void Start()
    {
        tv = transform.GetChild(0).gameObject;
        //audioPlayer = GetComponent<AudioSource>();
        videoPlayer = tv.GetComponent<VideoPlayer>();
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Timer = Timer - Time.deltaTime;
        Debug.Log(Timer);

        if (Timer <= 0)
        {
            SceneManager.LoadScene("Appartment");
            Debug.Log("Load");
        }
    }
}

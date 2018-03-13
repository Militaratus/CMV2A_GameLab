using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class BleepBloop : VRTK_InteractableObject
{
    public AudioClip Bleep;
    private AudioSource audioPlayer;

    internal Transform playerHead;
    internal Transform displayHead;

    GameObject menuCanvas;
    GameObject cluebotCanvas;
    GameObject noteCanvas;

    Button cluebotButton;
    Text cluebotText;

    int cluebotBattery = 100;

    bool canvasActive = false;
    bool cluebotActive = false;

    IEnumerator cluebotCoroutine;

    protected override void Awake()
    {
        base.Awake();

        audioPlayer = GetComponent<AudioSource>();
        audioPlayer.clip = Bleep;

        playerHead = GameObject.FindGameObjectWithTag("Player").transform;
        displayHead = transform.GetChild(0);

        menuCanvas = displayHead.GetChild(0).gameObject;
        cluebotCanvas = displayHead.GetChild(1).gameObject;
        cluebotButton = cluebotCanvas.transform.GetChild(2).GetComponent<Button>();
        cluebotText = cluebotButton.transform.GetChild(0).GetComponent<Text>();
        noteCanvas = displayHead.GetChild(2).gameObject;

        
    }

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);

        if (!canvasActive)
        {
            canvasActive = true;
            SwitchMenu("menu");
        }
        else if (canvasActive)
        {
            canvasActive = false;
            SwitchMenu("menu");
        }
    }

    public void SwitchMenu(string menu)
    {
        // Turn off everything
        menuCanvas.SetActive(false);
        cluebotCanvas.SetActive(false);
        noteCanvas.SetActive(false);

        if (menu == "menu")
        {
            menuCanvas.SetActive(true);
        }
        if (menu == "cluebot")
        {
            cluebotCanvas.SetActive(true);
        }
        if (menu == "note")
        {
            noteCanvas.SetActive(true);
        }

        audioPlayer.Play();
    }

    protected override void Update()
    {
        base.Update();

        // Aim the Display to the player
        displayHead.LookAt(playerHead.position);
    }

    public void ToggleCluebot()
    {
        cluebotButton.interactable = false;

        if (cluebotCoroutine != null)
        {
            StopCoroutine(cluebotCoroutine);
            cluebotCoroutine = null;
        }

        cluebotCoroutine = CluebotCoroutine();
        StartCoroutine(cluebotCoroutine);
    }

    IEnumerator CluebotCoroutine()
    {
        while (cluebotBattery > 0)
        {
            cluebotBattery--;
            cluebotText.text = cluebotBattery + "%";
            yield return new WaitForSeconds(1.0f);
        }

        Debug.Log("RECHARGING");

        while (cluebotBattery < 100)
        {
            cluebotBattery++;
            cluebotText.text = "RECHARGING...\n" + cluebotBattery + "%";
            yield return new WaitForSeconds(1.0f);
        }

        cluebotText.text = "Tap to Summon";
        cluebotButton.interactable = true;
    }
}

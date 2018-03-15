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
    GameObject noteCanvas;
    GameObject evidenceCanvas;
    GameObject scanningCanvas;
    GameObject viewCanvas;
    GameObject suspectCanvas;
    GameObject mapCanvas;
    GameObject cluebotCanvas;

    Scrollbar scanningSlider;
    Text scanningText;

    Button cluebotButton;
    Text cluebotText;

    Light displayLight;

    float scanningProgress = 0;
    int cluebotBattery = 100;

    bool canvasActive = false;
    bool cluebotActive = false;

    IEnumerator cluebotCoroutine;
    IEnumerator scanningRoutine;

    protected override void Awake()
    {
        base.Awake();

        audioPlayer = GetComponent<AudioSource>();
        audioPlayer.clip = Bleep;

        playerHead = GameObject.FindGameObjectWithTag("Player").transform;
        displayHead = transform.GetChild(0);

        displayLight = displayHead.GetComponent<Light>();

        menuCanvas = displayHead.GetChild(0).gameObject;
        noteCanvas = displayHead.GetChild(1).gameObject;
        evidenceCanvas = displayHead.GetChild(2).gameObject;
        scanningCanvas = displayHead.GetChild(3).gameObject;
        scanningSlider = scanningCanvas.transform.GetChild(3).GetComponent<Scrollbar>();
        scanningText = scanningSlider.transform.GetChild(1).GetComponent<Text>();

        viewCanvas = displayHead.GetChild(4).gameObject;
        suspectCanvas = displayHead.GetChild(5).gameObject;
        mapCanvas = displayHead.GetChild(6).gameObject;
        cluebotCanvas = displayHead.GetChild(7).gameObject;
        cluebotButton = cluebotCanvas.transform.GetChild(1).GetComponent<Button>();
        cluebotText = cluebotButton.transform.GetChild(0).GetComponent<Text>();

        TurnOff();
    }

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        ToggleDisplay();
        StopUsing();    // Prevents Coroutine crash when hammering the use button
    }

    void ToggleDisplay()
    {
        if (!canvasActive)
        {
            canvasActive = true;
            SwitchMenu("menu");
        }
        else if (canvasActive)
        {
            canvasActive = false;
            SwitchMenu("nothing");
        }

        displayLight.enabled = canvasActive;
    }

    void TurnOff()
    {
        menuCanvas.SetActive(false);
        noteCanvas.SetActive(false); ;
        evidenceCanvas.SetActive(false);
        scanningCanvas.SetActive(false);
        viewCanvas.SetActive(false);
        suspectCanvas.SetActive(false);
        mapCanvas.SetActive(false);
        cluebotCanvas.SetActive(false);
    }

    public void SwitchMenu(string menu)
    {
        // Turn off everything
        TurnOff();

        // Turn off unneccesary Coroutines
        if (scanningRoutine != null)
        {
            StopCoroutine(scanningRoutine);
            scanningRoutine = null;
        }

        if (menu == "menu")
        {
            menuCanvas.SetActive(true);
        }
        if (menu == "notes")
        {
            noteCanvas.SetActive(true);
        }
        if (menu == "evidence")
        {
            evidenceCanvas.SetActive(true);
        }
        if (menu == "scanning")
        {
            scanningRoutine = ScanningCoroutine();
            StartCoroutine(scanningRoutine);
            scanningCanvas.SetActive(true);
        }
        if (menu == "view")
        {
            viewCanvas.SetActive(true);
        }
        if (menu == "suspects")
        {
            suspectCanvas.SetActive(true);
        }
        if (menu == "map")
        {
            mapCanvas.SetActive(true);
        }
        if (menu == "cluebot")
        {
            cluebotCanvas.SetActive(true);
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

    public void CheckEvidence(int evidenceID)
    {
        SwitchMenu("scanning");
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

    IEnumerator ScanningCoroutine()
    {
        scanningProgress = 0;
        scanningSlider.size = scanningProgress / 100;
        scanningText.text = scanningProgress + "%";

        while (scanningProgress < 100)
        {
            scanningProgress += Random.Range(10, 25);
            if (scanningProgress > 100)
            {
                scanningProgress = 100;
            }
            scanningSlider.size = scanningProgress / 100;
            scanningText.text = scanningProgress + "%";
            yield return new WaitForSeconds(1.0f);
        }

        SwitchMenu("view");
    }
}

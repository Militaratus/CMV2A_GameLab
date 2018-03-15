﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class BleepBloop : VRTK_InteractableObject
{
    private GameManager managerGame;

    public List<Evidence> foundEvidence;
    GameObject evidencePrefab;
    GameObject[] evidenceContent;
    List<Suspect> foundSuspects;
    GameObject suspectPrefab;
    GameObject[] suspectContent;

    public AudioClip Bleep;
    private AudioSource audioPlayer;

    internal Transform playerHead;
    internal Transform displayHead;

    GameObject loadingCanvas;
    GameObject menuCanvas;
    GameObject noteCanvas;
    GameObject evidenceCanvas;
    GameObject scanningCanvas;
    GameObject viewCanvas;
    GameObject suspectCanvas;
    GameObject mapCanvas;
    GameObject cluebotCanvas;

    Transform evidenceContainer;
    Transform suspectContainer;

    int activeScanID = -1;
    Scrollbar scanningSlider;
    Text scanningText;

    Text viewText;

    Button cluebotButton;
    Text cluebotText;

    Light displayLight;

    float scanningProgress = 0;
    int cluebotBattery = 100;

    bool amLoading = false;
    float loadingTimer = 0;
    bool canvasActive = false;
    bool cluebotActive = false;

    IEnumerator cluebotCoroutine;
    IEnumerator scanningRoutine;

    protected override void Awake()
    {
        base.Awake();

        // Grab Game Manager
        managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();

        evidencePrefab = Resources.Load("UI/EvidenceButton") as GameObject;
        suspectPrefab = Resources.Load("UI/SuspectPanel") as GameObject;

        audioPlayer = GetComponent<AudioSource>();
        audioPlayer.clip = Bleep;

        playerHead = GameObject.FindGameObjectWithTag("Player").transform;
        displayHead = transform.GetChild(0);

        displayLight = displayHead.GetComponent<Light>();

        loadingCanvas = displayHead.GetChild(0).gameObject;
        menuCanvas = displayHead.GetChild(1).gameObject;
        noteCanvas = displayHead.GetChild(2).gameObject;
        evidenceCanvas = displayHead.GetChild(3).gameObject;
        scanningCanvas = displayHead.GetChild(4).gameObject;
        viewCanvas = displayHead.GetChild(5).gameObject;
        suspectCanvas = displayHead.GetChild(6).gameObject;
        mapCanvas = displayHead.GetChild(7).gameObject;
        cluebotCanvas = displayHead.GetChild(8).gameObject;

        viewText = viewCanvas.transform.GetChild(2).GetComponent<Text>();

        scanningSlider = scanningCanvas.transform.GetChild(3).GetComponent<Scrollbar>();
        scanningText = scanningSlider.transform.GetChild(1).GetComponent<Text>();
        cluebotButton = cluebotCanvas.transform.GetChild(1).GetComponent<Button>();
        cluebotText = cluebotButton.transform.GetChild(0).GetComponent<Text>();

        evidenceContainer = evidenceCanvas.transform.GetChild(1).GetChild(0).GetChild(0);
        suspectContainer = suspectCanvas.transform.GetChild(1).GetChild(0).GetChild(0);

        TurnOff();
    }

    void UpdateContent()
    {
        UpdateEvidence();
        UpdateSuspects();
    }

    void UpdateEvidence()
    {
        // Clear old
        if (foundEvidence != null && evidenceContent != null)
        {
            for (int i = 0; i < evidenceContent.Length; i++)
            {
               evidenceContent[i].GetComponent<Button>().onClick.RemoveAllListeners();
               Destroy(evidenceContent[i]);
            }
        }

        foundEvidence = managerGame.GetNewEvidence();
        if (foundEvidence.Count > 0)    // Only spawn if there is something to spawn
        {
            evidenceContent = new GameObject[foundEvidence.Count];
            for (int i = 0; i < evidenceContent.Length; i++)
            {
                evidenceContent[i] = Instantiate(evidencePrefab, evidenceContainer);
                evidenceContent[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -30 + (i * -60));
                evidenceContent[i].GetComponent<Button>().onClick.AddListener(delegate { CheckEvidence(i-1); });

                // Populate Children
                evidenceContent[i].transform.GetChild(1).GetComponent<Text>().text = foundEvidence[i].evidenceName;
                evidenceContent[i].transform.GetChild(2).GetComponent<Text>().text = foundEvidence[i].evidenceDescription;

                if (!foundEvidence[i].amScanned)
                {
                    evidenceContent[i].transform.GetChild(3).GetComponent<Text>().text = "Unscanned";
                }
                else
                {
                    evidenceContent[i].transform.GetChild(3).GetComponent<Text>().text = foundEvidence[i].evidenceInformation;
                }
            }
        }
    }

    void UpdateSuspects()
    {
        // Clear old
        if (foundSuspects != null && suspectContent != null)
        {
            for (int i = 0; i < suspectContent.Length; i++)
            {
                Destroy(suspectContent[i]);
            }
        }

        foundSuspects = managerGame.GetSuspects();
        if (foundSuspects.Count > 0)    // Only spawn if there is something to spawn
        {
            suspectContent = new GameObject[foundSuspects.Count];
            for (int i = 0; i < suspectContent.Length; i++)
            {
                suspectContent[i] = Instantiate(evidencePrefab, evidenceContainer);
                suspectContent[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -30 + (i * -60));
            }
        }
    }

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        if (!canvasActive)
        {
            canvasActive = true;
            SwitchMenu("loading");
            displayLight.enabled = canvasActive;
            UpdateContent();
        }
        StopUsing();    // Prevents Coroutine crash when hammering the use button
    }

    public void CloseDisplay()
    {
        if (canvasActive)
        {
            canvasActive = false;
            SwitchMenu("nothing");
            displayLight.enabled = canvasActive;
        }
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
        loadingCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        noteCanvas.SetActive(false);
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

        if (menu == "loading")
        {
            amLoading = true;
            loadingTimer = Time.time + 1;
            loadingCanvas.SetActive(true);
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

        if (amLoading)
        {
            if (Time.time > loadingTimer)
            {
                amLoading = false;
                SwitchMenu("menu");
            }
        }
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
        Debug.Log(evidenceID);
        activeScanID = evidenceID;
        viewText.text = foundEvidence[activeScanID].evidenceInformation;

        if (foundEvidence[activeScanID].amScanned == false)
        {
            SwitchMenu("scanning");
        }
        else
        {
            SwitchMenu("view");
        }
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
            if (scanningProgress >= 100)
            {
                scanningProgress = 100;
            }
            scanningSlider.size = scanningProgress / 100;
            scanningText.text = scanningProgress + "%";
            yield return new WaitForSeconds(1.0f);
        }

        foundEvidence[activeScanID].amScanned = true;
        if (foundEvidence[activeScanID].scannedEvidence != null)
        {
            managerGame.AddEvidence(foundEvidence[activeScanID].scannedEvidence);
            viewText.text = viewText.text + "\nFound Evidence:\n" + foundEvidence[activeScanID].scannedEvidence.evidenceName;
        }
        UpdateEvidence();

        SwitchMenu("view");
    }
}

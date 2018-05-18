using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class BleepBloop : VRTK_InteractableObject
{
    //
    private GameManager managerGame;
    public enum Mode { View, Accuse, Projector };
    internal Mode myMode = Mode.View;

    public static bool bleepBloopActive = false;
    public static bool evidenceChecked = false;
    public static bool clueBotSpawned = false;

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

    public GameObject loadingCanvas;
    public GameObject menuCanvas;
    public GameObject noteCanvas;
    public GameObject evidenceCanvas;
    public GameObject scanningCanvas;
    public GameObject viewCanvas;
    public GameObject suspectCanvas;
    public GameObject mapCanvas;
    public GameObject cluebotCanvas;

    Transform evidenceContainer;
    Transform suspectContainer;

    int activeScanID = -1;
    public Slider scanningSlider;

    Text viewText;

    GameObject cluebot;
    Button cluebotButton;
    Text cluebotText;

    Light displayLight;

    float scanningProgress = 0;
    int cluebotBattery = 100;

    public bool canvasActive = false;

    IEnumerator cluebotCoroutine;
    IEnumerator scanningRoutine;

    // Fallback protection
    string oldMenu = "";

    protected override void Awake()
    {
        base.Awake();

        GrabComponents();

        // Save Reference to Game Manager to summon
        managerGame.SetBleepBloop(this);

        TurnOff();
    }

    void GrabComponents()
    {
        // Grab Game Manager
        if (GameObject.Find("GameManager"))
        {
            managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        else
        {
            GameObject managerGamePrefab = Resources.Load("GameManager") as GameObject;
            GameObject managerGameInstant = Instantiate(managerGamePrefab);
            managerGameInstant.name = "GameManager";
            managerGame = managerGameInstant.GetComponent<GameManager>();
        }

        evidencePrefab = Resources.Load("UI/EvidenceButton") as GameObject;
        suspectPrefab = Resources.Load("UI/SuspectPanel") as GameObject;

        if (GetComponent<AudioSource>() != null)
        {
            audioPlayer = GetComponent<AudioSource>();
            audioPlayer.clip = Bleep;
        }
        

        playerHead = GameObject.FindGameObjectWithTag("Player").transform;
        displayHead = transform.GetChild(1);
        cluebot = transform.GetChild(2).gameObject;
        cluebot.SetActive(false);

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

        viewText = viewCanvas.transform.GetChild(1).GetChild(1).GetComponent<Text>();

        scanningSlider = scanningCanvas.transform.GetChild(1).GetChild(2).GetComponent<Slider>();
        cluebotButton = cluebotCanvas.transform.GetChild(1).GetComponent<Button>();
        cluebotText = cluebotButton.transform.GetChild(0).GetComponent<Text>();

        evidenceContainer = evidenceCanvas.transform.GetChild(1).GetChild(0).GetChild(0);
        suspectContainer = suspectCanvas.transform.GetChild(1).GetChild(0).GetChild(0);
    }

    public void UpdateContent()
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


        int buttonHeight = 480;
        foundEvidence = managerGame.GetNewEvidence();
        if (foundEvidence != null && foundEvidence.Count > 0)    // Only spawn if there is something to spawn
        {
            evidenceContent = new GameObject[foundEvidence.Count];
            for (int i = 0; i < evidenceContent.Length; i++)
            {
                // Spawn button
                evidenceContent[i] = Instantiate(evidencePrefab, evidenceContainer);
                evidenceContent[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-buttonHeight / 2)  + (i * -buttonHeight));

                // Populate Children
                if (foundEvidence[i] != null)
                {
                    evidenceContent[i].transform.GetChild(1).GetComponent<Text>().text = foundEvidence[i].evidenceName;
                    evidenceContent[i].transform.GetChild(2).GetComponent<Text>().text = foundEvidence[i].evidenceDescription;

                    // Am I already scanned?
                    if (!foundEvidence[i].amScanned)
                    {
                        evidenceContent[i].transform.GetChild(3).GetComponent<Text>().text = "Unscanned";
                    }
                    else
                    {
                        evidenceContent[i].transform.GetChild(3).GetComponent<Text>().text = foundEvidence[i].evidenceInformation;
                    }
                }

                // Delegate Work
                int myCount = i;
                evidenceContent[i].GetComponent<Button>().onClick.AddListener(delegate { UseEvidence(myCount); });
            }

            // Adjust ContainerSize
            RectTransform evidenceContainerRect = evidenceContainer.GetComponent<RectTransform>();
            float evidenceContainerWidth = evidenceContainerRect.sizeDelta.x;
            float evidenceContainerHeight = foundEvidence.Count * buttonHeight;
            evidenceContainerRect.sizeDelta = new Vector2(evidenceContainerWidth, evidenceContainerHeight);
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
                suspectContent[i] = Instantiate(suspectPrefab, suspectContainer);
                suspectContent[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -30 + (i * -60));

                // Populate Children
                suspectContent[i].transform.GetChild(1).GetComponent<Text>().text = foundSuspects[i].suspectName;
                suspectContent[i].transform.GetChild(2).GetComponent<Text>().text = foundSuspects[i].suspectDescription;

                string myEvidence = "";
                for (int x = 0; x < foundSuspects[i].suspectEvidence.Length; x++)
                {
                    for (int y = 0; y < foundEvidence.Count; y++)
                    {
                        if (foundSuspects[i].suspectEvidence[x] == foundEvidence[y])
                        {
                            myEvidence = myEvidence + foundSuspects[i].suspectEvidence[x].evidenceName + "\n";
                        }
                    }
                }

                suspectContent[i].transform.GetChild(3).GetComponent<Text>().text = myEvidence;
            }
        }
    }

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        bleepBloopActive = true;
        base.StartUsing(usingObject);
        
        if (!canvasActive)
        {
            OpenDisplay();
        }
        else
        {
            CloseDisplay();
        }
        StopUsing();    // Prevents Coroutine crash when hammering the use button
    }

    void OpenDisplay()
    {
        canvasActive = true;
        SwitchMenu("loading");
        displayHead.gameObject.SetActive(canvasActive);
        displayLight.enabled = canvasActive;
        UpdateContent();
    }

    public void CloseDisplay()
    {
        if (canvasActive)
        {
            canvasActive = false;
            SwitchMenu("nothing");
            displayLight.enabled = canvasActive;
            displayHead.gameObject.SetActive(canvasActive);
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
        if (!loadingCanvas || !menuCanvas || !noteCanvas || !evidenceCanvas || !scanningCanvas || !viewCanvas || !suspectCanvas || !mapCanvas || !cluebotCanvas)
        {
            GrabComponents();
        }

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
        // Fix for Unable to look at thing when selecting the menu button of the same menu
        if (oldMenu == menu)
        {
            return;
        }
        oldMenu = menu;

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
            StartCoroutine(LoadingCoroutine());
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
            evidenceChecked = true;
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
            clueBotSpawned = true;
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

        Vector3 spawnLocation = playerHead.position;
        //spawnLocation += -Vector3.forward * 2;
        //spawnLocation.y = playerHead.position.y - 0.68f;
        cluebot.transform.position = spawnLocation;
        cluebot.transform.parent = null;
        cluebot.SetActive(true);

        if (cluebotCoroutine != null)
        {
            StopCoroutine(cluebotCoroutine);
            cluebotCoroutine = null;
        }



        cluebotCoroutine = CluebotCoroutine();
        StartCoroutine(cluebotCoroutine);
    }

    public void UseEvidence(int evidenceID)
    {
        switch (myMode)
        {
            case Mode.View:
                CheckEvidence(evidenceID); break;
            case Mode.Accuse:
                managerGame.PassEvidenceConversation(evidenceID); break;
            case Mode.Projector:
                break;
            default:
                break;
        }
    }

    public void CheckEvidence(int evidenceID)
    {
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

    public void ChangeMode(Mode newMode)
    {
        myMode = newMode;

        if (myMode == Mode.View)
        {
            SwitchMenu("none");
        }
        else
        {
            SwitchMenu("evidence");
        }
    }

    IEnumerator LoadingCoroutine()
    {
        Slider loadingBar = loadingCanvas.transform.GetChild(1).GetChild(0).GetComponent<Slider>();
        loadingBar.value = 0;
        while(loadingBar.value < 1.0f)
        {
            loadingBar.value = loadingBar.value + 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        SwitchMenu("menu");
        StopCoroutine(LoadingCoroutine());
    }

    IEnumerator CluebotCoroutine()
    {
        while (cluebotBattery > 0)
        {
            cluebotBattery--;
            cluebotText.text = cluebotBattery + "%";
            yield return new WaitForSeconds(1.0f);
        }

        cluebot.SetActive(false);

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
        scanningSlider.value = scanningProgress / 100;

        while (scanningProgress < 100)
        {
            scanningProgress += 10;
            scanningSlider.value = scanningProgress / 100;
            yield return new WaitForSeconds(0.1f);
        }

        foundEvidence[activeScanID].amScanned = true;
        if (foundEvidence[activeScanID].scannedEvidence != null)
        {
            managerGame.AddEvidence(foundEvidence[activeScanID].scannedEvidence);
            viewText.text = viewText.text + "\n\nFound Evidence:\n" + foundEvidence[activeScanID].scannedEvidence.evidenceName;
        }
        UpdateContent();

        SwitchMenu("view");
    }
}

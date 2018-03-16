using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class Breakable : VRTK_InteractableObject
{
    [Header("Evidence", order = 1)]
    public Evidence evidence;

    // GameManager Reference
    private GameManager managerGame;

    private bool amAnalyzed;
    private int analyzePercentage = 0;
    private float analyzeCooldown = 0;
    private GameObject objectCanvas;
    private Text objectName;
    private Text objectText;

    private Vector3 resetPosition;
    private Quaternion resetRotation;

    private int shardCount;
    private Vector3[] shardPositions;
    private Quaternion[] shardRotations;

    private IEnumerator resetCoroutine;

    protected override void Awake()
    {
        base.Awake();
        SetReset();
        ResetObject();

        // Set up
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
        amAnalyzed = managerGame.CheckAddedEvidence(evidence);

        objectCanvas = transform.GetChild(1).gameObject;
        objectCanvas.SetActive(false);

        objectName = objectCanvas.transform.GetChild(0).GetComponent<Text>();
        objectName.text = evidence.evidenceName;

        objectText = objectCanvas.transform.GetChild(1).GetComponent<Text>();
        objectText.text = evidence.evidenceDescription;
    }

    protected override void Update()
    {
        base.Update();

        if (IsGrabbed() && Time.time > analyzeCooldown)
        {
            if (amAnalyzed)
            {
                objectName.text = evidence.evidenceName;
                objectText.text = evidence.evidenceDescription;
            }
            else
            {
                analyzePercentage = analyzePercentage + Random.Range(1, 25);
                if (analyzePercentage > 100.00f)
                {
                    amAnalyzed = true;
                    analyzePercentage = 100;
                    managerGame.AddEvidence(evidence);
                }

                objectName.text = "Analyzing...";
                objectText.text = analyzePercentage + "%";
            }

            if (!objectCanvas.activeSelf)
            {
                objectCanvas.SetActive(true);
            }

            analyzeCooldown = Time.time + 1;
        }
        
        if (!IsGrabbed() && objectCanvas.activeSelf)
        {
            objectCanvas.SetActive(false);
        }
    }

    // Apply the key text to the GUI of the button
    void SetReset()
    {
        resetPosition = transform.position;
        resetRotation = transform.rotation;

        shardCount = transform.GetChild(0).childCount;
        shardPositions = new Vector3[shardCount];
        shardRotations = new Quaternion[shardCount];
        for (int i = 0; i < shardCount; i++)
        {
            shardPositions[i] = transform.GetChild(0).GetChild(i).localPosition;
            shardRotations[i] = transform.GetChild(0).GetChild(i).localRotation;
        }
    }

    void ResetObject()
    {
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }

        transform.position = resetPosition;
        transform.rotation = resetRotation;
        for (int i = 0; i < shardCount; i++)
        {
            transform.GetChild(0).GetChild(i).localPosition = shardPositions[i];
            transform.GetChild(0).GetChild(i).localRotation = shardRotations[i];
        }
        GetComponent<MeshRenderer>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
        {
            GetComponent<AudioSource>().Play();
            GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(true);
            resetCoroutine = ResetTimer();
            StartCoroutine(resetCoroutine);
        }

    }

    public IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(3.0f);
        ResetObject();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class StandardObject : VRTK_InteractableObject
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

    private IEnumerator resetCoroutine;

    protected override void Awake()
    {
        base.Awake();
        SetReset();
        ResetObject();

        if (evidence)
        {
            // Set up
            managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
            amAnalyzed = managerGame.CheckAddedEvidence(evidence);
        }

        objectCanvas = transform.GetChild(0).gameObject;
        objectCanvas.SetActive(false);

        objectName = objectCanvas.transform.GetChild(0).GetComponent<Text>();
        objectText = objectCanvas.transform.GetChild(1).GetComponent<Text>();
    }

    protected override void Update()
    {
        base.Update();

        if (IsGrabbed() && Time.time > analyzeCooldown && evidence)
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
    }

    public override void Grabbed(VRTK_InteractGrab currentGrabbingObject = null)
    {
        base.Grabbed(currentGrabbingObject);

        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }
    }

    public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
    {
        base.Ungrabbed(previousGrabbingObject);

        resetCoroutine = ResetTimer();
        StartCoroutine(resetCoroutine);
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
    }

    public IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(3.0f);
        ResetObject();
    }
}

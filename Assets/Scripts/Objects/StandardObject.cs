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
    internal GameManager managerGame;

    internal Transform playerHead;
    internal Transform tooltipHead;

    internal bool amAnalyzed;
    internal int analyzePercentage = 0;
    internal float analyzeCooldown = 0;
    internal GameObject objectCanvas;
    internal Text objectName;
    internal Text objectText;

    internal Vector3 resetPosition;
    internal Quaternion resetRotation;

    internal IEnumerator resetCoroutine;

    protected override void Awake()
    {
        base.Awake();
        SetReset();
        ResetObject();

        // Set up
        managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
        amAnalyzed = managerGame.CheckAddedEvidence(evidence);

        playerHead = GameObject.FindGameObjectWithTag("Player").transform;
        tooltipHead = transform.GetChild(0);

        objectCanvas = tooltipHead.GetChild(0).gameObject;
        objectCanvas.SetActive(false);

        if (evidence)
        {
            objectName = objectCanvas.transform.GetChild(1).GetComponent<Text>();
            objectName.text = evidence.evidenceName;

            objectText = objectCanvas.transform.GetChild(2).GetComponent<Text>();
            objectText.text = evidence.evidenceDescription;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (evidence)
        {
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
    }

    // Apply the key text to the GUI of the button
    public virtual void SetReset()
    {
        resetPosition = transform.position;
        resetRotation = transform.rotation;
    }

    public override void Grabbed(VRTK_InteractGrab currentGrabbingObject = null)
    {
        base.Grabbed(currentGrabbingObject);

        // Reset Respawn
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }

        // Aim the Display to the player
        tooltipHead.LookAt(playerHead.position);
    }

    public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
    {
        base.Ungrabbed(previousGrabbingObject);

        resetCoroutine = ResetTimer();
        StartCoroutine(resetCoroutine);
    }

    public virtual void ResetObject()
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

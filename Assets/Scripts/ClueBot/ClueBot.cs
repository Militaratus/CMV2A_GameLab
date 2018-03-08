using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueBot : MonoBehaviour
{
    public GameObject[] detectedEvidence;
    private Vector3 startLocation;
    private Vector3 evidenceLocation;

    // Idle Height
    private float idleHeight = 0.68f;

    // Journey stats
    private int chosenEvidence;
    private float journeyStart;
    private float journeyLength;

    // Light
    private Light buttLight;

    internal Transform playerHead;
    internal Transform tooltipHead;
    internal Text objectName;
    internal Text objectText;

    enum BotBehavior { WAKE, IDLE, SCANNING, SCAN_SUCCES, SCAN_FAIL, MOVING, INDICATING };
    BotBehavior currentBehavior;
    IEnumerator currentBehaviorCoroutine;

    // Use this for initialization
    void Awake ()
    {
        playerHead = GameObject.FindGameObjectWithTag("Player").transform;
        tooltipHead = transform.GetChild(0);
        objectName = tooltipHead.GetChild(0).GetChild(1).GetComponent<Text>();
        objectName.text = gameObject.name;
        objectText = tooltipHead.GetChild(0).GetChild(2).GetComponent<Text>();

        buttLight = transform.GetChild(2).GetChild(0).GetComponent<Light>();
        buttLight.color = Color.clear;

        ChangeState(BotBehavior.WAKE);
    }

    private void OnEnable()
    {
        ChangeState(BotBehavior.WAKE);
    }

    void ChangeState(BotBehavior newBehavior)
    {
        
        if (currentBehaviorCoroutine != null)
        {
            StopCoroutine(currentBehaviorCoroutine);
            currentBehaviorCoroutine = null;
        }


        if (newBehavior == BotBehavior.WAKE)
        {
            currentBehavior = newBehavior;
            currentBehaviorCoroutine = WakingUp();
        }
        if (newBehavior == BotBehavior.IDLE)
        {
            currentBehavior = newBehavior;
            currentBehaviorCoroutine = Idleing();
        }
        if (newBehavior == BotBehavior.SCANNING)
        {
            
            if (currentBehavior == newBehavior) // Was I already scanning?
            {
                if (detectedEvidence.Length > 1)
                {
                    bool newEvidence = false;
                    for (int i = 0; i < detectedEvidence.Length; i++)
                    {
                        if (!detectedEvidence[i].GetComponent<StandardObject>().amAnalyzed)
                        {
                            newEvidence = true;
                            break;
                        }
                    }

                    if (newEvidence)
                    {
                        currentBehaviorCoroutine = ScanningSuccess();
                    }
                    else
                    {
                        currentBehaviorCoroutine = ScanningFail();
                    }
                }
                else
                {
                    currentBehaviorCoroutine = ScanningFail();
                }
            }
            else // If I wasn't start now!
            {
                currentBehavior = newBehavior;
                currentBehaviorCoroutine = Scanning();

                detectedEvidence = GameObject.FindGameObjectsWithTag("Evidence");
            }
        }

        if (newBehavior == BotBehavior.MOVING)
        {
            bool continueMoving = false;
            // Am I already on the way, no need to recalculate destination, unless the object is already scanned
            if (currentBehavior == BotBehavior.MOVING)
            {
                if (!detectedEvidence[chosenEvidence].GetComponent<StandardObject>().amAnalyzed)
                {
                    continueMoving = true;
                    currentBehavior = newBehavior;
                    currentBehaviorCoroutine = Moving();
                }
            }

            if (!continueMoving)
            {
                // Can't move if no evidence is found
                if (detectedEvidence.Length < 1)
                {
                    currentBehavior = BotBehavior.IDLE;
                    currentBehaviorCoroutine = Idleing();
                }
                else
                {
                    bool newEvidence = false;
                    for (int i = 0; i < detectedEvidence.Length; i++)
                    {
                        if (!detectedEvidence[i].GetComponent<StandardObject>().amAnalyzed)
                        {
                            chosenEvidence = i;
                            evidenceLocation = detectedEvidence[i].transform.position;
                            evidenceLocation.y += idleHeight;
                            newEvidence = true;
                            break;
                        }
                    }

                    if (newEvidence)
                    {
                        currentBehavior = newBehavior;
                        currentBehaviorCoroutine = Moving();

                        // Journey
                        startLocation = transform.position;
                        journeyStart = Time.time;
                        journeyLength = Vector3.Distance(startLocation, evidenceLocation) / 4;
                    }
                    else
                    {
                        currentBehavior = BotBehavior.IDLE;
                        currentBehaviorCoroutine = Idleing();
                    }
                }
            }
        }

        if (newBehavior == BotBehavior.INDICATING)
        {
            if (!detectedEvidence[chosenEvidence].GetComponent<StandardObject>().amAnalyzed && !detectedEvidence[chosenEvidence].GetComponent<StandardObject>().IsGrabbed())
            {
                currentBehavior = newBehavior;
                currentBehaviorCoroutine = Indicating();
            }
            else
            {
                currentBehavior = BotBehavior.IDLE;
                currentBehaviorCoroutine = Idleing();
            }
        }

        if (currentBehaviorCoroutine != null)
        {
            currentBehavior = newBehavior;
            StartCoroutine(currentBehaviorCoroutine);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Aim the Display to the player
        tooltipHead.LookAt(playerHead.position);

        if (currentBehavior == BotBehavior.MOVING)
        {
            float distCovered = (Time.time - journeyStart) * 1;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startLocation, evidenceLocation, fracJourney);
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        // Ground Check
        Vector3 floorDirection = transform.TransformDirection(Vector3.down);
        if (Physics.Raycast(transform.position, floorDirection, out hit, idleHeight))
        {
            transform.position += Vector3.up * (Time.deltaTime / 2);

            if (currentBehavior == BotBehavior.MOVING)
            {
                if (hit.transform.gameObject == detectedEvidence[chosenEvidence])
                {
                    ChangeState(BotBehavior.INDICATING);
                }
            }
        }

        
    }

    IEnumerator WakingUp()
    {
        objectText.text = "(Z_Z)";
        yield return new WaitForSeconds(3.0f);
        objectText.text = "(^_^)7";
        yield return new WaitForSeconds(2.0f);
        ChangeState(BotBehavior.IDLE);
    }

    IEnumerator Idleing()
    {
        objectText.text = ".";
        yield return new WaitForSeconds(1.0f);
        objectText.text = "..";
        yield return new WaitForSeconds(1.0f);
        objectText.text = "...";
        yield return new WaitForSeconds(1.0f);
        ChangeState(BotBehavior.SCANNING);
    }

    IEnumerator Scanning()
    {
        objectText.text = "(=O_o)7";
        yield return new WaitForSeconds(1.0f);
        objectText.text = "(o_O=)";
        yield return new WaitForSeconds(1.0f);
        ChangeState(BotBehavior.SCANNING);
    }

    IEnumerator ScanningSuccess()
    {
        objectText.text = "\\(^_^)/";
        yield return new WaitForSeconds(2.0f);
        ChangeState(BotBehavior.MOVING);
    }

    IEnumerator ScanningFail()
    {
        objectText.text = "(T_T)";
        yield return new WaitForSeconds(2.0f);
        ChangeState(BotBehavior.IDLE);
    }

    IEnumerator Moving()
    {
        objectText.text = "FOLLOW!";
        yield return new WaitForSeconds(1.0f);
        ChangeState(BotBehavior.MOVING);
    }

    IEnumerator Indicating()
    {
        objectText.text = "ლ(ಠ益ಠლ";
        buttLight.color = Color.red;
        yield return new WaitForSeconds(1.0f);
        buttLight.color = Color.blue;
        yield return new WaitForSeconds(1.0f);
        buttLight.color = Color.clear;
        yield return new WaitForSeconds(1.0f);
        ChangeState(BotBehavior.INDICATING);
    }
}

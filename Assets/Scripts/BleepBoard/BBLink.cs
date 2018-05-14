using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class BBLink : MonoBehaviour
{
    public bool inUse = false;
    public Evidence myEvidence;

    public Transform lineTarget;
    private LineRenderer lineRenderer;

    public GameObject myDisplay;

	// Use this for initialization
	void Start ()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position); // Refer to itself

        // Do I have a DisplayPanel?
        if (transform.GetChild(0).name == "DetailsPanel")
        {
            myDisplay = transform.GetChild(0).gameObject;
            myDisplay.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*
		if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        if (lineTarget != null)
        {
            //lineRenderer.SetPosition(0, transform.position);
            //lineRenderer.SetPosition(1, lineTarget.position);

            if (lineRenderer.material == null)
            {
                Shader shader = new Shader();
                Material myMaterial = new Material(shader);
                myMaterial.color = Color.green;
                lineRenderer.material = myMaterial;
            }

            lineRenderer.material.color = Color.green;
        }*/
    }

    public void AddLink(Transform newLink)
    {
        int oldCount = lineRenderer.positionCount;

        // Add links
        lineRenderer.positionCount = oldCount + 2;

        // Draw line
        lineRenderer.SetPosition(oldCount, newLink.position);       // Refer to target
        lineRenderer.SetPosition(oldCount + 1, transform.position); // Reger to itself

        
    }

    public bool HasLink(Transform curLink)
    {
        bool confirmLink = false;

        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            if (lineRenderer.GetPosition(i) == curLink.position)
            {
                confirmLink = true;
                DeleteLink(i);
                break;
            }
        }

        return confirmLink;
    }

    // Avoid messy array cleaning by replacing the value with the main position
    public void DeleteLink(int linkID)
    {
        lineRenderer.SetPosition(linkID, transform.position);
    }

    public void ToggleDisplay()
    {
        if (myDisplay.activeSelf)
        {
            myDisplay.SetActive(false);
        }
        else
        {
            myDisplay.SetActive(true);
        }
        
    }

    public void PopulateLink(Evidence newEvidence)
    {
        myEvidence = newEvidence;
        if (myDisplay != null)
        {
            Text myButtonText = transform.GetChild(2).GetComponent<Text>();
            myButtonText.text = myEvidence.evidenceDescription;

            Text myDisplayText = myDisplay.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            myDisplayText.text = myEvidence.evidenceInformation;
        }
        else
        {
            Text myButtonText = transform.GetChild(1).GetComponent<Text>();
            myButtonText.text = myEvidence.evidenceDescription;
        }
        inUse = true;
    }

    public void ResetButton()
    {
        if (myDisplay != null)
        {
            myDisplay.SetActive(false);

            Text myButtonText = transform.GetChild(2).GetComponent<Text>();
            myButtonText.text = "Empty Slot";
        }
        else
        {
            Text myButtonText = transform.GetChild(1).GetComponent<Text>();
            myButtonText.text = "Empty Slot";
        }

        myEvidence = null;
        lineRenderer.positionCount = 0;

        Button myButton = GetComponent<Button>();
        myButton.interactable = false;

        inUse = false;
    }
}

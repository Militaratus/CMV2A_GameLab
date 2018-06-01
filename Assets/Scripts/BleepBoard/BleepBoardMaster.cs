using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class BleepBoardMaster : MonoBehaviour
{

    public static bool placedClue = false;
    public static bool connectedClue = false;
    public static bool deletedClue = false;

    public BBLink sLinkA;
    public BBLink sLinkB;
    public Transform linkA;
    public Transform linkB;

    public Transform[] evidenceSlots;
    public Transform[] locationSlots;
    public Transform[] suspectSlots;

    public List<GameObject> evidenceItems = new List<GameObject>();

    private GameManager managerGame;
    private Transform evidenceContainer;
    private Evidence addEvidence;
    private int curID = 0;

    // Use this for initialization
    void Awake ()
    {
        // Evidence
        Transform slotsPanel = transform.GetChild(0).GetChild(4);
        int myChildCount = slotsPanel.childCount;
        evidenceSlots = new Transform[myChildCount];
        for (int i = 0; i < myChildCount; i++)
        {
            evidenceSlots[i] = slotsPanel.GetChild(i);
        }

        //Locations
        slotsPanel = transform.GetChild(0).GetChild(0);
        myChildCount = slotsPanel.childCount;
        locationSlots = new Transform[myChildCount];
        for (int i = 0; i < myChildCount; i++)
        {
            locationSlots[i] = slotsPanel.GetChild(i);
        }

        // Suspects
        slotsPanel = transform.GetChild(0).GetChild(1);
        myChildCount = slotsPanel.childCount;
        suspectSlots = new Transform[myChildCount];
        for (int i = 0; i < myChildCount; i++)
        {
            suspectSlots[i] = slotsPanel.GetChild(i);
        }

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

        evidenceContainer = transform.GetChild(0).GetChild(1);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (evidenceItems.Count < managerGame.GetEvidence().Count)
        {
            PopulateEvidence();
        }
	}

    void PopulateEvidence()
    {
        List<Evidence> mgEvidence = managerGame.GetEvidence();
        GameObject evidencePrefab = Resources.Load("UI/AddEvidenceButton") as GameObject;

        for (int i = 0; i < mgEvidence.Count; i++)
        {
            if ((i + 1) > evidenceItems.Count)
            {
                // Spawn button
                GameObject newButton = Instantiate(evidencePrefab, evidenceContainer) as GameObject;
                newButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -0 + (i * -60));
                newButton.GetComponent<BBEvidence>().AddEvidence(mgEvidence[i], i);

                evidenceItems.Add(newButton);
            }
            /*
            if (evidenceItems[i].GetComponent<BBEvidence>().myEvidence == null)
            {
                
            }*/
        }
    }

    public void PrepareLink(Transform linkObject)
    {
        if (!linkObject.GetComponent<BBLink>().inUse)
        {
            // Evidence Used Analytics
            Analytics.CustomEvent("BoardAddEvidence", new Dictionary<string, object>
            {
                { "EvidenceName", addEvidence.evidenceName }
            });

            linkObject.GetComponent<BBLink>().PopulateLink(addEvidence);
            placedClue = true;
            UnprepareNewEvidence();
            //CleanUp();
            return;
        }


        if (linkA == null)
        {
            linkA = linkObject;
            sLinkA = linkObject.GetComponent<BBLink>();

            if (sLinkA.myDisplay != null)
            {
                sLinkA.ToggleDisplay();
            }
        }
        else
        {
            linkB = linkObject;
            sLinkB = linkObject.GetComponent<BBLink>();

            if (linkA == linkB)
            {
                // I clicked myself, nothing to see here.
                CleanUp();
            }
        }

        if (linkA != null && linkB != null)
        {
            // If there is no link, add one. If there is one, it is removed.
            if (!sLinkA.HasLink(linkB) && !sLinkB.HasLink(linkA))
            {
                // Is the evidence Linked?
                bool linkedEvidence = false;
                Evidence eLinkA = sLinkA.myEvidence;
                Evidence eLinkB = sLinkB.myEvidence;

                // Evidence Linked Analytics
                Analytics.CustomEvent("BoardLinkAttempt", new Dictionary<string, object>
                {
                    { "EvidenceA", eLinkA.evidenceName },
                    { "EvidenceB", eLinkB.evidenceName }
                });

                //Evidence linkEvidence = newLink.GetComponent<BBLink>().myEvidence;
                for (int i = 0; i < eLinkA.linkedEvidence.Count; i++)
                {
                    if (eLinkA.linkedEvidence[i] == eLinkB)
                    {
                        linkedEvidence = true;
                        break;
                    }
                }

                if (linkedEvidence)
                {
                    // Evidence Linked Analytics
                    Analytics.CustomEvent("BoardLinkMade", new Dictionary<string, object>
                    {
                        { "EvidenceA", eLinkA.evidenceName },
                        { "EvidenceB", eLinkB.evidenceName }
                    });

                    connectedClue = true;
                    // Great success
                    sLinkA.AddLink(linkB);
                }
            }

            CleanUp();
        }
    }

    void CleanUp()
    {
        if (sLinkA.myDisplay != null)
        {
            sLinkA.ToggleDisplay();
        }

        linkA = null;
        sLinkA = null;
        linkB = null;
        sLinkB = null;
    }

    public void DeleteNode(Transform linkObject)
    {
        // Clean Evidence
        for (int i = 0; i < evidenceSlots.Length; i++)
        {
            deletedClue = true;
            evidenceSlots[i].GetComponent<BBLink>().HasLink(linkObject);
        }

        // Clean Locations
        for (int i = 0; i < locationSlots.Length; i++)
        {
            if (locationSlots[i].GetComponent<BBLink>())
            {
                locationSlots[i].GetComponent<BBLink>().HasLink(linkObject);
            }
            
        }

        // Clean Suspects
        for (int i = 0; i < suspectSlots.Length; i++)
        {
            if (suspectSlots[i].GetComponent<BBLink>())
            {
                suspectSlots[i].GetComponent<BBLink>().HasLink(linkObject);
            }
        }

        linkObject.GetComponent<BBLink>().ResetButton();
        linkA = null;
        sLinkA = null;
        linkB = null;
        sLinkB = null;
    }

    public void PrepareNewEvidence(Evidence newEvidence, int newID)
    {
        addEvidence = newEvidence;
        evidenceItems[curID].GetComponent<Image>().color = Color.white;
        evidenceItems[newID].GetComponent<Image>().color = Color.red;
        curID = newID;

        for (int i = 0; i < evidenceSlots.Length; i++)
        {
            Button curEvidenceSlot = evidenceSlots[i].GetComponent<Button>();
            curEvidenceSlot.interactable = true;
        }
    }

    void UnprepareNewEvidence()
    {
        evidenceItems[curID].GetComponent<Image>().color = Color.white;
        for (int i = 0; i < evidenceSlots.Length; i++)
        {
            if (!evidenceSlots[i].GetComponent<BBLink>().inUse)
            {
                Button curEvidenceSlot = evidenceSlots[i].GetComponent<Button>();
                curEvidenceSlot.interactable = false;
            }
        }
    }
}

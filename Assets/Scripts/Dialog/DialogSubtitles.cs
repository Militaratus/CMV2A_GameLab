using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSubtitles : MonoBehaviour
{
    public GameObject subtitlePanel;
    public Text subtitleText;

    public GameObject evidencePanel;
    public Text evidenceText;

    public GameObject suspectPanel;
    public Text suspectText;

    // Use this for initialization
    void Awake ()
    {
        subtitlePanel = transform.GetChild(1).gameObject;
        subtitleText = subtitlePanel.transform.GetChild(0).GetComponent<Text>();

        evidencePanel = transform.GetChild(2).gameObject;
        evidenceText = evidencePanel.transform.GetChild(0).GetComponent<Text>();

        suspectPanel = transform.GetChild(3).gameObject;
        suspectText = suspectPanel.transform.GetChild(0).GetComponent<Text>();

        HideText();
    }

    public void UpdateText(string subtitle, string name = "")
    {
        subtitlePanel.SetActive(true);

        string newSubtitle = "<color=\"red\">" + name + "</color>: " + subtitle;
        subtitleText.text = newSubtitle;
    }

    public void HideText()
    {
        if (subtitlePanel == null)
        {
            subtitlePanel = transform.GetChild(1).gameObject;
        }

        subtitlePanel.SetActive(false);
        evidencePanel.SetActive(false);
        suspectPanel.SetActive(false);
    }

    public void UpdateEvidence(string evidence)
    {
        evidencePanel.SetActive(true);

        string newEvidence = "<color=\"red\">New Evidence:</color>: " + evidence;
        evidenceText.text = newEvidence;
    }

    public void HideEvidence()
    {
        if (evidencePanel == null)
        {
            evidencePanel = transform.GetChild(2).gameObject;
        }
        evidencePanel.SetActive(false);
    }

    public void UpdateSuspect(string suspect)
    {
        suspectPanel.SetActive(true);

        string newSuspect = "<color=\"red\">New Suspect:</color>: " + suspect;
        suspectText.text = newSuspect;
    }

    public void HideSuspect()
    {
        if (suspectPanel == null)
        {
            suspectPanel = transform.GetChild(3).gameObject;
        }
        suspectPanel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSubtitles : MonoBehaviour
{
    public GameObject subtitlePanel;
    public Text subtitleText;

	// Use this for initialization
	void Awake ()
    {
        subtitlePanel = transform.GetChild(1).gameObject;
        subtitleText = subtitlePanel.transform.GetChild(0).GetComponent<Text>();
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
    }
}

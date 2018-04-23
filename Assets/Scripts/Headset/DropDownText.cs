using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownText : MonoBehaviour
{

    public Dropdown dropdown1;
    public Text selectEvidence1;
    public Dropdown dropdown2;
    public Text selectEvidence2;
    public Dropdown dropdown3;
    public Text selectEvidence3;


    GameManager managerGame;

    // Use this for initialization
    void Start()
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
        PopulateList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PopulateList()
    {

        List<string> dropdownOptions = new List<string>();
        for (int i = 0; i < managerGame.gatheredEvidence.Count; i++)
        {
            dropdownOptions.Add(managerGame.gatheredEvidence[i].evidenceName);
        }
        dropdown1.AddOptions(dropdownOptions);
        dropdown2.AddOptions(dropdownOptions);
        dropdown3.AddOptions(dropdownOptions);
    }
}

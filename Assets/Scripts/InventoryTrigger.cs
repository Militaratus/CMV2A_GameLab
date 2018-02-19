using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class InventoryTrigger : MonoBehaviour
{

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    { 
        if (other.tag == "Item")
        {
            rb = other.GetComponent<Rigidbody>();
            Debug.Log("Inventory");
            rb.transform.SetParent(this.transform);
            rb.transform.localPosition = Vector3.zero;
        }
    }
}

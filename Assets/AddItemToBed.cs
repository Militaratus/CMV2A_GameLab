using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class AddItemToBed : MonoBehaviour
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
        rb = other.GetComponent<Rigidbody>();
        rb.transform.SetParent(this.transform);
    }
}

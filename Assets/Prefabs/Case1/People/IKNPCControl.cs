using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKNPCControl : MonoBehaviour
{
    protected Animator animator;

    public bool ikActive = false;
    public Transform lookObj = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {
                if (lookObj == null)
                {
                    lookObj = GameObject.FindGameObjectWithTag("Player").transform;
                }

                // Set the look target position, if one has been assigned
                if (lookObj != null)
                {
                    // Face the direction of the camera
                    //float yRotation = lookObj.eulerAngles.y;
                    //transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);

                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }
            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetLookAtWeight(0);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using VRTK;

public class StandardObject : VRTK_InteractableObject
{
    public static bool PickUPActive = true;

    [Header("Salty Puppies", order = 1)]
    public bool canRespawn;
    public Evidence evidence;
    internal bool amAnalyzed;

    // GameManager Reference
    internal GameManager managerGame;

    // Respawn Variables
    internal Vector3 resetPosition;
    internal Quaternion resetRotation;
    internal IEnumerator resetCoroutine;

    // Gravity Variables
    private float cooldownSwitchGravity;

    protected override void Awake()
    {
        base.Awake();

        // Only trigger on respawnable objects
        if (canRespawn)
        {
            SetReset();
        }

        // Only trigger when evidence was added
        if (AmEvidence())
        {
            // Set up
            if (GameObject.Find("GameManager"))
            {
                managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
            else
            {
                Debug.LogError("ERROR: Game Manager was not found.");
                GameObject managerGamePrefab = Resources.Load("GameManager") as GameObject;
                GameObject managerGameInstant = Instantiate(managerGamePrefab);
                managerGameInstant.name = "GameManager";
                managerGame = managerGameInstant.GetComponent<GameManager>();
            }

            amAnalyzed = false;
            gameObject.tag = "Evidence";
        }

        // Disable Gravity on spawn to allow artists to place what they want, where they want
        UseGravity(false);

        // Allows item collision be at the model level and not the VRTK Usable Area
        gameObject.layer = 8;

        // Do we need to take care of anything else?
        ExtraAwake();
    }

    // An overridable class just in case for inheritent classes 
    internal virtual void ExtraAwake()
    {

    }

    // Apply the key text to the GUI of the button
    internal virtual void SetReset()
    {
        resetPosition = transform.position;
        resetRotation = transform.rotation;
    }

    public override void Grabbed(VRTK_InteractGrab currentGrabbingObject = null)
    {
        base.Grabbed(currentGrabbingObject);

        // Reset Respawn
        if (canRespawn)
        {
            if (resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
                resetCoroutine = null;
            }
        }

        if (AmEvidence())
        {
            amAnalyzed = true;
            managerGame.AddEvidence(evidence);
        }
    }

    public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
    {
        // Enable Gravity on release, allow emergent gameplay.
        UseGravity();

        base.Ungrabbed(previousGrabbingObject);

        PickUPActive = false;

        if (canRespawn)
        {
            resetCoroutine = ResetTimer();
            StartCoroutine(resetCoroutine);
        }
    }

    bool AmEvidence()
    {
        bool bEvidence = false;
        if (evidence != null)
        {
            bEvidence = true;
        }
        return bEvidence;
    }

    internal void UseGravity(bool bUseGravity = true)
    {
        if (cooldownSwitchGravity > Time.time)
        {
            return;
        }

        if (bUseGravity == true)
        {
            interactableRigidbody.constraints = RigidbodyConstraints.None;
        }
        else
        {
            interactableRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        cooldownSwitchGravity = Time.time + 0.1f;
    }

    internal virtual void ResetObject()
    {
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }

        transform.position = resetPosition;
        transform.rotation = resetRotation;
    }

    internal IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(30.0f);
        ResetObject();
    }
}

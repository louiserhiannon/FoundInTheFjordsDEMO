using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementControls : MonoBehaviour
{
    public static MovementControls MC;
    //private GameObject xRRig;

    private void Awake()
    {
        //xRRig = this.gameObject;
        MC = this;
    }
    public void ActivateMovementControls()
    {
        if (TryGetComponent<LocomotionController_General>(out LocomotionController_General general))
        {
            general.enabled = true;
        }
        else if (TryGetComponent<LocomotionController_Orientation>(out LocomotionController_Orientation orientation))
        {
            orientation.enabled = true;
        }

        //if (GetComponentInChildren<ActionBasedSnapTurnProvider>() != null)
        //{
        //    GetComponentInChildren<ActionBasedSnapTurnProvider>().enabled = true;
        //}
    }

    public void DeactivateMovementControls()
    {
        if (TryGetComponent<LocomotionController_General>(out LocomotionController_General general))
        {
            Debug.Log("general locomotion controller present");
            general.enabled = false;
        }
        else if (TryGetComponent<LocomotionController_Orientation>(out LocomotionController_Orientation orientation))
        {
            Debug.Log("general locomotion controller not present; orientation controller present");
            orientation.enabled = false;
        }
        else
        {
            Debug.Log("no locomotion controller present");
        }
            

        //if (GetComponentInChildren<ActionBasedSnapTurnProvider>() != null)
        //{
        //    GetComponentInChildren<ActionBasedSnapTurnProvider>().enabled = false;
        //}
        
    }

    public void ActivateEatControls()
    {
        if (TryGetComponent<EatingControllerDEMO>(out EatingControllerDEMO eat))
        {
            eat.enabled = true;
            eat.targetActive = true;
        }
        
    }

    public void DeActivateEatControls()
    {
        if (TryGetComponent<EatingControllerDEMO>(out EatingControllerDEMO eat))
        {
            eat.enabled = false;
             eat.targetActive = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateControlsDEMO : MonoBehaviour
{
    public static ActivateControlsDEMO AC;

    private void Awake()
    {
        AC = this;
    }

    public void ActivateMovementControls()
    {
        if (TryGetComponent<LocomotionControllerDEMO>(out LocomotionControllerDEMO locomotion))
        {
            locomotion.enabled = true;
        }

        else
        {
            Debug.Log("no locomotion controller present");
        }
    }

    public void DeActivateMovementControls()
    {
        if (TryGetComponent<LocomotionControllerDEMO>(out LocomotionControllerDEMO locomotion))
        {
            locomotion.enabled = false;
        }

        else
        {
            Debug.Log("no locomotion controller present");
        }
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

    public void ActivateTailslapControls()
    {
        if (TryGetComponent<TailslapControllerDEMO>(out TailslapControllerDEMO slap))
        {
            slap.enabled = true;
            slap.tailChargeable = true;
            //add other things if necessary
        }

    }

    public void DeActivateTailslapControls()
    {
        if (TryGetComponent<TailslapControllerDEMO>(out TailslapControllerDEMO slap))
        {
            slap.tailChargeable = false;
            slap.enabled = false;
            //add other things if necessary
        }
    }


}



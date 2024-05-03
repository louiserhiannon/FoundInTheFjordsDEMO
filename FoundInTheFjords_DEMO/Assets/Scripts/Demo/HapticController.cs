using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticController : MonoBehaviour
{
    [SerializeField]
    private ActionBasedController controller;


    void Awake()
    {
        controller = GetComponent<ActionBasedController>();
    }

    public void ActivateHaptic(float amplitude, float duration)
    {


        if (controller != null)
        {
            controller.SendHapticImpulse(amplitude, duration);
        }

        Debug.Log("yes, I definitely should be shaking");
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CameraPosition : MonoBehaviour
{
    private XROrigin origin;
    private float cameraHeightSeated = 1.1f;
    private float cameraHeightStanding = 1.5f;

    void Start()
    {
        origin = GetComponent<XROrigin>();
        if (PlayerPosition.isStanding)
        {
            origin.CameraYOffset = cameraHeightStanding;
        }
        else
        {
            origin.CameraYOffset = cameraHeightSeated;
        }

    }

    
}

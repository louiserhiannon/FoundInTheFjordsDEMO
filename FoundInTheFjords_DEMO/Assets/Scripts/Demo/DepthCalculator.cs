using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthCalculator : MonoBehaviour
{
    public Transform headset; //Transform component of maincamera Game object
    public float headsetDepth; //y-coordinate (height) of camera in world space
    public float headsetDepthCorrected; // corrected depth that takes into account that ocean surface is at y = 2 (due to sky box)

    void Update()
    {
        if (headset != null)
        {
            headsetDepth = headset.position.y;
            headsetDepthCorrected = headsetDepth -2;
        }
    }
}

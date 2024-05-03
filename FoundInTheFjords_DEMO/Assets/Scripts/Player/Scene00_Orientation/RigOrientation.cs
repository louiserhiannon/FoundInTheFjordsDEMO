using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigOrientation : MonoBehaviour
{
    //public Transform headset; //Transform component of maincamera Game object
    //[SerializeField]
    //protected float headsetDepth; //y-coordinate (height) of camera in world space
    //[SerializeField]
    //protected float headsetDepthCorrected;// corrected depth that takes into account that ocean surface is at y = 2 (due to sky box)
    //private float rotationAbove = 180f;
    //private float rotationBelow = 90f;
    private bool underwater = true;
    public Transform headset;
    public DepthCalculator depthCalculator;

    // Update is called once per frame
    void Update()
    {
        //headsetDepth = headset.position.y;
        //headsetDepthCorrected = headsetDepth - 2f;

        //when the camera is above the water surface
        if (depthCalculator.headsetDepthCorrected >= 0)
        {
            RotateAboveWater();
        }
        else
        {
            RotateBelowWater();
        }
    }

    private void RotateAboveWater()
    {
        if (underwater)
        {
            float rotationX = headset.localEulerAngles.x + 90;
            headset.localEulerAngles = new Vector3(rotationX, headset.localEulerAngles.y, headset.localEulerAngles.z);
            underwater = false;
        }
    }

    private void RotateBelowWater()
    {
        if (!underwater)
        {
            float rotationX = headset.localEulerAngles.x - 90;
            headset.localEulerAngles = new Vector3(rotationX, headset.localEulerAngles.y, headset.localEulerAngles.z);
            underwater = true;
        }
    }
}

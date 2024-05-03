using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FogManager : MonoBehaviour
    //Controls the density and appearance of fog in renderer so it is deactivated above water and increases with depth underwater
{
    //public Transform headset; //Transform component of maincamera Game object
    //[SerializeField]
    //protected float headsetDepth; //y-coordinate (height) of camera in world space
    //[SerializeField]
    public DepthCalculator depthCalculator;
    //protected float fogFactor; // normalized value (between 0 and 1) used to calculate fog density at a given depth
    [SerializeField]
    //protected float maxFogDepth; // depth (negative height) at which fog density reaches maximum value
    //[SerializeField]
    //protected float headsetDepthCorrected; // corrected depth that takes into account that ocean surface is at y = 2 (due to sky box)
    public float maxFogDensity; // max fog density value (occurs at max fog depth and beyond
    //public float minFogDensity; // minimum fog density value (occurs at water surface

    public GameObject underwaterDistortion;
    private MeshRenderer underwaterDistortionRenderer;
    private Color underwaterDistortionColor;
    //public float minAlpha;
    public float maxAlpha;

    void Start()
    {
        //maxFogDepth = -7.0f;
        //maxFogDensity = 0.02f;
        //minFogDensity = 0.02f;
        underwaterDistortionRenderer = underwaterDistortion.GetComponent<MeshRenderer>();

    }

    public virtual void Update()
    {
        //headsetDepth = headset.position.y;
        //headsetDepthCorrected = headsetDepth - 2f;

        //when the camera is above the water surface
        if(depthCalculator != null)
        {
            if (depthCalculator.headsetDepthCorrected >= 0)
            {
                //Switch off fog
                RenderSettings.fog = false;
                //turn down light
                RenderSettings.ambientIntensity = 2f;
                SwitchOffDistortion();
            }
            else
            {
                //switch on fog using exponential squared setting
                RenderSettings.fog = true;
                RenderSettings.fogMode = FogMode.ExponentialSquared;
                RenderSettings.ambientIntensity = 3f;
                RenderSettings.fogDensity = maxFogDensity;


                ////when the rig is shallower than the max fog depth (note that depth is a negative value so shallower depths correspond to larger [less negative] numbers)
                //if (depthCalculator.headsetDepth > maxFogDepth)
                //{

                //    //the fog density is a linear interpolation from min to max based on depth
                //    fogFactor = depthCalculator.headsetDepthCorrected / maxFogDepth;
                //    RenderSettings.fogDensity = minFogDensity + fogFactor * (maxFogDensity - minFogDensity);
                //}

                //else
                //{
                //    //Once below the max depth, fog is set to the max density.
                //    RenderSettings.fogDensity = maxFogDensity;
                //}

                ControlDistortion();
            }
        }
        
        

    }

    private void ControlDistortion()
    {
        //activates tinted blue filter in front of camera
        underwaterDistortion.SetActive(true);
        //controls alpha of filter based on depth
        underwaterDistortionColor = underwaterDistortionRenderer.material.color;
        underwaterDistortionColor.a = maxAlpha;

        //if (depthCalculator.headsetDepth > maxFogDepth)
        //{

            
        //    underwaterDistortionColor.a = minAlpha + fogFactor * (maxAlpha - minAlpha);
            
        //}

        //else
        //{
        //    underwaterDistortionColor.a = maxAlpha;
        //}

        underwaterDistortionRenderer.material.color = underwaterDistortionColor;

        
        

    }

    private void SwitchOffDistortion()
    {
        //switches of filter when camera goes above surface
        underwaterDistortion.SetActive(false);
    }
}

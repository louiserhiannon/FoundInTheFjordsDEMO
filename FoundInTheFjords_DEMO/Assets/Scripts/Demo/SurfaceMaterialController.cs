using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SurfaceMaterialController : MonoBehaviour
{
    public DepthCalculator depthCalculator;
    public Material transparentWaterSurface;
    public Material opaqueWaterSurface;
    private MeshRenderer surfaceRenderer;
    private Color surfaceColor;
    private bool changeToTransparent = false;
    private bool changeToOpaque = true;
    private float minAlpha;
    private float maxAlpha;
    private float minDepth;
    private float maxDepth;
    // Start is called before the first frame update
    void Start()
    {
        surfaceRenderer = GetComponent<MeshRenderer>();
        surfaceRenderer.materials[0] = transparentWaterSurface;
        minAlpha = 85f / 255f;
        maxAlpha = 1;
        surfaceColor = surfaceRenderer.materials[0].color;
        surfaceColor.a = maxAlpha;
        surfaceRenderer.materials[0].color = surfaceColor;
        minDepth = -3f;
        maxDepth = -12f;

    }

    // Update is called once per frame
    void Update()
    {
        if(depthCalculator.headsetDepthCorrected >= 0)
        {
            if(changeToOpaque)
            {
                Material[] materials = surfaceRenderer.materials;
                materials[0] = opaqueWaterSurface;
                surfaceRenderer.materials = materials;
                changeToOpaque = false;
                changeToTransparent = true;
            }
        }
        else
        {
            if (changeToTransparent)
            {
                Material[] materials = surfaceRenderer.materials;
                materials[0] = transparentWaterSurface;
                surfaceRenderer.materials = materials;
                changeToOpaque = true;
                changeToTransparent = false;
            }

            if (depthCalculator.headsetDepthCorrected <= minDepth && depthCalculator.headsetDepthCorrected >= maxDepth)
            {
                AdjustTransparency();
            }
        }

        
    }
    
    private void AdjustTransparency()
    {
        float transparencyFactor = Mathf.Clamp((depthCalculator.headsetDepthCorrected - minDepth) / (maxDepth - minDepth), 0, 1);
        surfaceColor.a = maxAlpha - (1 - transparencyFactor) * (maxAlpha - minAlpha);
        surfaceRenderer.materials[0].color = surfaceColor;
        //Debug.Log(surfaceColor.a);
    }
}

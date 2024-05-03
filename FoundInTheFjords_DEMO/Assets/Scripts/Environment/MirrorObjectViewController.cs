using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorObjectViewController : MonoBehaviour
{
    public List<GameObject> mirrorObjects;
    //public GameObject mirrorOrca;
    //public GameObject mirrorJelly;
    //public GameObject upperSeaBox;
    public GameObject terrain;
    public MeshRenderer waterSurfaceRenderer;
    private Material surfaceMaterial;
    private Color surfaceColour;
    public DepthCalculator depthCalculator;

    private void Start()
    {
        surfaceMaterial = waterSurfaceRenderer.material;
        terrain.SetActive(false);
    }

    void Update()
    {
        if(depthCalculator != null)
        {
            if (mirrorObjects[0] != null)
            {
                if (depthCalculator.headsetDepthCorrected >= 0)
                {
                    //switches off mirror orca, mirror jelly, and upper seabox
                    for (int i = 0; i < mirrorObjects.Count; i++)
                    {
                        mirrorObjects[i].SetActive(false);
                    }
                    terrain.SetActive(true);
                    SetSurfaceAlpha();
                }
                else
                {
                    //switches on mirror objects, mirror jelly, and upper seabox
                    for (int i = 0; i < mirrorObjects.Count; i++)
                    {
                        mirrorObjects[i].SetActive(true);
                    }
                    terrain.SetActive(false);
                    ResetSurfaceAlpha();
                }
            }
            
        }
        
    }
    private void SetSurfaceAlpha()
    {
        surfaceColour = surfaceMaterial.color;
        surfaceColour.a = 1.0f;
        surfaceMaterial.color = surfaceColour;
    }

    private void ResetSurfaceAlpha()
    {
        surfaceColour = surfaceMaterial.color;
        surfaceColour.a = 0.5f;
        surfaceMaterial.color = surfaceColour;
    }
}

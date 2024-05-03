using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRenderAnimation : MonoBehaviour
{
    private Material pulsatingMaterial;
    //private float maxEmissiveIntensity;
    //private float emissiveIntensity;
    private Color baseColour;
    //private Color HDRColour;
    private float colourFactor;
    public float correctionFactor;

    // Start is called before the first frame update
    void Start()
    {
        pulsatingMaterial= GetComponent<MeshRenderer>().material;
        //maxEmissiveIntensity = 1f;
        //HDRColour = Color.cyan;
    }

    // Update is called once per frame
    void Update()
    {
        colourFactor = (Mathf.Sin(Time.time * correctionFactor) + 1) / 2;
        SetAlpha(colourFactor);
        //SetEmission(colourFactor);
        
    }

    private void SetAlpha(float alpha)
    {
        baseColour = pulsatingMaterial.color;
        baseColour.a = alpha * 0.5f + 0.3f;
        pulsatingMaterial.color = baseColour;
    }

    //private void SetEmission(float intensity)
    //{
    //    emissiveIntensity = colourFactor * maxEmissiveIntensity;
    //    pulsatingMaterial.SetColor("_EmissiveColor", HDRColour * emissiveIntensity);
    //}
}

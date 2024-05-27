using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OrcaHighlightAnimation : MonoBehaviour
{
    private SkinnedMeshRenderer meshRenderer;
    private Material skinMaterial;
    public Color baseColour = Color.white;
    public Color highlightColour = Color.magenta;
    private Color highlight;
    public bool isHighlighted = false;
    public float effectDuration;
    [SerializeField] private float time = 0; 
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinMaterial = meshRenderer.materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(isHighlighted)
        {
            time += Time.deltaTime;
            // Taken modulo the effect duration to loop animation.
            float elapsedTime = time % effectDuration;
            // Each colour change takes 1/4 of the duration.
            float ratio = (elapsedTime % (effectDuration / 2)) / (effectDuration / 2);

            // Lerps for each of the four sections
            
            
            if (elapsedTime < (effectDuration / 2))
            {
                highlight = Color.Lerp(baseColour, highlightColour, ratio);
                
            }
            else if (elapsedTime < (effectDuration))
            {
                highlight = Color.Lerp(highlightColour, baseColour, ratio);
            }

            skinMaterial.SetColor("_BaseColor", highlight);
        }
        else
        {
            skinMaterial.SetColor("_BaseColor", baseColour);
            time = 0;
        }
    }
}

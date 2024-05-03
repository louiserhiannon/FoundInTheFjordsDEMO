using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyCombAnimation : MonoBehaviour
{
    [SerializeField] private Material gradientMaterial;
    public float effectDuration = 3f;

    // Black/Black
    [SerializeField] private Color startTopColour = new(0.92f, 0, 1);
    [SerializeField] private Color startBottomColour = new(0, 0.69f, 1);
    // Black/Yellow
    [SerializeField] private Color midTopColour = new(0, 0.69f, 1);
    [SerializeField] private Color midBottomColour = new(0, 1, 0.2f);
    // White/Blue
    [SerializeField] private Color endTopColour = new(0,1,0.2f);
    [SerializeField] private Color endBottomColour = new(0.92f, 0, 1);

    private void Start()
    {
        gradientMaterial = GetComponent<SkinnedMeshRenderer>().materials[3];
    }

    void Update()
    {
        // Taken modulo the effect duration to loop animation.
        float elapsedTime = Time.timeSinceLevelLoad % effectDuration;
        // Each colour change takes 1/4 of the duration.
        float percent = (elapsedTime % (effectDuration / 4)) / (effectDuration / 4);

        // Lerps for each of the four sections
        Color top, bottom;
        if (elapsedTime < (effectDuration / 4))
        {
            top = Color.Lerp(startTopColour, midTopColour, percent);
            bottom = Color.Lerp(startBottomColour, midBottomColour, percent);
        }
        else if (elapsedTime < (effectDuration / 2))
        {
            top = Color.Lerp(midTopColour, endTopColour, percent);
            bottom = Color.Lerp(midBottomColour, endBottomColour, percent);
        }
        else if (elapsedTime < ((effectDuration * 3) / 4))
        {
            top = Color.Lerp(endTopColour, midTopColour, percent);
            bottom = Color.Lerp(endBottomColour, midBottomColour, percent);
        }
        else
        {
            top = Color.Lerp(midTopColour, startTopColour, percent);
            bottom = Color.Lerp(midBottomColour, startBottomColour, percent);
        }
        gradientMaterial.SetColor("_TopColour", top);
        gradientMaterial.SetColor("_BottomColour", bottom);
    }
}

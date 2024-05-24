using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

public class JellyTalkAnimation : MonoBehaviour
{
    private SkinnedMeshRenderer meshRenderer;
    private Material jellyMouthMaterial;
    private Color colourOriginal;
    private Color colourFinal;
    private Color mouthColour;
    private float alphaOriginal;
    private float alphaFinal;
    private float alpha;
    private float transitionTime;
    private float time;
    public bool isTalking = false;
    private bool newSpeech = true;

    void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        jellyMouthMaterial = meshRenderer.materials[2];
        colourOriginal = jellyMouthMaterial.color;
        colourFinal = new Color(190, 139, 192);
        mouthColour = colourOriginal;
        alphaOriginal = jellyMouthMaterial.color.a;
        alphaFinal = 0.8f;
        alpha = alphaOriginal;
        time = 0;
    }

    public IEnumerator ClaraIsTalking()
    {
        while (isTalking)
        {
            
            if (newSpeech)
            {
                newSpeech = false;
                time = 0;
                transitionTime = 0.5f;
                while (time < transitionTime)
                {
                    alpha += (alphaFinal - alphaOriginal) / transitionTime * Time.deltaTime;
                    mouthColour.a = alpha;
                    jellyMouthMaterial.color = mouthColour;
                    time += Time.deltaTime;
                }

            }
            
            time = 0;
            transitionTime = Random.Range(0, 0.5f);
            while(time < transitionTime)
            {
                mouthColour = Color.Lerp(mouthColour, colourFinal, transitionTime * Time.deltaTime);
                jellyMouthMaterial.color = mouthColour;
                time += Time.deltaTime;
            }
            time = 0;
            transitionTime = Random.Range(0, 0.5f);
            while (time < transitionTime)
            {
                mouthColour = Color.Lerp(mouthColour, colourOriginal, transitionTime * Time.deltaTime);
                jellyMouthMaterial.color = mouthColour;
                time += Time.deltaTime;
            }

            yield return null;
        }

        time = 0;
        transitionTime = 0.5f;
        while (time < transitionTime)
        {
            alpha += (alphaOriginal - alphaFinal) / transitionTime * Time.deltaTime;
            mouthColour.a = alpha;
            jellyMouthMaterial.color = mouthColour;
            time += Time.deltaTime;
        }
    }
}

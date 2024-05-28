using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

public class JellyTalkAnimation : MonoBehaviour
{
    private SkinnedMeshRenderer meshRenderer;
    private Material jellyMouthMaterial;
    [SerializeField] private Color startTopColour = new(0.92f, 0, 1);
    [SerializeField] private Color startBottomColour = new(0, 0.69f, 1);
    [SerializeField] private Color midTopColour = new(0, 0.69f, 1);
    [SerializeField] private Color midBottomColour = new(0, 1, 0.2f);
    [SerializeField] private Color endTopColour = new(0, 1, 0.2f);
    [SerializeField] private Color endBottomColour = new(0.92f, 0, 1);

    private Color OriginalColour = new(0.92f, 0, 1);

    [SerializeField] private float transitionTime;
    [SerializeField] private float time;
    //[SerializeField] private float deltaAlpha;
    public bool isTalking = false;
    //[SerializeField] private bool newSpeech = true;
    //private Color testColour;

    void Awake()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        jellyMouthMaterial = meshRenderer.materials[2];
        time = 0;
        OriginalColour = startTopColour;
    }

    public void GetTalking()
    {
        StartCoroutine(ClaraIsTalking());
    }

    private IEnumerator ClaraIsTalking()
    {
        Color top, bottom;
        while (isTalking)
        {
            time = 0;
            transitionTime = Random.Range(1, 2f);
            Debug.Log(transitionTime);
            while (time < transitionTime/4)
            {
                top = Color.Lerp(startTopColour, midTopColour, time % (transitionTime/4)/(transitionTime/4));
                bottom = Color.Lerp(startBottomColour, midBottomColour, time % (transitionTime / 4) / (transitionTime / 4));
                jellyMouthMaterial.SetColor("_TopColour", top);
                jellyMouthMaterial.SetColor("_BottomColour", bottom);

                time += Time.deltaTime;
                yield return null;
            }
            while (time < transitionTime / 2)
            {
                top = Color.Lerp(midTopColour, endTopColour, time % (transitionTime / 4) / (transitionTime / 4));
                bottom = Color.Lerp(midBottomColour, endBottomColour, time % (transitionTime / 4) / (transitionTime / 4));
                jellyMouthMaterial.SetColor("_TopColour", top);
                jellyMouthMaterial.SetColor("_BottomColour", bottom);
                time += Time.deltaTime;
                yield return null;
            }
            while (time < 3 * transitionTime / 4 )
            {
                top = Color.Lerp(endTopColour, midTopColour, time % (transitionTime / 4) / (transitionTime / 4));
                bottom = Color.Lerp(endBottomColour, midBottomColour, time % (transitionTime / 4) / (transitionTime / 4));
                jellyMouthMaterial.SetColor("_TopColour", top);
                jellyMouthMaterial.SetColor("_BottomColour", bottom);
                time += Time.deltaTime;
                yield return null;
            }
            while (time < transitionTime)
            {
                top = Color.Lerp(midTopColour, startTopColour, time % (transitionTime / 4) / (transitionTime / 4));
                bottom = Color.Lerp(midBottomColour, startBottomColour, time % (transitionTime / 4) / (transitionTime / 4));
                jellyMouthMaterial.SetColor("_TopColour", top);
                jellyMouthMaterial.SetColor("_BottomColour", bottom);
                time += Time.deltaTime;
                yield return null;
            }

            yield return null;
        }

        jellyMouthMaterial.SetColor("_TopColour", OriginalColour);
        jellyMouthMaterial.SetColor("_BottomColour", OriginalColour);

    }

}

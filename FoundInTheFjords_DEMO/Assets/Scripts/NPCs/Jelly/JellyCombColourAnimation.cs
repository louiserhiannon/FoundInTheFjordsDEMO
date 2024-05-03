using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class JellyCombColourAnimation : MonoBehaviour
{
    private SkinnedMeshRenderer jellySkinnedMeshRenderer;
    //private List<Material> materials = new List<Material>();
    //private MaterialPropertyBlock materialPropertyBlock;
    [SerializeField] private float transitionTime;
    private float transitionDuration = 2f;
    [SerializeField] private float colourGradientValue;
    //private float transitionOffset = 0.05f;
    [SerializeField] Color newColour;

    [SerializeField] Gradient colourGradient;


    // Start is called before the first frame update
    void Start()
    {
        
        jellySkinnedMeshRenderer= GetComponent<SkinnedMeshRenderer>();
        //for (int i = 3; i < jellySkinnedMeshRenderer.materials.Length; i++)
        //{
        //    materials.Add(jellySkinnedMeshRenderer.materials[i]);  
        //}
        //materialPropertyBlock = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        //update the time
        transitionTime = Mathf.Repeat(transitionTime + Time.deltaTime, transitionDuration);
        newColour = colourGradient.Evaluate(transitionTime / transitionDuration);

        //retrieve and apply colour
        for (int i = 3; i < jellySkinnedMeshRenderer.materials.Length; i++)
        {
            
            //materials[i].color = newColour;
            //jellySkinnedMeshRenderer.materials[i+3].color = materials[i].color;
            jellySkinnedMeshRenderer.materials[i].SetColor("_BaseColor", newColour);
            //jellySkinnedMeshRenderer.materials[i].SetColor("_EmmissionColor", newColour);
            //jellySkinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyInteractionBasic : MonoBehaviour
{
    
    
    public GameObject interactableObject;
    public MeshRenderer interactionSignifier;
    private Color originalColour;
    public Level00Coroutines level00;
    public Level01Coroutines level01;




    private void Awake()
    {
        //jellyCanvas = FindObjectOfType<Canvas>();
        originalColour = interactionSignifier.material.color;
    }


    public void ChangeSignifierColour()
    {
        interactionSignifier.material.color = Color.green;
    }

    public void ResetSignifierColour()
    {
        interactionSignifier.material.color = originalColour;
    }

    public void PerformAction00(string coroutineName)
    {
        level00.StartCustomCoroutine(coroutineName);
    }

    public void PerformAction01(string coroutineName)
    {
        level01.StartCustomCoroutine(coroutineName);
    }


}

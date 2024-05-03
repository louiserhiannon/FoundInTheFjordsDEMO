using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIT_GoToZodiac : UITransition
{
    public GameObject ladder;
    public Canvas thisCanvas;
    public Canvas sayGoodbyeToMom;
    public AudioClip voiceover44;
    public AudioClip voiceover45;
    public ParticleSystem swirl;
    public ParticleSystem scales;
    public MovementControls moveControls;

    private float voiceover44duration = 7.8f;
    
    public override void UINext()
    {
        ladder.SetActive(true);
        foreach (CanvasGroup panel in thisCanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.interactable= false;
            panel.blocksRaycasts= false;
            panel.DOFade(0, 1);
        }

        StartCoroutine(ReadyToGo());

    }

    public IEnumerator ReadyToGo()
    {
        //stop particle systems
        swirl.Stop();
        scales.Stop();
        //play voiceover44
        audioSource.PlayOneShot(voiceover44);
        yield return new WaitForSeconds(voiceover44duration);
        //play voiceover 45
        audioSource.PlayOneShot(voiceover45);
        //activate move controls
        moveControls.ActivateMovementControls();
        yield return new WaitForSeconds(voiceover45.length);
        //activate mom canvas
        foreach (CanvasGroup panel in sayGoodbyeToMom.GetComponentsInChildren<CanvasGroup>())
        {
            panel.interactable = true;
            panel.blocksRaycasts = true;
            panel.DOFade(1, 1);
        }
    }
}

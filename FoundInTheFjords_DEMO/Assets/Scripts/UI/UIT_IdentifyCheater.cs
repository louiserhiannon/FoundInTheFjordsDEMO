using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;

public class UIT_IdentifyCheater : UITransition
{
    public EatingControllerDEMO eatingController;
    public MovementControls moveControls;
    public GameObject xRRig;
    public bool isRight;
    public AudioSource orcaMomAudio;
    public AudioClip voiceover09;
    public AudioClip voiceover10;
    public AudioClip voiceover11;
    public AudioClip voiceover12;
    public AudioClip voiceover13;
    private float voiceover10Duration = 11.5f;
    private float voiceover11Duration = 26.6f;
    private float voiceover12Duration = 13.2f;
    private float voiceover13aDuration = 8f;
    private float voiceover13bDuration = 10f;
    private float voiceover13cDuration = 11.5f;
    public Canvas identifyThief;
    public Canvas eatDirectionsCanvas;
    public CanvasGroup step01Text;
    public CanvasGroup step02Text;
    public CanvasGroup step03Text;
    public Canvas herringCanvas;
    //public HerringCounter herringCounter;
    public SpawnEatableHerring spawnHerring;


    public override void UINext()
    {
        if (isRight)
        {
            StartCoroutine(CorrectResponse());
        }
        else
        {
            StartCoroutine(IncorrectResponse());
        }
        
    }

    protected IEnumerator CorrectResponse()
    {
        foreach (CanvasGroup panel in identifyThief.GetComponentsInChildren<CanvasGroup>())
        {
            panel.interactable = false;
        }

        yield return new WaitForSeconds(0.5f);
        //Fade Panels
        foreach (CanvasGroup panel in identifyThief.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(0f, 1.5f);
            panel.blocksRaycasts = false;
        }
        //play voiceover 10
        orcaMomAudio.PlayOneShot(voiceover10);
        yield return new WaitForSeconds(voiceover10Duration);
        
        //play voiceover 11
        orcaMomAudio.PlayOneShot(voiceover11);
        yield return new WaitForSeconds(voiceover11Duration);

        //play voiceover 12
        orcaMomAudio.PlayOneShot(voiceover12);
        yield return new WaitForSeconds(voiceover12Duration);

        //Activate instructional panel
        eatDirectionsCanvas.GetComponentInChildren<CanvasGroup>().DOFade(1f, 1f);
        yield return new WaitForSeconds(0.5f);
        
        //Start Audio of steps
        orcaMomAudio.PlayOneShot(voiceover13);
        
        //Step 1 Text
        step01Text.DOFade(1f, 1f);
        yield return new WaitForSeconds(voiceover13aDuration);

        //Step 2 Audio and Text
        step02Text.DOFade(1f, 1f);
        yield return new WaitForSeconds(voiceover13bDuration);

        //Step 3 Audio and Text
        step03Text.DOFade(1f, 1f);

        
        
        yield return new WaitForSeconds(voiceover13cDuration);

        //DeActivate instructional panel
        foreach (CanvasGroup panel in eatDirectionsCanvas.GetComponentsInChildren<CanvasGroup>())
        {
             panel.DOFade(0f, 1f);
        }
       

        //activate movement controls
        moveControls.ActivateMovementControls();

        //activate eating controller
        moveControls.ActivateEatControls();

        //set counter type bool to fishingBoat
        EatingController.EC.eat_fishing = true;

        //set target number of fish
        EatingController.EC.targetHerringCount = 6;

        //activate herring panel
        foreach (CanvasGroup panel in herringCanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(1f, 1f);
        }
        herringCounter.displayActive = true;
    }

    protected IEnumerator IncorrectResponse()
    {
        yield return new WaitForSeconds(0.5f);
        orcaMomAudio.PlayOneShot(voiceover09);
    }

}

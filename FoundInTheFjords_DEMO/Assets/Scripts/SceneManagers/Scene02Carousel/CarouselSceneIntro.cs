using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class CarouselSceneIntro : MonoBehaviour
{
    public AudioSource orcaMomSounds;
    public AudioSource reflectedSounds;
    public AudioSource noraSounds;
    public AudioClip voiceover01a;
    public AudioClip voiceover01b;
    public AudioClip voiceover01c;
    public AudioClip voiceover02;
    public AudioClip clickTrain;
    public Canvas arrowCanvas;
    public List<CanvasGroup> echolocationPanels;
    public OceanMovement oceanMovement;
    public Animator orcaMomAnimator;
    public float timeBeforeVoiceover1;
    public float voiceover1Part1aDuration;
    public float voiceover1Part1bDuration;
    public float voiceover1Part1cDuration;
    public float pauseAfterClick;
    public float voiceover2Duration;
    public float pauseForReflection;



    public IEnumerator Scene02Intro()
    {
        //wait some seconds
        yield return new WaitForSeconds(timeBeforeVoiceover1);
        //Start voiceover 1a
        orcaMomSounds.PlayOneShot(voiceover01a);
        //wait some seconds
        yield return new WaitForSeconds(voiceover1Part1aDuration);
        //Start voiceover 1b
        noraSounds.PlayOneShot(voiceover01b);
        
        //Slow down surroundings and stop
        oceanMovement.isMoving = false;
        //Stop swim animation
        orcaMomAnimator.SetTrigger("Trigger_StopSwim");
        //Wait some seconds
        yield return new WaitForSeconds(voiceover1Part1bDuration);
        //show echolocation canvas
        if (echolocationPanels[0] != null)
        {
            echolocationPanels[0].DOFade(1f, 1.5f);
            echolocationPanels[0].interactable = true;
            echolocationPanels[0].blocksRaycasts = true;
        }
        //Start voiceover 1a
        orcaMomSounds.PlayOneShot(voiceover01c);
        //wait some seconds
        yield return new WaitForSeconds(voiceover1Part1cDuration);
        //play click sound
        orcaMomSounds.PlayOneShot(clickTrain);
        //wait some seconds
        yield return new WaitForSeconds(pauseAfterClick);
        //Activate Voiceover 2
        orcaMomSounds.PlayOneShot(voiceover02);

        //Start echolocation animation
        for (int i = 0; i < 8; i++)
        {
            ImageSwitch(i);
            yield return new WaitForSeconds(1f);
        }

        if (echolocationPanels[8] != null)
        {
            echolocationPanels[8].alpha = 0;
            echolocationPanels[8].interactable = false;
            echolocationPanels[8].blocksRaycasts = false;
        }
        if (echolocationPanels[1] != null)
        {
            echolocationPanels[1].alpha = 1;
            echolocationPanels[1].interactable = true;
            echolocationPanels[1].blocksRaycasts = true;
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 8; i++)
        {
            ImageSwitch(i);
            yield return new WaitForSeconds(1f);
        }

        if (echolocationPanels[8] != null)
        {
            echolocationPanels[8].alpha = 0;
            echolocationPanels[8].interactable = false;
            echolocationPanels[8].blocksRaycasts = false;
        }
        if (echolocationPanels[0] != null)
        {
            echolocationPanels[0].alpha =1;
            echolocationPanels[0].interactable = true;
            echolocationPanels[0].blocksRaycasts = true;
        }

        //Wait some seconds
        yield return new WaitForSeconds(voiceover2Duration / 4f);

        //Fade echolocation canvas
        if (echolocationPanels[0] != null)
        {
            echolocationPanels[0].DOFade(0f, 1.5f);
            echolocationPanels[0].interactable = false;
            echolocationPanels[0].blocksRaycasts = false;
        }

        //Wait some seconds
        yield return new WaitForSeconds(voiceover2Duration / 4f);

        //play click sound
        orcaMomSounds.PlayOneShot(clickTrain);
        
        
        //wait some seconds
        yield return new WaitForSeconds(pauseForReflection);
        //play click sound reflection
        reflectedSounds.PlayOneShot(clickTrain, 0.5f);

        //Activate Arrow Panels
        foreach (CanvasGroup panel in arrowCanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(1f, 1.5f);
            panel.interactable = true;
            panel.blocksRaycasts = true;
        }

    }

    private void ImageSwitch(int value)
    {
        if (echolocationPanels[value] != null)
        {
            echolocationPanels[value].alpha = 0;
            echolocationPanels[value].interactable = false;
            echolocationPanels[value].blocksRaycasts = false;
        }
        if (echolocationPanels[value+1] != null)
        {
            echolocationPanels[value + 1].alpha = 1;
            echolocationPanels[value + 1].interactable = true;
            echolocationPanels[value + 1].blocksRaycasts = true;
        }
    }
}

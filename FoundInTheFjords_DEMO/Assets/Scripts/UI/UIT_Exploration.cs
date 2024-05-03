using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;

public class UIT_Exploration : MonoBehaviour
{
    public AudioSource momAudioSource;
    public List<AudioClip> voiceoverClips; //voiceovers 1-4
    public List<float> voiceoverDurations;
    public List<CanvasGroup> subtitleSnippets;
    public Canvas controllerInstructions;
    public GameObject leftFin;
    public GameObject rightFin;
    public GameObject leftController;
    public GameObject rightController;
    public CanvasGroup subtitlePanel;
    public ParticleSystem bubbles;
    public GameObject claraInteractionSignifier;
    
    public Animator orcaMomAnimator;
    public MoveToObject momToClara;
    public MovementControls moveControls;
    


    public void StartExploringWithMom()
    {
        StartCoroutine(ExplorationPart1());
    }

    public IEnumerator ExplorationPart1()
    {
        //start mom swimming
        orcaMomAnimator.SetTrigger("Trigger_Swim");


        StartCoroutine(FadePanels());

        while(momToClara.distance > momToClara.minDistance)
        {
            momToClara.MoveToMinimumDistance();
            yield return null;
        }
        orcaMomAnimator.SetTrigger("Trigger_StopSwim");

        yield return new WaitForSeconds(1f);
        
        //start voiceover 01 and subtitles
        momAudioSource.PlayOneShot(voiceoverClips[0]);
        bubbles.Play();
        subtitlePanel.DOFade(1, 1);
        subtitleSnippets[0].DOFade(1, 1);
        //Switch on locomotion controls
        MovementControls.MC.ActivateMovementControls();
        //wait for duration of clip
        yield return new WaitForSeconds(voiceoverDurations[0]);
        subtitleSnippets[0].DOFade(0, 1);
        bubbles.Stop();
        yield return new WaitForSeconds(1f);

        //start voiceover 02 and subtitles
        momAudioSource.PlayOneShot(voiceoverClips[1]);
        bubbles.Play();
        subtitleSnippets[1].DOFade(1, 1);
        //wait for duration of clip
        yield return new WaitForSeconds(voiceoverDurations[1]);
        subtitleSnippets[1].DOFade(0, 1);
        bubbles.Stop();
        yield return new WaitForSeconds(1f);

        //start voiceover 03 and subtitles
        momAudioSource.PlayOneShot(voiceoverClips[2]);
        bubbles.Play();
        subtitleSnippets[2].DOFade(1, 1);
        //wait for duration of subtitle snippet
        yield return new WaitForSeconds(voiceoverDurations[2]);
        subtitleSnippets[2].DOFade(0, 1);
        subtitleSnippets[3].DOFade(1, 1);
        yield return new WaitForSeconds(voiceoverDurations[3]);
        subtitleSnippets[3].DOFade(0, 1);
        subtitleSnippets[4].DOFade(1, 1);
        yield return new WaitForSeconds(voiceoverDurations[4]);
        subtitleSnippets[4].DOFade(0, 1);
        subtitleSnippets[5].DOFade(1, 1);
        yield return new WaitForSeconds(voiceoverDurations[5]);
        subtitleSnippets[5].DOFade(0, 1);
        bubbles.Stop();
        yield return new WaitForSeconds(1f);

        //start voiceover 04 and subtitles
        momAudioSource.PlayOneShot(voiceoverClips[3]);
        
        bubbles.Play();
        subtitleSnippets[6].DOFade(1, 1);
        //wait for duration of subtitle snippet
        yield return new WaitForSeconds(voiceoverDurations[6]);
        subtitleSnippets[6].DOFade(0, 1);
        subtitleSnippets[7].DOFade(1, 1);
        yield return new WaitForSeconds(voiceoverDurations[7]);
        subtitleSnippets[7].DOFade(0, 1);
        bubbles.Stop();
        yield return new WaitForSeconds(1f);

        subtitlePanel.DOFade(0, 1);

        //make Clara interactable
        claraInteractionSignifier.SetActive(true);

    }

    private IEnumerator FadePanels()
    {
        //fade canvas panels
        foreach (CanvasGroup panel in controllerInstructions.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(0f, 1f);
            panel.interactable = false;
            panel.blocksRaycasts = false;
        }
        yield return new WaitForSeconds(2f);
        //switch controller models back
        leftFin.SetActive(true);
        rightFin.SetActive(true);
        leftController.SetActive(false);
        rightController.SetActive(false);
    }
}

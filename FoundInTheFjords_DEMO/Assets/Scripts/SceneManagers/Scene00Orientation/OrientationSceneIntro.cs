using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class OrientationSceneIntro : MonoBehaviour
{
    public AudioSource claraAudioSource;
    public AudioClip voiceover01;
    public AudioClip voiceover02;
    public AudioClip voiceover03;
    private float voiceover01Duration = 8.6f;
    private float voiceover02Duration = 10.5f;
    private float voiceover03Duration = 12.5f;
    public CanvasGroup controllerInstructions;
    public CanvasGroup controllerInstructionsButton;
    public GameObject leftFin;
    public GameObject rightFin;
    public GameObject leftController;
    public GameObject rightController;
    public CanvasGroup subtitlePanel;
    public List<CanvasGroup> subtitleSnippets;
    private TMP_Text next;
    private Color originalColour;
    

    private void Start()
    {
        next = controllerInstructionsButton.GetComponentInChildren<TMP_Text>();
        originalColour = next.color;
        next.color = Color.grey;
    }

    public IEnumerator Scene00Intro()
    {
        
        yield return new WaitForSeconds(2f);
        //play voiceover 1 and show subtitle 1
        claraAudioSource.PlayOneShot(voiceover01);
        subtitlePanel.DOFade(1, 1);
        subtitleSnippets[0].DOFade(1, 1);
        //wait for duration of clip
        yield return new WaitForSeconds(voiceover01Duration);
        subtitleSnippets[0].DOFade(0, 1);
        yield return new WaitForSeconds(1f);
        //play voiceover 2
        claraAudioSource.PlayOneShot(voiceover02);
        subtitleSnippets[1].DOFade(1, 1);
        //wait for duration of clip
        yield return new WaitForSeconds(voiceover02Duration);
        subtitleSnippets[1].DOFade(0, 1);
        yield return new WaitForSeconds(1f);
        //play voiceover 3
        claraAudioSource.PlayOneShot(voiceover03);
        subtitleSnippets[2].DOFade(1, 1);
        //wait for duration of clip
        yield return new WaitForSeconds(voiceover03Duration);
        subtitleSnippets[2].DOFade(0, 1);
        subtitlePanel.DOFade(0, 1);
        yield return new WaitForSeconds(1f);
        //show instructional panel and switch out fins for controllers
        controllerInstructions.DOFade(1.0f, 1.5f);
        //controllerInstructions.interactable = true;
        //controllerInstructions.blocksRaycasts = true;
        controllerInstructionsButton.DOFade(1.0f, 1.5f);

        leftFin.SetActive(false);
        rightFin.SetActive(false);
        leftController.SetActive(true);
        rightController.SetActive(true);
        

        //pause for learning and practice
        yield return new WaitForSeconds(8f);

        //activate button

        next.color = originalColour;
        controllerInstructions.blocksRaycasts = true;
        controllerInstructions.interactable = true;
        controllerInstructionsButton.interactable = true;
        controllerInstructionsButton.blocksRaycasts = true;

    }
}

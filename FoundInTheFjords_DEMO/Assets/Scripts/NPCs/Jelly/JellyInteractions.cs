using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEditor.Rendering;

public class JellyInteractions : MonoBehaviour
{
    public GameObject interactableObject;
    public Canvas jellyCanvas;
    public CanvasGroup firstPanel;
    private AudioSource jellyAudioSource;
    public AudioClip jellyClip;
    public CanvasGroup firstButton;
    public float clipDuration;
    public MeshRenderer interactionSignifier;
    private Color originalColour;
    public MovementControls moveControls;
    public Transform snorkeler01Target;
    public MoveToObject moveToSnorkeler;
    public MoveToObject rotateToNora;
    public AudioSource zodiacDriverAudioSource;
    public AudioSource claraAudioSource;
    public AudioSource noraAudioSource;
    public AudioClip voiceover31;
    public AudioClip voiceover32;
    public AudioClip voiceover33;
    public AudioClip voiceover34;
    public AudioClip voiceover35;
    public AudioClip voiceover36;
    public AudioClip voiceover37;
    public AudioClip voiceover38;
    public AudioClip voiceover39;
    private float voiceover31Duration = 12.0f;
    private float voiceover32Duration = 9.1f;
    private float voiceover33Duration = 3.4f;
    private float voiceover34Duration = 9.4f;
    private float voiceover35Duration = 3.5f;
    private float voiceover36Duration = 9.7f;
    private float voiceover37Duration = 2.6f;
    private float voiceover38Duration = 12.4f;
    private float voiceover39Duration = 5.3f;
    public AudioSource bgmSource;
    public string coroutineName;
    public bool firstSelectionAudio = true;
    public bool firstSelectionUI = true;
    private bool coroutineStarted = false;
    public Transform snorkeler01;
    public Transform snorkeler02;
    public Transform snorkeler03;
    public float speed;
    public MeetSnorkeler meetSnorkleler;
    private bool alternativeActionSelected = false;




    private void Awake()
    {
        //jellyCanvas = FindObjectOfType<Canvas>();
        jellyAudioSource = GetComponentInParent<AudioSource>();
        originalColour = interactionSignifier.material.color;
        DisablePanels();

        if (jellyClip == null)
        {
            clipDuration = 0f;
        }

    }

    
    private void DisablePanels()
    {
        if(jellyCanvas != null)
        {
            foreach (CanvasGroup panel in jellyCanvas.GetComponentsInChildren<CanvasGroup>())
            {
                panel.alpha = 0;
                panel.interactable = false;
                panel.blocksRaycasts = false;
            }
        }
        
    }

    public void ChangeSignifierColour()
    {
        interactionSignifier.material.color = Color.green;
    }

    public void ResetSignifierColour()
    {
        interactionSignifier.material.color = originalColour;
    }


    public void PlayJellyAudio()
    {
        if (firstSelectionAudio)
        {
            StartCoroutine(FadeAudioSource.StartFade(bgmSource, 2f, 0f));
            if (jellyClip != null)
            {
                jellyAudioSource.PlayOneShot(jellyClip);
                firstSelectionAudio = false;
                //make mom uninteractable
                if (interactableObject.activeSelf)
                {
                    //interactableObject.gameObject.GetComponent<XRSimpleInteractable>().enabled = false;
                    interactableObject.SetActive(false);
                }
                    
            }
            else 
            {
                SelectAlternativeAction();

            }
        }
        


    }

    public void EnableJellyUI()
    {
        if (firstSelectionUI)
        {
            if (firstPanel != null)
            {
                firstPanel.DOFade(1f, 1.5f);
                firstPanel.interactable = true;
                firstPanel.blocksRaycasts = true;

                if(firstButton != null)
                {
                    StartCoroutine(ShowButton());
                }
                                
                firstSelectionUI = false;
                //make mom uninteractable
                if (interactableObject.activeSelf)
                {
                    //interactableObject.gameObject.GetComponent<XRSimpleInteractable>().enabled = false;
                    interactableObject.SetActive(false);
                }
            }

        }
        else if (!alternativeActionSelected)
        {
            alternativeActionSelected = true;
            SelectAlternativeAction();
            
        }

    }

    public void SelectAlternativeAction()
    {
        if(firstSelectionAudio || firstSelectionUI == false)
        {
            if(coroutineStarted == false)
            {
                StartCustomCoroutine(coroutineName);
                coroutineStarted = true;
            }
        }

        
    }

    public IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(clipDuration);
        firstButton.DOFade(1f, 1.5f);
        firstButton.interactable = true;
        firstButton.blocksRaycasts = true;
        Debug.Log("button should be visible");
    }

    public void StartCustomCoroutine(string name)
    {
        StartCoroutine(name);
    }

    public IEnumerator IntroduceSnorkelers()
    {
        //Music fade out
        StartCoroutine(FadeAudioSource.StartFade(bgmSource, 2f, 0f));
        //Play voiceover 31
        zodiacDriverAudioSource.PlayOneShot(voiceover31);
        yield return new WaitForSeconds(voiceover31Duration);

        //make mom uninteractable
        //interactableObject.gameObject.GetComponent<XRSimpleInteractable>().enabled = false;
        interactableObject.SetActive(false);

        //Play voiceover 32
        claraAudioSource.PlayOneShot(voiceover32);
        yield return new WaitForSeconds(voiceover32Duration);
        //Play voiceover 33
        zodiacDriverAudioSource.PlayOneShot(voiceover33);
        //Play snorkeler animation
        //set targettransform1 rotation to 270
        snorkeler01Target.eulerAngles = new Vector3(snorkeler01Target.eulerAngles.x, 270, snorkeler01Target.eulerAngles.z);
        StartCoroutine(SnorkelerSlide(snorkeler01));
        yield return new WaitForSeconds(voiceover33Duration / 3);
        StartCoroutine(SnorkelerSlide(snorkeler02));
        yield return new WaitForSeconds(voiceover33Duration / 3);
        StartCoroutine(SnorkelerSlide(snorkeler03));
        yield return new WaitForSeconds(voiceover33Duration / 3);

        //Play voiceover 34
        claraAudioSource.PlayOneShot(voiceover34);
        yield return new WaitForSeconds(voiceover34Duration);
        //Play voiceover 35
        noraAudioSource.PlayOneShot(voiceover35);
        yield return new WaitForSeconds(voiceover35Duration);
        //Play voiceover 36
        claraAudioSource.PlayOneShot(voiceover36);
        yield return new WaitForSeconds(voiceover36Duration);
        //Play voiceover 37
        noraAudioSource.PlayOneShot(voiceover37);
        yield return new WaitForSeconds(voiceover37Duration);
        //Play voiceover 38
        claraAudioSource.PlayOneShot(voiceover38);
        
        yield return new WaitForSeconds(voiceover38Duration);

        meetSnorkleler.interactWithSnorkeler = true;
        //make target snorkeler glow
        StartGlow(snorkeler01);

        //Play voiceover 39
        claraAudioSource.PlayOneShot(voiceover39);
        yield return new WaitForSeconds(voiceover39Duration);
        //Activate mirror actions on snorkeler

        coroutineStarted = false;
        alternativeActionSelected = false;

    }

    public IEnumerator SnorkelerSlide(Transform snorkeler)
    {
        
        snorkeler.GetComponent<Animator>().SetTrigger("Trigger_Slide");
        yield return new WaitForSeconds(0.5f);
        float time = 0;
        while (time < 1.8f)
        {
            snorkeler.Translate((snorkeler.transform.forward - snorkeler.transform.up) * speed * Time.deltaTime);
            time+= Time.deltaTime;
            yield return null;
        }

        while (snorkeler.position.y < 0.1f)
        {
            snorkeler.Translate((snorkeler.transform.forward + snorkeler.transform.up) * speed);
            yield return null;
        }

        var moveSnorkeler = snorkeler.GetComponent<MoveToObject>();
        moveSnorkeler.distance = Vector3.Distance(snorkeler.position, moveSnorkeler.targetTransform.position);
        while(moveSnorkeler.distance > moveSnorkeler.minDistance)
        {
            moveSnorkeler.MoveToMinimumDistance();
            yield return null;
        }
        while (moveSnorkeler.transform.eulerAngles.y > moveSnorkeler.targetTransform.eulerAngles.y + 5 || moveSnorkeler.transform.eulerAngles.y < moveSnorkeler.targetTransform.eulerAngles.y - 5)
        {
            moveSnorkeler.RotateToAlign();
            yield return null;
        }

        alternativeActionSelected = false;
    }
    public void StartGlow(Transform snorkeler)
    {
        Material material = snorkeler.GetComponentInChildren<SkinnedMeshRenderer>().materials[6];
        Color activeColour = Color.green;
        material.color = activeColour;

    }

}

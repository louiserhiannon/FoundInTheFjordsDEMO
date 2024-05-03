using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UIT_locateBaitball : MonoBehaviour
{
    public AudioSource orcaMomSounds;
    public AudioSource baitballSounds;
    public Animator orcaMomAnimator;
    public AudioClip voiceover3;
    public AudioClip clickTrain;
    public AudioClip voiceover4;
    public AudioClip voiceover5;
    public AudioClip voiceover6;
    public AudioClip voiceover7;
    public AudioClip voiceover8;
    public GameObject carouselTransform;
    public GameObject xRRig;
    public MoveToObject moveToBaitball;
    public GameObject orcaMom;
    public Canvas arrowCanvas;
    public Canvas migrationCanvas;
    public Canvas ecosystemCanvas;
    public Canvas herringCounterCanvas;
    public HerringCounter herringCounter;

    public bool isRight;
   

    
    public void UINext()
    {
        if(isRight)
        {
            StartCoroutine(RightDirection());
        }
        else
        {
            StartCoroutine(WrongDirection());
        }
    }

    protected IEnumerator WrongDirection()
    {
        //Wait some seconds
        yield return new WaitForSeconds(0.5f);

        //Activate Voiceover 3
        orcaMomSounds.PlayOneShot(voiceover3);

        //Wait some seconds
        yield return new WaitForSeconds(4.5f);

        //Repeat click and reflected Sound
        orcaMomSounds.PlayOneShot(clickTrain);
        yield return new WaitForSeconds(5f);
        baitballSounds.PlayOneShot(clickTrain, 0.3f);

    }

    protected IEnumerator RightDirection()
    {
        //Wait some seconds
        yield return new WaitForSeconds(0.5f);
        //Activate Voiceover4
        orcaMomSounds.PlayOneShot(voiceover4);
        

        //start orca carousel animation
        //Start Carousel Animation

        carouselTransform.GetComponentInChildren<FlockManager_Circular>().enabled = true;
        CarouselManager.CM.SpawnCarouselOrca();
        //for (int i = 0; i < CarouselManager.CM.allAxes.Length; i++)
        //{
        //    CarouselManager.CM.allAxes[i].SetActive(false);
        //}
        //Wait until end of voiceover 4
        yield return new WaitForSeconds(voiceover4.length + 0.5f);
        
        //start orca swim animation
        orcaMomAnimator.SetTrigger("Trigger_Swim");
        //Set XR Rig as child of Orca Mom
        xRRig.transform.SetParent(orcaMom.transform);
        //move towards baitball until it comes into view
        while (moveToBaitball.distance > moveToBaitball.viewDistance)
        {
            moveToBaitball.MoveToMinimumDistance();
            //followMom.Follow();
            
            yield return null;
        }

        //fade arrow canvas
        foreach (CanvasGroup panel in arrowCanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(0, 1.5f);
            panel.interactable = false;
            panel.blocksRaycasts = false;
        }

        //Activate voiceover 5
        orcaMomSounds.PlayOneShot(voiceover5);
        //move towards baitball until min distance is reached (8)
        StartCoroutine(MoveCloser()); 
        //stop swim animation
        orcaMomAnimator.SetTrigger("Trigger_StopSwim");
        //Wait until end of voiceover 5
        yield return new WaitForSeconds(voiceover5.length + 0.5f);
        //Activate voiceover 6
        orcaMomSounds.PlayOneShot(voiceover6);
        //Wait until end of Voiceover 6
        yield return new WaitForSeconds(voiceover6.length + 0.5f);
        //Activate voiceover 7
        orcaMomSounds.PlayOneShot(voiceover7);
        //Wait until end of Voiceover 7
        yield return new WaitForSeconds(voiceover7.length + 0.5f);
        //Activate voiceover 8
        orcaMomSounds.PlayOneShot(voiceover8);
        //Remove rig from orca parent
        xRRig.transform.SetParent(null);
        //Activate controls
        xRRig.GetComponent<LocomotionController_General>().enabled = true;
        //xRRig.GetComponentInChildren<ActionBasedSnapTurnProvider>().enabled = true;
        xRRig.GetComponent<EatingControllerDEMO>().enabled = true;
        xRRig.GetComponent<EatingControllerDEMO>().targetActive= true;
        EatingController.EC.eat_simple = true;

        //Show Counter Panel
        herringCounterCanvas.GetComponentInChildren<CanvasGroup>().DOFade(1f,1f);
        herringCounter.displayActive = true;
        


        ////Show panels
        //foreach (CanvasGroup panel in migrationCanvas.GetComponentsInChildren<CanvasGroup>())
        //{
        //    panel.alpha= 1.0f;
        //    panel.interactable= true;
        //    panel.blocksRaycasts= true;
        //}
        //foreach (CanvasGroup panel in ecosystemCanvas.GetComponentsInChildren<CanvasGroup>())
        //{
        //    panel.alpha = 1.0f;
        //    panel.interactable = true;
        //    panel.blocksRaycasts = true;
        //}
    }

    private IEnumerator MoveCloser()
    {
        while (moveToBaitball.distance > moveToBaitball.minDistance)
        {
            moveToBaitball.MoveToMinimumDistance();
            //followMom.Follow();
            yield return null;
        }
    }
}

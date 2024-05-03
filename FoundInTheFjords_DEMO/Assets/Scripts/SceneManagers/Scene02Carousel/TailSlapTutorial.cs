using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TailSlapTutorial : MonoBehaviour
{

    public AudioSource momAudioSource;
    public AudioSource humpbackAudioSource;
    public AudioSource noraAudioSource;
    public AudioClip voiceover14;
    public AudioClip tailslapPractice;
    public AudioClip step00;
    public AudioClip step01;
    public AudioClip step02;
    public AudioClip step03;
    public AudioClip step04;
    public AudioClip tailslapGO;
    public AudioClip humpbackSong;
    public AudioClip voiceover16a;
    public AudioClip voiceover16b;
    public AudioClip voiceover16c;
    public AudioClip voiceover16d;
    public AudioClip voiceover17a;
    public AudioClip voiceover17b;
    private float voiceover14Duration = 7.5f;
    private float tailslapPracticeDuration = 8f;
    private float step00Duration = 4.0f;
    private float step01Duration = 2.5f;
    private float step02Duration = 5.0f;
    private float step03Duration = 5.0f;
    private float step04Duration = 6.0f;
    private float tailslapGODuration = 4.0f;
    private float voiceover16aDuration = 2.1f;
    private float voiceover16bDuration = 13.5f;
    private float voiceover16cDuration = 2.5f;
    private float voiceover16dDuration = 5.5f;

    public Canvas herringCounterCanvas;
    public Canvas tailslapChargeCanvas;
    public Canvas tailslapInstructionCanvas;
    public Canvas combinedCanvas;
    public CanvasGroup step01Text;
    public CanvasGroup step02Text;
    public CanvasGroup step03Text;
    public CanvasGroup step04Text;
    public GameObject xRRig;
    public GameObject orcaMom;
    public MoveToObject moveToMom;
    public HerringCounter herringCounter;
    public Transform safeDistance;
    public HumpbackTestController humpbackController;
    


    
    public IEnumerator TailslapTutorial01()
    {
        //reset herring count
        EatingController.EC.eatenHerringCount = 0;

        yield return new WaitForSeconds(2.5f);
        //Activate voiceover 14
        momAudioSource.PlayOneShot(voiceover14);

        //deactivate Eating Controller
        MovementControls.MC.DeActivateEatControls();

        //deactivate movement controls
        MovementControls.MC.DeactivateMovementControls();

        yield return new WaitForSeconds(voiceover14Duration);

        //fade panels
        foreach (CanvasGroup panel in herringCounterCanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(0f, 1f);
            panel.blocksRaycasts = false;
            panel.interactable = false;
        }

   
        //return to mom
        while (moveToMom.distance > moveToMom.minDistance)
        {
            moveToMom.MoveToMinimumDistance();
            yield return null;
        }

        //rotate to align to mom

        while (xRRig.transform.eulerAngles.y < moveToMom.targetTransform.eulerAngles.y - 2 || xRRig.transform.eulerAngles.y > moveToMom.targetTransform.eulerAngles.y + 2)
        {
            moveToMom.RotateToAlign();
            //float yRotationSelf = transform.eulerAngles.y;
            //float yRotationMom = moveToMom.targetTransform.eulerAngles.y;
            Debug.Log("Trapped in loop");
            //Debug.Log(yRotationSelf);
            //Debug.Log(yRotationMom);
            yield return null;

            //xRRig.transform.rotation != moveToMom.targetTransform.rotation
        }

        Debug.Log("broken out of loop");
        //Remind of tailslap control
        momAudioSource.PlayOneShot(tailslapPractice);
        yield return new WaitForSeconds(tailslapPracticeDuration);

        //Activate tailslap controls (without spawning)
        xRRig.GetComponent<TailslapController>().enabled = true;
        xRRig.GetComponent<TailslapController>().spawnable = false;

        //activate tailslap charge canvas
        tailslapChargeCanvas.GetComponentInChildren<CanvasGroup>().DOFade(1f, 1f);


    }

    public IEnumerator TailslapTutorial02()
    {
        yield return new WaitForSeconds(3f);
        //deactivate tailslap charge panel
        tailslapChargeCanvas.GetComponentInChildren<CanvasGroup>().DOFade(0f, 1f);

        //stop other orca from spawning herring
        CarouselManager.CM.stunHerring = false;

        //Step 0 Audio 
        momAudioSource.PlayOneShot(step00);
        yield return new WaitForSeconds(step00Duration + 1);

        //Activate instructional panel
        tailslapInstructionCanvas.GetComponentInChildren<CanvasGroup>().DOFade(1f, 1f);
        yield return new WaitForSeconds(1.5f);

        //Step 1 Audio and Text
        step01Text.DOFade(1f, 1f);
        momAudioSource.PlayOneShot(step01);
        yield return new WaitForSeconds(step01Duration);

        //Step 2 Audio and Text
        step02Text.DOFade(1f, 1f);
        momAudioSource.PlayOneShot(step02);
        yield return new WaitForSeconds(step02Duration);

        //Step 3 Audio and Text
        step03Text.DOFade(1f, 1f);
        momAudioSource.PlayOneShot(step03);
        yield return new WaitForSeconds(step03Duration);

        //Step 4 Audio and Text
        step04Text.DOFade(1f, 1f);
        momAudioSource.PlayOneShot(step04);
        yield return new WaitForSeconds(step04Duration);

        //TailslapGO audio
        momAudioSource.PlayOneShot(tailslapGO);
        yield return new WaitForSeconds(tailslapGODuration);

        //fade instructional panel
        foreach (CanvasGroup panel in tailslapInstructionCanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(0f, 1f);
        }

        //activate movement controls
        MovementControls.MC.ActivateMovementControls();
        Debug.Log("movement controls should be activated");

        //activate eating controller
        MovementControls.MC.ActivateEatControls();
        Debug.Log("eating controls should be activated");

        //set counter type bool to tailslap
        EatingController.EC.eat_tailslap = true;

        //set target number of fish
        EatingController.EC.targetHerringCount = 4;

        //activate combined panel
        foreach (CanvasGroup panel in combinedCanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(1f, 1f);
        }
        //deactivate charged slider
        var controller = xRRig.GetComponent<TailslapController>();
        for(int i = 0; i < controller.chargedSliders.Count; i++)
        {
            controller.chargedSliders[i].enabled = false;
        }
        herringCounter.displayActive = true;

        //move mom back
        orcaMom.transform.position -= (2 * orcaMom.transform.forward - orcaMom.transform.up);



    }

    public IEnumerator TailslapTutorial03()
    {
        
        yield return new WaitForSeconds(2.5f);
        //play humpback song
        humpbackAudioSource.PlayOneShot(humpbackSong);

        yield return new WaitForSeconds(4f);

        //deactivate Eating Controller
        MovementControls.MC.DeActivateEatControls();

        //deactivate movement controls
        MovementControls.MC.DeactivateMovementControls();


        //resume other orca spawning herring
        CarouselManager.CM.stunHerring = true;

        //fade panels
        foreach (CanvasGroup panel in combinedCanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(0f, 1f);
            panel.blocksRaycasts = false;
            panel.interactable = false;
        }

        //reset herring count
        EatingController.EC.eatenHerringCount = 0;

        //play shocked Nora
        noraAudioSource.PlayOneShot(voiceover16a);
        yield return new WaitForSeconds(voiceover16aDuration);
        noraAudioSource.PlayOneShot(voiceover16b);
        yield return new WaitForSeconds(voiceover16bDuration);
        noraAudioSource.PlayOneShot(voiceover16c);
        yield return new WaitForSeconds(voiceover16cDuration);
        noraAudioSource.PlayOneShot(voiceover16d);
        yield return new WaitForSeconds(voiceover16dDuration);


        //return to mom
        moveToMom.distance = Vector3.Distance(moveToMom.targetTransform.position, moveToMom.transform.position);
        while (moveToMom.distance > moveToMom.minDistance)
        {
            moveToMom.MoveToMinimumDistance();
            yield return null;
        }

        StartCoroutine(humpbackController.GoHumpback());

        //rotate to align to mom

        while (xRRig.transform.eulerAngles.y < moveToMom.targetTransform.eulerAngles.y - 2 || xRRig.transform.eulerAngles.y > moveToMom.targetTransform.eulerAngles.y + 2)
        {
            moveToMom.RotateToAlign();
            yield return null;
            //xRRig.transform.rotation != moveToMom.targetTransform.rotation
        }

        //yield return new WaitForSeconds(1f);

        
       
    }

}

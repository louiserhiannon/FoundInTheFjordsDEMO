using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ExploreFishingBoatArea : MonoBehaviour
{
    
    public Canvas herringCounterCanvas;
    public MoveToObject moveToMom;
    public MoveToObject sDGJelly1;
    public MoveToObject sDGJelly2;
    public Transform clara;
    public Transform zodiac;
    public GameObject xRRig;
    public GameObject momInteractionSignifier;
    public Transform mom;
    public AudioSource momAudioSource;
    public AudioClip voiceover23;
    public AudioClip voiceover24;
    private float voiceover23Duration = 13.1f;
    private float voiceover24Duration = 16.1f;
    public AudioSource bgmSource;
    public AudioClip exploreMusic;
    public EatingControllerDEMO eatingController;

    public IEnumerator ExploreSurroundings01()
    {
        //deactivate Eating Controller
        MovementControls.MC.DeActivateEatControls();

        //deactivate movement controls
        MovementControls.MC.DeactivateMovementControls();

        //fade panels
        foreach (CanvasGroup panel in herringCounterCanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(0f, 1f);
            panel.blocksRaycasts = false;
            panel.interactable = false;
        }

        //reset herring count
        eatingController.eatenHerringCount = 0;

        //Set Target Transform
        moveToMom.targetTransform = mom;
        moveToMom.minDistance = 3.5f;
        moveToMom.speed = 2.5f;
        moveToMom.rotationSpeed = 0.5f;
        //return to mom
        moveToMom.distance = Vector3.Distance(moveToMom.targetTransform.position, moveToMom.transform.position);
        while (moveToMom.distance > moveToMom.minDistance)
        {
            moveToMom.MoveToMinimumDistance();
            yield return null;
        }

        //rotate to align to mom

        while (xRRig.transform.eulerAngles.y < moveToMom.targetTransform.eulerAngles.y - 2 || xRRig.transform.eulerAngles.y > moveToMom.targetTransform.eulerAngles.y + 2)
        {
            moveToMom.RotateToAlign();
            yield return null;
            //xRRig.transform.rotation != moveToMom.targetTransform.rotation
        }

        //play voiceover 23
        momAudioSource.PlayOneShot(voiceover23);

        //start moving interactivejellies into place

        //StartCoroutine(SDGJelly1Move());
        //StartCoroutine(SDGJelly2Move());
        //Set Clara Transform
        clara.SetParent(zodiac);
        clara.localPosition = new Vector3(5.5f, -1f, 5f);
        clara.eulerAngles = new Vector3(0, 60, 0);

        //moveToMom.distance = Vector3.Distance(moveToMom.targetTransform.position, moveToMom.transform.position);
        //while (moveToMom.distance > moveToMom.minDistance)
        //{
        //    moveToMom.MoveToMinimumDistance();
        //    yield return null;
        //}

        //wait for seconds
        yield return new WaitForSeconds(voiceover23Duration);

        //play voiceover 24
        momAudioSource.PlayOneShot(voiceover24);
        yield return new WaitForSeconds(voiceover24Duration);

        //Activate move controls
        MovementControls.MC.ActivateMovementControls();

        //make mom interactable
        momInteractionSignifier.GetComponent<XRSimpleInteractable>().enabled = true;
        momInteractionSignifier.SetActive(true);
        bgmSource.clip = exploreMusic;
        bgmSource.loop = true;
        bgmSource.Play();
        StartCoroutine(FadeAudioSource.StartFade(bgmSource, 5f, 1f));

        yield return null;
    }


    //public IEnumerator SDGJelly1Move()
    //{
    //    sDGJelly1.distance = Vector3.Distance(sDGJelly1.targetTransform.position, sDGJelly1.transform.position);
    //    while (sDGJelly1.distance > sDGJelly1.minDistance)
    //    {
    //        sDGJelly1.MoveToMinimumDistance();
    //        yield return null;
    //    }
    //}

    //public IEnumerator SDGJelly2Move()
    //{
    //    sDGJelly2.distance = Vector3.Distance(sDGJelly2.targetTransform.position, sDGJelly2.transform.position);
    //    while (sDGJelly2.distance > sDGJelly2.minDistance)
    //    {
    //        sDGJelly2.MoveToMinimumDistance();
    //        yield return null;
    //    }
    //}

    


}

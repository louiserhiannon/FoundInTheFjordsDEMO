using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;
using System;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEditor.Rendering;

public class DEMOCoroutine1 : MonoBehaviour
{
    public static DEMOCoroutine1 coroutine01;
    public AudioSource backgroundMusic;
    public AudioClip ottering;
    public AudioSource momAudioSource;
    public List<AudioClip> voiceoverClips;
    public ParticleSystem momBubbles;
    public Animator orcaMomAnimator;
    public MoveToObject moveMomToTarget;
    //private MoveToObject moveNoraToTarget;
    public Transform momRotateTarget;
    public Transform momInitialTarget;
    public Transform momMidTarget;
    public Transform xRRigBoundingBox;
    public GameObject activeOceanBox;
    public List<Transform> groupOneOrcaGroupTargets;
    public List<Transform> groupOneOrcaScatterTargets;
    public OceanMovement oceanMovement;
    public GameObject xRRig;
    public CanvasGroup youCanMovePanel;
    public Canvas meetMomCanvas;
    public List<MoveToObject> moveGroupOneOrcas;
    public GameObject carouselTransform;
    public Transform carouselTarget;
    private MoveToObject moveCarousel;
    public Transform carouselOceanBox;
    public Transform faceCarousel;
    public Transform noraCentre;
    public CanvasGroup chargeCanvas;
    //public DEMOCoroutine2 coroutine02;
    private List<bool> orcasInPlace;
    private WaveManager waveManager;
    public float carouselDistance;

    private void Start()
    {
        coroutine01 = this;
        orcasInPlace = new List<bool>();
        carouselTransform.SetActive(false);
        carouselTransform.GetComponentInChildren<FlockManager_Circular>().enabled = false;
        ActivateControlsDEMO.AC.DeActivateMovementControls();
        ActivateControlsDEMO.AC.DeActivateTailslapControls();
        ActivateControlsDEMO.AC.DeActivateEatControls();
        waveManager = activeOceanBox.GetComponentInChildren<WaveManager>();
        oceanMovement.waveManager = waveManager;
        //moveNoraToTarget = xRRig.GetComponent<MoveToObject>();
        moveCarousel = carouselTransform.GetComponent<MoveToObject>();
        StartCoroutine(DemoCoroutine01());
    }

    //Coroutine 1: Move Mom towards Clara, Start journey, bring Group 1 Orca into the mix, and start carousel activity
    public IEnumerator DemoCoroutine01()
    {
        yield return new WaitForSeconds(0.2f);
        //start mom swimming and move to Nora
        orcaMomAnimator.SetTrigger("Trigger_Swim");
        moveMomToTarget.targetTransform = momInitialTarget;
        while(moveMomToTarget.distance > moveMomToTarget.minDistance)
        {
            //Debug.Log("at least we made it into the loop");
            moveMomToTarget.MoveToMinimumDistance();
            //Debug.Log("mom should be moving");
            yield return null;
        }

        //Deactivate ResetTransform for orca
        for (int i = 0; i< moveGroupOneOrcas.Count; i++)
        {
            GameObject orca = moveGroupOneOrcas[i].gameObject;
            if (orca.TryGetComponent<ResetTransform>(out ResetTransform reset))
            {
                orca.GetComponent<ResetTransform>().enabled = false;
            }
        }

        //while (moveMomToTarget.transform.eulerAngles.y < moveMomToTarget.targetTransform.eulerAngles.y - 2 || moveMomToTarget.transform.eulerAngles.y > moveMomToTarget.targetTransform.eulerAngles.y + 2)
        //{
        //    moveMomToTarget.RotateToAlign();
        //    yield return null;
        //}

        orcaMomAnimator.SetTrigger("Trigger_StopSwim");

        //yield return new WaitForSeconds(1f);
        momAudioSource.PlayOneShot(voiceoverClips[0]);
        momBubbles.Play();
        orcaMomAnimator.SetTrigger("Trigger_Talk");
        yield return new WaitForSeconds(voiceoverClips[0].length);
        momBubbles.Stop();
        orcaMomAnimator.SetTrigger("Trigger_StopTalk");
        yield return new WaitForSeconds(1);

        //Start sea motion
        oceanMovement.isMoving = true;
        orcaMomAnimator.SetTrigger("Trigger_Swim");
        momBubbles.transform.localEulerAngles = new Vector3(-65, momBubbles.transform.localEulerAngles.y, momBubbles.transform.localEulerAngles.z);

        //Start Music
        backgroundMusic.volume = 0f;
        backgroundMusic.Play();
        backgroundMusic.DOFade(0.7f,3);
        //FadeAudioSource.StartFade(backgroundMusic, 3, 1);

        //activate move controls
        ActivateControlsDEMO.AC.ActivateMovementControls();
        StartCoroutine(ManageInfoPanels());
               
        //move and rotate Group 1 orca into place
        for (int i = 0; i < moveGroupOneOrcas.Count; i++)
        {
            moveGroupOneOrcas[i].targetTransform = groupOneOrcaGroupTargets[i];
            StartCoroutine(MoveGroupOneOrca(i));
        }
        //Rotate mom to face forwards
        moveMomToTarget.targetTransform = momRotateTarget;
        while (moveMomToTarget.transform.eulerAngles.y < moveMomToTarget.targetTransform.eulerAngles.y - 2 || moveMomToTarget.transform.eulerAngles.y > moveMomToTarget.targetTransform.eulerAngles.y + 2)
        {
            moveMomToTarget.RotateToAlign();
            yield return null;
        }

        

        //move mom closer to Nora
        moveMomToTarget.targetTransform = momMidTarget;
        moveMomToTarget.speed = 1f;
        moveMomToTarget.minDistance = 0.5f;
        moveMomToTarget.CalculateDistance();
        while (moveMomToTarget.distance > moveMomToTarget.minDistance)
        {
            //Debug.Log("at least we made it into the loop");
            moveMomToTarget.TranslateToMinimumDistance();
            //Debug.Log("mom should be moving");
            yield return null;
        }

        moveMomToTarget.speed = 2f;
        

        while (orcasInPlace.Count < 3)
        {
            yield return null;
        }

        //hide mom info panel
        foreach (CanvasGroup panel in meetMomCanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(0, 1);
        }

        //Play Clip 2.1
        momAudioSource.PlayOneShot(voiceoverClips[1]);
        momBubbles.Play();
        orcaMomAnimator.SetTrigger("Trigger_Talk");

        float tempTime = 0;
        while (orcasInPlace.Count < 5)
        {
            tempTime += Time.deltaTime;
            yield return null;
        }

        //Debug.Log(tempTime);
        orcasInPlace.Clear();
        OrcaOscillationController.OOC.isOscillating = true;

        yield return new WaitForSeconds(voiceoverClips[1].length - tempTime);
        momBubbles.Stop();
        orcaMomAnimator.SetTrigger("Trigger_StopTalk");

        yield return new WaitForSeconds(4);

        momAudioSource.PlayOneShot(voiceoverClips[2]); //this is my favourite thing to do
        momBubbles.Play();
        orcaMomAnimator.SetTrigger("Trigger_Talk");

        //yield return new WaitForSeconds(voiceoverClips[1].length / 3);

        yield return new WaitForSeconds(voiceoverClips[2].length);
        momBubbles.Stop();
        orcaMomAnimator.SetTrigger("Trigger_StopTalk");


        //momAudioSource.PlayOneShot(voiceoverClips[2]);
        //yield return new WaitForSeconds(voiceoverClips[2].length); //might not need
        //Activate carousel when parent box is in far away position
        while (carouselOceanBox.position.z < 200 || carouselOceanBox.position.z > 210)
        {
            yield return null;
        }
        
        carouselTransform.SetActive(true);
        carouselTransform.GetComponentInChildren<FlockManager_Circular>().enabled = true;
        //CarouselManager.CM.SpawnCarouselOrca();
        //decelerate ocean so that it stops in correct place
        while (carouselOceanBox.position.z > 29)
        {
            yield return null;
        }
        
        //rotate mom and Nora to face carousel
        //StartCoroutine(RotateToCarousel(moveNoraToTarget));
        //ActivateControlsDEMO.AC.DeActivateMovementControls();
        xRRigBoundingBox.SetParent(moveMomToTarget.transform, true);

        
        oceanMovement.isMoving = false;
        momBubbles.transform.localEulerAngles = new Vector3(0, momBubbles.transform.localEulerAngles.y, momBubbles.transform.localEulerAngles.z);
        //Activate Group 1 orca scatter
        OrcaOscillationController.OOC.isOscillating = false;
        for (int i = 0; i < moveGroupOneOrcas.Count; i++)
        {
            moveGroupOneOrcas[i].targetTransform = groupOneOrcaScatterTargets[i];
            StartCoroutine(MoveGroupOneOrca(i));
        }
        //Play clip 5.1


        while (oceanMovement.speed > 0)
        {
            yield return null;
        }
        backgroundMusic.DOFade(0, 3);
        momAudioSource.PlayOneShot(voiceoverClips[3]);
        momBubbles.Play();
        orcaMomAnimator.SetTrigger("Trigger_Talk");
        StartCoroutine(RotateToCarousel(moveMomToTarget));
        //StartCoroutine(RecentreNora());
        orcaMomAnimator.SetTrigger("Trigger_StopSwim");
         
             
        yield return new WaitForSeconds(voiceoverClips[3].length - 0.5f);
        momBubbles.Stop();
        orcaMomAnimator.SetTrigger("Trigger_StopTalk");


        yield return new WaitForSeconds(1);

        //move closer to carousel
        //moveCarousel.targetTransform = carouselTarget;
        //moveCarousel.speed = 1.5f;
        //moveCarousel.minDistance = 0.2f;
        //moveCarousel.distance = Vector3.Distance(moveCarousel.targetTransform.position, moveCarousel.transform.position);
        float distance = Vector3.Distance(carouselTransform.transform.position, moveMomToTarget.transform.position);
        carouselDistance = distance;
        float speed = 1.5f;
        //momBubbles.transform.localEulerAngles = new Vector3(-45, momBubbles.transform.localEulerAngles.y, momBubbles.transform.localEulerAngles.z);
        orcaMomAnimator.SetTrigger("Trigger_Swim");

        while (distance > 9)
        {
            //moveCarousel.TranslateToMinimumDistance();
            moveMomToTarget.transform.Translate(speed * Time.deltaTime * Vector3.forward);
            distance = Vector3.Distance(moveMomToTarget.transform.position, carouselTransform.transform.position);
            yield return null;
        }
        momBubbles.transform.localEulerAngles = new Vector3(0, momBubbles.transform.localEulerAngles.y, momBubbles.transform.localEulerAngles.z);
        orcaMomAnimator.SetTrigger("Trigger_StopSwim");
        yield return new WaitForSeconds(2);

        //Activate grip button
        ActivateControlsDEMO.AC.ActivateTailslapControls();
        //Show charge panel
        chargeCanvas.DOFade(1, 1);
        //backgroundMusic.clip = ottering;
        //backgroundMusic.Play();
        //backgroundMusic.DOFade(0.5f, 3);
        //FadeAudioSource.StartFade(backgroundMusic, 2, 1);
        //Play clip 6.1
        momAudioSource.PlayOneShot(voiceoverClips[4]);
        momBubbles.Play();
        orcaMomAnimator.SetTrigger("Trigger_Talk");
        yield return new WaitForSeconds(voiceoverClips[4].length);
        momBubbles.Stop();
        orcaMomAnimator.SetTrigger("Trigger_StopTalk");

        //skip to next coroutine
        //StartCoroutine(coroutine02.DEMOCoroutine02());
    }

    public IEnumerator MoveGroupOneOrca(int index)
    {
        Vector3 bubbleAngle = moveGroupOneOrcas[index].transform.Find("Bubbles").transform.localEulerAngles;
        bubbleAngle.x = 0;
        moveGroupOneOrcas[index].transform.Find("Bubbles").transform.localEulerAngles = new Vector3(bubbleAngle.x, bubbleAngle.y, bubbleAngle.z);

        moveGroupOneOrcas[index].CalculateDistance();
        while (moveGroupOneOrcas[index].distance > moveGroupOneOrcas[index].minDistance)
        {
            moveGroupOneOrcas[index].TranslateToMinimumDistance();
            yield return null;
        }
        while (moveGroupOneOrcas[index].transform.eulerAngles.y < moveGroupOneOrcas[index].targetTransform.eulerAngles.y - 2 || moveGroupOneOrcas[index].transform.eulerAngles.y > moveGroupOneOrcas[index].targetTransform.eulerAngles.y + 2)
        {
            moveGroupOneOrcas[index].RotateToAlign();
            yield return null;
        }
        
        bubbleAngle.x = -65;
        moveGroupOneOrcas[index].transform.Find("Bubbles").transform.localEulerAngles = new Vector3 (bubbleAngle.x, bubbleAngle.y, bubbleAngle.z);

        orcasInPlace.Add(true);
        moveGroupOneOrcas[index].gameObject.GetComponent<OrcaOscillation>().SetOscillationParameters();
    }

    public IEnumerator RotateToCarousel(MoveToObject rotate)
    {
        rotate.targetTransform = faceCarousel;
        rotate.rotationSpeed = 0.2f;
        while (rotate.transform.eulerAngles.y < rotate.targetTransform.eulerAngles.y - 2 || rotate.transform.eulerAngles.y >  rotate.targetTransform.eulerAngles.y + 2)
        {
            rotate.RotateToFaceObject();
            yield return null;
        }
        
    }

    private IEnumerator ManageInfoPanels()
    {
        //flash panel with movement reminder
        youCanMovePanel.DOFade(1, 1);
        yield return new WaitForSeconds(7.5f);
        //hide movement reminder panel
        youCanMovePanel.DOFade(0, 1);
        yield return new WaitForSeconds(1);

        //Show mom info panel
        foreach (CanvasGroup panel in meetMomCanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(1, 1);
        }
    }

    //public IEnumerator RecentreNora()
    //{
    //    Vector3 rigMoveDir = noraCentre.position - xRRig.transform.position;
    //    float rigDistance = Vector3.Distance(noraCentre.position, xRRig.transform.position);
    //    while (rigDistance > 0.05f)
    //    {
    //        rigMoveDir = noraCentre.position - xRRig.transform.position;
    //        xRRig.transform.Translate(1 * Time.deltaTime * rigMoveDir);
    //        rigDistance = Vector3.Distance(noraCentre.position, xRRig.transform.position);
    //        yield return null;
    //    }
    //}




    //Coroutine 3: Meet snorkelers

    //Coroutine 4: Body swap and end
}

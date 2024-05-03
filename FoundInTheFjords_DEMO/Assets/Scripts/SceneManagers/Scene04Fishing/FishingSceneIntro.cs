using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit;

public class FishingSceneIntro : MonoBehaviour

{
    public GameObject nora;
    private MoveToObject noraMoveToTarget;
    public Transform clara;
    public Transform mom;
    public AudioSource claraAudioSource;
    public AudioSource momAudioSource;
    public AudioClip voiceover01;
    public AudioClip voiceover02;
    public AudioClip voiceover03;
    public AudioClip voiceover04;
    public AudioClip voiceover05;
    private float voiceover01Duration = 17.8f;
    private float voiceover02Duration = 6.4f;
    private float voiceover03Duration = 19.5f;
    private float voiceover04Duration = 27.3f;
    private float voiceover05Duration = 8.7f;
    public Canvas identifyThief;


    private void Awake()
    {
        noraMoveToTarget = nora.GetComponent<MoveToObject>();
        //Set target tranform as Clara
        noraMoveToTarget.targetTransform = clara;
        //Set other swim parameters
        noraMoveToTarget.speed = 2f;
        noraMoveToTarget.minDistance = 1.5f;
        noraMoveToTarget.rotationSpeed = 0.5f;
    }

    public IEnumerator Scene04Intro()
    {
        //start herring swimming
        SpawnEatableHerring.SH.spawnHerring = true;
        SpawnEatableHerring.SH.StartSpawning();

        //Move to Clara

        if (noraMoveToTarget != null)
        {
            noraMoveToTarget.distance = Vector3.Distance(noraMoveToTarget.targetTransform.position, noraMoveToTarget.transform.position);
            while (noraMoveToTarget.distance > noraMoveToTarget.minDistance)
            {
                noraMoveToTarget.MoveToMinimumDistance();
                //followMom.Follow();

                yield return null;
            }
        }

        

        //Play Voiceover 1
        claraAudioSource.PlayOneShot(voiceover01);
        yield return new WaitForSeconds(voiceover01Duration);
        //Play Voiceover 2
        claraAudioSource.PlayOneShot(voiceover02);
        yield return new WaitForSeconds(voiceover02Duration);
        //Play voiceover 3
        claraAudioSource.PlayOneShot(voiceover03);
        yield return new WaitForSeconds(voiceover03Duration);
        
        //Set target tranform as Clara
        noraMoveToTarget.targetTransform = mom;
        noraMoveToTarget.minDistance = 2.5f;
        
        //Swim to mom (set target and parameters)
        if (noraMoveToTarget != null)
        {
            //Recalculate distance
            noraMoveToTarget.distance = Vector3.Distance(noraMoveToTarget.targetTransform.position, noraMoveToTarget.transform.position);

            while (noraMoveToTarget.distance > noraMoveToTarget.minDistance)
            {
                noraMoveToTarget.MoveToMinimumDistance();
                //followMom.Follow();

                yield return null;
            }

            //rotate to align to mom

            while (noraMoveToTarget.transform.eulerAngles.y < noraMoveToTarget.targetTransform.eulerAngles.y - 2 || noraMoveToTarget.transform.eulerAngles.y > noraMoveToTarget.targetTransform.eulerAngles.y + 2)
            {
                noraMoveToTarget.RotateToAlign();
                yield return null;
                //xRRig.transform.rotation != moveToMom.targetTransform.rotation
            }
        }

        //Play Voiceover 4
        momAudioSource.PlayOneShot(voiceover04);
        yield return new WaitForSeconds(voiceover04Duration);
        //Play voiceover 5
        momAudioSource.PlayOneShot(voiceover05);
        yield return new WaitForSeconds(voiceover05Duration);
        //Show panels
        foreach (CanvasGroup panel in identifyThief.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(1.0f, 1.5f);
            panel.interactable = true;
            panel.blocksRaycasts = true;
        }

    }
}

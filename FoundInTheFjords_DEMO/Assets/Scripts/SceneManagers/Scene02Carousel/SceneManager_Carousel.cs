using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using FoundInTheFjordsSceneManager;

public class SceneManager_Carousel : SceneManager
{
    public CarouselSceneIntro carouselSceneIntro;
    public GameObject flockManager;
    public GameObject migrationInteractionSignifier;
    public GameObject ecosystemInteractionSignifier;
    public GameObject momInteractionSignifier;
    public GameObject flock;
    
    
   

    protected override void Awake()
    {
        base.Awake();
        //Start orca mom swim animation
        orcaMomAnimator.SetTrigger("Trigger_Swim");

        //Disable flock manager and flock
        flockManager.GetComponent<FlockManager_Circular>().enabled = false;
        flock.SetActive(false);

        //disable tailslap controller
        xRRig.GetComponent<TailslapController>().enabled = false;

        //Start Scene02IntroCoroutine
        StartCoroutine(carouselSceneIntro.Scene02Intro());

        migrationInteractionSignifier.SetActive(false);
        ecosystemInteractionSignifier.SetActive(false);
        momInteractionSignifier.SetActive(false);
        

    }

    
}

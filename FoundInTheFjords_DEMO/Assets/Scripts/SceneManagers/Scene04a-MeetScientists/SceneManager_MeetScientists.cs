using FoundInTheFjordsSceneManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneManager_MeetScientists : SceneManager
{
    public MeetScientistsSceneIntro meetScientistsSceneIntro;
    public GameObject momInteractionSignifier;
    public GameObject ladder;
    public GameObject leftController;
    public GameObject rightController;
    //public JellyInteractions orcaMomInteractions;

    protected override void Awake()
    {
        base.Awake();
        //start Scene04aIntroCoroutine
        StartCoroutine(meetScientistsSceneIntro.Scene04aIntro());
        //humpbackAnimation.StartSwim();
        //orcaMom.GetComponentInChildren<XRSimpleInteractable>().enabled = false;
        momInteractionSignifier.SetActive(false);
        ladder.SetActive(false);
        leftController.SetActive(false);
        rightController.SetActive(false);
        //xRRig.GetComponent<EatingController>().enabled = false;

        



    }

    
}

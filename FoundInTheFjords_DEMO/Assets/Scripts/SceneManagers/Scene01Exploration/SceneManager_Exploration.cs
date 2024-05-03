using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoundInTheFjordsSceneManager;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneManager_Exploration : SceneManager
{
    public ExplorationSceneIntro orientationSceneIntro;
    public GameObject leftController;
    public GameObject rightController;
    public GameObject momInteractionSignifier;
    public GameObject claraInteractionSignifier;




    protected override void Awake()
    {
        base.Awake();
        //Start Scene02IntroCoroutine
        StartCoroutine(orientationSceneIntro.Scene01Intro());
        //deactivate controller models
        leftController.SetActive(false);
        rightController.SetActive(false);
        momInteractionSignifier.SetActive(false);
        claraInteractionSignifier.SetActive(false);



    }
}

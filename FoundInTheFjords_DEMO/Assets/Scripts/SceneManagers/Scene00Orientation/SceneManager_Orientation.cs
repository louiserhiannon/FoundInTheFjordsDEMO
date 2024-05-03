using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoundInTheFjordsSceneManager;
public class SceneManager_Orientation : SceneManager
{
    public OrientationSceneIntro orientationSceneIntro;
    public GameObject leftController;
    public GameObject rightController;
    public GameObject claraInteractionSignifierActual;
    public GameObject claraInteractionSignifierReflected;


    protected override void Awake()
    {
        base.Awake();
        //Start Scene02IntroCoroutine
        StartCoroutine(orientationSceneIntro.Scene00Intro());
        //deactivate controller models
        leftController.SetActive(false);
        rightController.SetActive(false);
        
        claraInteractionSignifierActual.SetActive(false);
        claraInteractionSignifierReflected.SetActive(false);

    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using FoundInTheFjordsSceneManager;
public class SceneManager_Fishing : SceneManager
{
    public FishingSceneIntro fishingSceneIntro;
    public HumpbackSwimAnimation humpbackAnimation;
    public GameObject momInteractionSignifier;
    public ExploreFishingBoatArea testExplore;
    //public GameObject ladder;

    protected override void Awake()
    {
        base.Awake();
        //start Scene04IntroCoroutine
        StartCoroutine(fishingSceneIntro.Scene04Intro());
        humpbackAnimation.StartSwim();
        orcaMom.GetComponentInChildren<XRSimpleInteractable>().enabled = false;
        momInteractionSignifier.SetActive(false);
        //ladder.SetActive(false);

        //start test coroutine
        //StartCoroutine(testExplore.ExploreSurroundings01());



    }

}

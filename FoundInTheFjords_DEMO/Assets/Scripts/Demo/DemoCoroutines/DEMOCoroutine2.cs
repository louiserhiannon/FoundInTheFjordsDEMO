using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VisualScripting;
using System;
//using System.Diagnostics;

public class DEMOCoroutine2 : MonoBehaviour
{
    public AudioSource momAudioSource;
    public ParticleSystem momBubbles;
    public Animator momAnimator;
    public GameObject xRRig;
    public MoveToObject rotateMom;
    public Transform momTarget;
    public Transform xRRigBoundingBox;
    public AudioSource humpbackAudioSource;
    public HumpbackSwimAnimation humpbackAnimationController;
    public List<AudioClip> voiceoverClips;
    public GameObject humpback;
    public GameObject carouselTransform;
    public OceanMovement oceanMovement;
    public Transform groupTwoOrcaParent;
    [SerializeField] private List<Transform> groupTwoOrcas = new List<Transform>();
    private List<MoveToObject> groupTwoOrcaFlees = new List<MoveToObject>();
    //private List<MoveToObject> returnAllOrcas = new List<MoveToObject>();
    public List<Transform> groupOneOrcas;
    public List<Transform> allOrcas;
    public List<Transform> orcaRestartPositions;
    public List<Transform> returnToFamilyTargets;
    public MoveToObject humpbackSwimToBaitball;
    public Transform humpbackTurnTarget;
    public Transform humpbackEatTarget;
    public Transform humpbackFleeTarget;
    public Transform orcaFleeTarget;
    public ParticleSystem herringScales;
    public AudioClip humpbackOmnomnom;
    public GameObject highlightAnna;
    public GameObject highlightMagnus;
    public GameObject fishingBoat;
    public Transform herringNet;
    public GameObject zodiac;
    public Transform boatOceanBox;
    public Transform orcaTargetCentre;
    //public List<SetMoveToObjectParameters> setOrcaMoveParameters;
    public CanvasGroup chargeCanvas;
    public CanvasGroup eatFishInstructionalPanel;
    public CanvasGroup readyButton;
    public DEMOCoroutine3 coroutine03;
    private List<bool> orcasBackInPlace;
    private WaveManager waveManager;


    private void Awake()
    {
        
        highlightAnna.SetActive(false);
        highlightMagnus.SetActive(false);
        humpback.SetActive(false);
        fishingBoat.SetActive(false);
        zodiac.SetActive(false);
        orcasBackInPlace = new List<bool>();
        waveManager = boatOceanBox.GetComponentInChildren<WaveManager>();

    }

    public IEnumerator DEMOCoroutine02()
    {
        //Deactivate tailslap control
        //ActivateControlsDEMO.AC.DeActivateTailslapControls();
        ActivateControlsDEMO.AC.ActivateMovementControls();
        //Hide charge panel
        chargeCanvas.DOFade(0, 1);
        //Play clip 6.2
        momAudioSource.PlayOneShot(voiceoverClips[0]);
        momAnimator.SetTrigger("Trigger_Talk");
        momBubbles.Play();
        yield return new WaitForSeconds(voiceoverClips[0].length);
        momAnimator.SetTrigger("Trigger_StopTalk");
        momBubbles.Stop();
        //Play humpback Sound
        humpback.SetActive(true);
        humpbackAudioSource.PlayOneShot(voiceoverClips[1]);
        yield return new WaitForSeconds(voiceoverClips[1].length);
        momAudioSource.PlayOneShot(voiceoverClips[2]);
        momAnimator.SetTrigger("Trigger_Talk");
        momBubbles.Play();
        StartCoroutine(WaitForEndOfClip(voiceoverClips[2].length));
        //yield return new WaitForSeconds(voiceoverClips[2].length + 0.5f);
        //Activate humpback model and animation (humpback coroutine)

        humpbackSwimToBaitball.targetTransform = humpbackTurnTarget;
        humpbackAnimationController.StartSwim();
        humpbackSwimToBaitball.distance = Vector3.Distance(humpbackSwimToBaitball.targetTransform.position, humpbackSwimToBaitball.transform.position);
        while (humpbackSwimToBaitball.distance > humpbackSwimToBaitball.viewDistance)
        {
            humpbackSwimToBaitball.MoveToMinimumDistance();
            yield return null;
        }

        //Reparent Group 2 Orca and set them to swim away
        StartCoroutine(OrcaSwimAway());

        //humpback continues
        while (humpbackSwimToBaitball.distance > humpbackSwimToBaitball.minDistance)
        {
            humpbackSwimToBaitball.MoveToMinimumDistance();
            yield return null;
        }

        //humpback continues to surface
        StartCoroutine(Gulp());

        humpbackSwimToBaitball.targetTransform = humpbackEatTarget;
        humpbackSwimToBaitball.distance = Vector3.Distance(humpbackSwimToBaitball.targetTransform.position, humpbackSwimToBaitball.transform.position);
        Debug.Log(humpbackSwimToBaitball.distance);

        while (humpbackSwimToBaitball.distance > humpbackSwimToBaitball.minDistance)
        {
            humpbackSwimToBaitball.MoveToMinimumDistance();
            yield return null;
        }

        //Destroy herring (deactivate)
        for (int i = FlockManager_Circular.FM.numFlockers - 1; i > -1; i--)
        {
            if (i % 20 == 0)
            {

            }
            else
            {

                FlockManager_Circular.FM.allFlockers[i].SetActive(false);
                FlockManager_Circular.FM.allFlockers.RemoveAt(i);
            }
        }

        //start herring scales particle system
        herringScales.Play();

        StartCoroutine(HumpbackSwimAway());

        yield return new WaitForSeconds(4);
        

        //Restart oceanbox movement
        oceanMovement.isMoving = true;
        oceanMovement.waveManager = waveManager;
        momAnimator.SetTrigger("Trigger_Swim");

        //rotate mom and nora back to face forwards
        rotateMom.targetTransform = momTarget;
        StartCoroutine(RotateMomAndNora());

        

        //Move Group 1 and Group 2 orca into place
        for (int i  = 0; i < groupOneOrcas.Count; i++)
        {
            allOrcas.Add(groupOneOrcas[i]);
        }

        for (int i = 0; i < groupTwoOrcas.Count; i++)
        {
            allOrcas.Add(groupTwoOrcas[i]);
            
        }


        for (int i = 0; i < allOrcas.Count; i++)
        {
            //returnAllOrcas.Add(allOrcas[i].GetComponent<MoveToObject>());
            //Reset orca family positions
            allOrcas[i].transform.position = orcaRestartPositions[i].transform.position;
            allOrcas[i].transform.rotation = orcaRestartPositions[i].transform.rotation;
            //move back
            StartCoroutine(MoveOrcaBack(i));
        }

        
        //Play clip 8
        momAudioSource.PlayOneShot(voiceoverClips[3]);
        momAnimator.SetTrigger("Trigger_Talk");
        momBubbles.Play();
        StartCoroutine(WaitForEndOfClip(voiceoverClips[3].length));

        StartCoroutine(StartOscillation());
        
        yield return new WaitForSeconds(30);

        //highlight grandma in some way (placeholder - add interactivity sphere and activate/deactivate)
        var highlightGrandma = allOrcas[6].Find("InteractionSignifierPrefab");
        highlightGrandma.gameObject.SetActive(true);
        yield return new WaitForSeconds(13);
        highlightGrandma.gameObject.SetActive(false);
        //wait an appropriate number of seconds
        yield return new WaitForSeconds(7f);
        //Highlight Anna and Magnus
        highlightAnna.SetActive(true);
        highlightMagnus.SetActive(true);
        yield return new WaitForSeconds(10);
        highlightAnna.SetActive(false);
        highlightMagnus.SetActive(false);
        //Wait an appropriate number of seconds
        yield return new WaitForSeconds(4);

        //Play clip 9
        momAudioSource.PlayOneShot(voiceoverClips[4]);
        momAnimator.SetTrigger("Trigger_Talk");
        momBubbles.Play();
        StartCoroutine(WaitForEndOfClip(voiceoverClips[4].length));
        //Activate boats and herring (at right time)
        Debug.Log("ActivePoint 1 (boat) is at " + OceanMovement.OM.activePoints[0].position.z);
        Debug.Log("ActivePoint 2 (carousel) is at " + OceanMovement.OM.activePoints[1].position.z);
        Debug.Log("ActivePoint 3 (empty) is at " + OceanMovement.OM.activePoints[2].position.z);
        float fishingBoatTime = 0;
        while (boatOceanBox.position.z < 500 || boatOceanBox.position.z > 550)
        {
            fishingBoatTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("There is a pause of " + fishingBoatTime + " seconds before the fishing boat activates");

        fishingBoat.SetActive(true);
        zodiac.SetActive(true);

        for (int i = 0; i < allOrcas.Count; i++)
        {
            if (!allOrcas[i].TryGetComponent<SetMoveToObjectParameters>(out SetMoveToObjectParameters moveParameters))
            {
                allOrcas[i].AddComponent<SetMoveToObjectParameters>().enabled = false;
            }

        }
        //Decelerate ocean boxes to stationary (at right time)
        while (boatOceanBox.position.z > 120)
        {
            yield return null;
        }
        oceanMovement.isMoving = false;
        //Activate fishing scene orca movement (random) for group 1 and 2 orca
        
        for (int i = 0; i < returnToFamilyTargets.Count; i++)
        {
            returnToFamilyTargets[i].GetComponent<ResetTransform>().enabled = false;
        }

        OrcaOscillationController.OOC.isOscillating = false;
        
        for (int i = 0;i < allOrcas.Count;i++)
        {
            var startRandomMotion = allOrcas[i].GetComponent<SetMoveToObjectParameters>();
            startRandomMotion.orcaTargetCentre = orcaTargetCentre;
            startRandomMotion.enabled = true;
            startRandomMotion.setParameters = true;
        }
        
        //start herring swimming
        SpawnEatableHerring.SH.spawnHerring = true;
        
        SpawnEatableHerring.SH.StartSpawning();
        //Mom and player swim to net
        xRRigBoundingBox.SetParent(rotateMom.transform, true);
        Debug.Log("xrRig should be parented to orca mom");
        yield return new WaitForSeconds(2);
        rotateMom.targetTransform = herringNet.transform;
        rotateMom.speed = 3f;
        rotateMom.CalculateDistance();
        rotateMom.minDistance = 17;

        while (rotateMom.distance > rotateMom.minDistance)
        {
            rotateMom.MoveToMinimumDistance();
            yield return null;
        }

        momAnimator.SetTrigger("Trigger_StopSwim");
       

        //Play Clip 10
        momAudioSource.PlayOneShot(voiceoverClips[5]);
        momAnimator.SetTrigger("Trigger_Talk");
        momBubbles.Play();
        yield return new WaitForSeconds(voiceoverClips[5].length);
        //Play clip 11
        momAudioSource.PlayOneShot(voiceoverClips[6]);
        yield return new WaitForSeconds(voiceoverClips[6].length);
        momAnimator.SetTrigger("Trigger_StopTalk");
        momBubbles.Stop();
        Debug.Log("instructional panel should be visible");
        //Show fish eating instructional panel
        eatFishInstructionalPanel.DOFade(1, 1);
        eatFishInstructionalPanel.blocksRaycasts = true;
        yield return new WaitForSeconds(4);
        //Activate Ready button
        readyButton.DOFade(1, 1);
        readyButton.blocksRaycasts = true;
        readyButton.interactable = true;
        ActivateControlsDEMO.AC.ActivateEatControls();
        EatingControllerDEMO.ECDemo.herringLifetime = 45;




        ////fake transition to coroutine 3
        //yield return new WaitForSeconds(3);
        ////Hide fish eating instructional panel
        //eatFishInstructionalPanel.DOFade(0, 1);
        ////Activate Ready button
        //readyButton.DOFade(0, 1);
        //StartCoroutine(coroutine03.DEMOCoroutine03());
        //Debug.Log("Next coroutine should trigger");
    }

    public IEnumerator OrcaSwimAway()
    {

        for (int i = 0; i < CarouselManager.CM.allAxes.Count; i++)
        {
            //stop axis rotation
            CarouselManager.CM.allAxes[i].GetComponent<RotateCarouselAxis>().isRotating = false;
            //remove model from parent transform
            var model = CarouselManager.CM.allAxes[i].transform.Find("Orca_Shoaling_Animated");
            model.SetParent(groupTwoOrcaParent);
            //add to new list
            groupTwoOrcas.Add(model);
        }                                                                                                                                                                                                                                                                                                                                                                                                                


        for (int i = 0; i < groupTwoOrcas.Count; i++)
        {
            //stop carousel rotation
            groupTwoOrcas[i].GetComponent<CarouselMotion>().isCarouselFeeding = false;
        }



        for (int i = 0; i < groupTwoOrcas.Count; i++)
        {
            var flee = groupTwoOrcas[i].GetComponent<MoveToObject>();
            groupTwoOrcaFlees.Add(flee);

            //set target transform
            groupTwoOrcaFlees[i].targetTransform = orcaFleeTarget;

            //calculate distance
            groupTwoOrcaFlees[i].distance = Vector3.Distance(groupTwoOrcaFlees[i].targetTransform.position, groupTwoOrcaFlees[i].transform.position);
        }


        while (groupTwoOrcaFlees[0].distance > groupTwoOrcaFlees[0].minDistance)
        {
            for (int i = 0; i < groupTwoOrcaFlees.Count; i++)
            {
                groupTwoOrcaFlees[i].speed = UnityEngine.Random.Range(2.5f, 3.5f);
                groupTwoOrcaFlees[i].MoveToMinimumDistance();
            }
            yield return null;
        }



    }

    public IEnumerator Gulp()
    {
        yield return new WaitForSeconds(1.9f);
        humpbackAnimationController.StopSwim();
        yield return new WaitForSeconds(0.1f);
        humpbackAnimationController.StartGulp();
        humpbackAudioSource.PlayOneShot(humpbackOmnomnom);
        // aya memo: can add modifier to volume using `, 2`
    }

    public IEnumerator RotateMomAndNora()
    {
        Vector3 direction = rotateMom.targetTransform.position - rotateMom.transform.position;
        float angle = Quaternion.LookRotation(direction).eulerAngles.y;
        rotateMom.rotationSpeed = 0.5f;

        while (rotateMom.transform.eulerAngles.y < angle - 2 || rotateMom.transform.eulerAngles.y > angle + 2)
        {
            //Debug.Log("Mom should be rotating");
            rotateMom.RotateToFaceObject();
            yield return null;
        }

        xRRigBoundingBox.SetParent(null, true);
    }


    public IEnumerator MoveOrcaBack(int index)
    {
        var move = allOrcas[index].GetComponent<MoveToObject>();
        move.targetTransform = returnToFamilyTargets[index];
        move.speed = UnityEngine.Random.Range(2.0f, 3.0f);
        Debug.Log("Orca " + index + " has a speed of " +  move.speed);
        move.CalculateDistance();

        while (move.distance > move.minDistance)
        {
            move.TranslateToMinimumDistance();
            yield return null;
        }

        while (move.transform.eulerAngles.y < move.targetTransform.eulerAngles.y - 2 || move.transform.eulerAngles.y > move.targetTransform.eulerAngles.y + 2)
        {
            move.RotateToAlign();
            yield return null;
        }

        if (!allOrcas[index].TryGetComponent<OrcaOscillation>(out OrcaOscillation oscillationParameters))
        {
            allOrcas[index].AddComponent<OrcaOscillation>();
        }
        allOrcas[index].GetComponent<OrcaOscillation>().SetOscillationParameters();
        orcasBackInPlace.Add(true);

    }

    public IEnumerator HumpbackSwimAway()
    {
        humpbackSwimToBaitball.targetTransform = humpbackFleeTarget;
        humpbackSwimToBaitball.distance = Vector3.Distance(humpbackSwimToBaitball.targetTransform.position, humpbackSwimToBaitball.transform.position);
        humpbackAnimationController.StartSwim();

        while (humpbackSwimToBaitball.distance > humpbackSwimToBaitball.minDistance)
        {
            humpbackSwimToBaitball.MoveToMinimumDistance();
            yield return null;
        }

        //Deactivate carousel transform and humpback
        carouselTransform.SetActive(false);
        humpback.SetActive(false);
    }

    public IEnumerator StartOscillation()
    {
        float time = 0;
        while (orcasBackInPlace.Count < 8)
        {
            time += Time.deltaTime;
            yield return null;
        }
        Debug.Log("it takes " + time + " seconds for the 8 orca to get back into place");
        OrcaOscillationController.OOC.isOscillating = true;
    }

    private IEnumerator WaitForEndOfClip(float duration)
    {
        yield return new WaitForSeconds(duration);
        momAnimator.SetTrigger("Trigger_StopTalk");
        momBubbles.Stop();
    }
}

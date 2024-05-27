using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;


public class DEMOCoroutine3 : MonoBehaviour
{
    public CanvasGroup herringCounterCanvas;
    public GameObject orcaMom;
    private Animator orcaMomAnimator;
    private ParticleSystem momBubbles;
    public GameObject xRRig;
    public MoveToObject moveToClara;
    public Transform claraTargetTransform;
    public Transform rotateToClara;
    public Transform xRRigBoundingBox;
    public AudioSource claraAudioSource;
    public AudioSource momAudioSource;
    public AudioSource driverAudioSource;
    public AudioSource backgroundMusic;
    public AudioSource shipDrone;
    public AudioSource shipClanging;
    public DEMOCoroutine2 coroutine2;
    public List<AudioClip> voiceoverClips;
    public Transform snorkeler01Target;
    public Transform snorkeler01;
    public Transform snorkeler02;
    public Transform snorkeler03;
    public float speed;
    public float rotationSpeed;
    public Transform noraRecentreTarget;
    public Transform snorkeler1BodySwapTarget;
    public ActivateIdentitySwap activateIdentitySwap;
    public DEMOCoroutine4 coroutine04;
    private List<bool> slidings;
    public CanvasGroup interactWithSnorkelerPanel;
    public JellyTalkAnimation claraTalkAnimation;
    //additional variables for testing
    //public GameObject fishingBoat;
    //public GameObject Zodiac;
    //public Transform oceanbox01;
    //public Transform oceanbox02;
    //public Transform oceanbox03;


    private void Awake()
    {
        orcaMomAnimator = orcaMom.GetComponent<Animator>();
        momBubbles = orcaMom.GetComponentInChildren<ParticleSystem>();
        slidings = new List<bool>();
        for (int i = 0; i < 3; i++)
        {
            slidings.Add(false);
        }

        ////TEST: fake final position of ocean boxes
        //oceanbox01.position = new Vector3(0, 2, 58);
        //oceanbox02.position = new Vector3(0, 2, -192);
        //oceanbox03.position = new Vector3(0,2,308);

        ////Activate fishingboat and inflatable boat
        //fishingBoat.SetActive(true);
        //Zodiac.SetActive(true);
        ////Set nora as child of mom
        //xRRig.transform.SetParent(orcaMom.transform);  

        //fake start into this coroutine
        //StartCoroutine(DEMOCoroutine03());


    }

    public void StartNextSection()
    {
        StartCoroutine(DEMOCoroutine03());
    }

    public IEnumerator DEMOCoroutine03()
    {

        //Deactivate eat controls and fade music
        backgroundMusic.DOFade(0, 2);
        ActivateControlsDEMO.AC.DeActivateEatControls(); //For test only
        yield return new WaitForSeconds(3);
        herringCounterCanvas.DOFade(0, 1);
        //Play Clip 13
        //Debug.Log("Coroutine should be running");
        momAudioSource.PlayOneShot(voiceoverClips[0]);
        orcaMomAnimator.SetTrigger("Trigger_Talk");
        momBubbles.Play();
        yield return new WaitForSeconds(voiceoverClips[0].length);
        orcaMomAnimator.SetTrigger("Trigger_StopTalk");
        momBubbles.Stop();

        //Gradually fade ship sounds
        shipDrone.DOFade(0, 15);
        shipClanging.DOFade(0, 15);
        
        ////Mom and player swim to Clara
        //xRRig.transform.SetParent(orcaMom.transform, true);
        orcaMomAnimator.SetTrigger("Trigger_Swim");
        moveToClara.targetTransform = claraTargetTransform;
        moveToClara.minDistance = 6f;
        moveToClara.speed = 3.5f;
        moveToClara.rotationSpeed = 0.5f;
        moveToClara.CalculateDistance();
        while (moveToClara.distance > moveToClara.minDistance)
        {
            moveToClara.MoveToMinimumDistance();
            yield return null;
        }

        
        //rotate to face Actual Clara
        moveToClara.targetTransform = rotateToClara;
        Vector3 direction = moveToClara.targetTransform.position - moveToClara.transform.position;
        float angle = Quaternion.LookRotation(direction).eulerAngles.y;

        while (moveToClara.transform.eulerAngles.y < angle - 2 || moveToClara.transform.eulerAngles.y > angle + 2)
        {
            //Debug.Log("Mom should be rotating");
            moveToClara.RotateToFaceObject();
            yield return null;
        }

        orcaMomAnimator.SetTrigger("Trigger_StopSwim");
        //Play Clip 14.1 and make it obvious that Clara is talking
        xRRigBoundingBox.SetParent(null);
        claraAudioSource.PlayOneShot(voiceoverClips[1]);
        claraTalkAnimation.isTalking = true;
        claraTalkAnimation.GetTalking();
        yield return new WaitForSeconds(voiceoverClips[1].length);
        claraTalkAnimation.isTalking = false;
        //Play Clip 14.2
        driverAudioSource.PlayOneShot(voiceoverClips[2]);
        yield return new WaitForSeconds(voiceoverClips[2].length);
        
        //Deactivate other orca and stop herring spawning
        for (int i = 0; i < coroutine2.allOrcas.Count; i++)
        {
            coroutine2.allOrcas[i].gameObject.SetActive(false);
        }

        SpawnEatableHerring.SH.spawnHerring = false;
        //for (int i = 0; i < HerringSpawner.HS.herringListFishing.Count; i++)
        //{
        //    HerringSpawner.HS.herringListFishing[i].SetActive(false);
        //}

        //Start Snorkeler Animation
        snorkeler01Target.eulerAngles = new Vector3(snorkeler01Target.eulerAngles.x, 270, snorkeler01Target.eulerAngles.z);
        StartCoroutine(SnorkelerSlide(snorkeler01, 0));
        yield return new WaitForSeconds(1);
        StartCoroutine(SnorkelerSlide(snorkeler02, 1));
        yield return new WaitForSeconds(1);
        StartCoroutine(SnorkelerSlide(snorkeler03, 2));
        yield return new WaitForSeconds(1);

        //Play Clip 14.3 and make it obvious that Clara is talking
        claraAudioSource.PlayOneShot(voiceoverClips[3]);
        claraTalkAnimation.isTalking = true;
        claraTalkAnimation.GetTalking();
        StartCoroutine(WaitForClaraClipToEnd(voiceoverClips[3].length));
        //yield return new WaitForSeconds(voiceoverClips[3].length);
        //Deactivate move controls and move player to surface (and rotate to be parallel to boat)
        ActivateControlsDEMO.AC.DeActivateMovementControls();
        var recentreNora = xRRig.GetComponent<MoveToObject>();
        recentreNora.targetTransform = noraRecentreTarget;
        recentreNora.CalculateDistance();
        while (recentreNora.distance > recentreNora.minDistance)
        {
            recentreNora.MoveToMinimumDistance();
            yield return null;
        }

        while (xRRig.transform.eulerAngles.y < recentreNora.targetTransform.eulerAngles.y - 2 || xRRig.transform.eulerAngles.y > recentreNora.targetTransform.eulerAngles.y + 2)
        {
            recentreNora.RotateToAlign();
            yield return null;
        }

        while (slidings[0] || slidings[1] || slidings[2])
        {
            yield return null;
        }
        //Move snorkeler 1 to face player
        var moveToFaceNora = snorkeler01.GetComponent<MoveToObject>();
        moveToFaceNora.targetTransform = xRRig.transform;
        moveToFaceNora.CalculateDistance();
        moveToFaceNora.minDistance = 1.5f;
        while(moveToFaceNora.distance > moveToFaceNora.minDistance)
        {
            moveToFaceNora.MoveToMinimumDistance();
            yield return null;
        }

        Vector3 faceDirection = xRRig.transform.position - snorkeler01.position;
        float faceAngle = Quaternion.LookRotation(faceDirection).eulerAngles.y;

        while (snorkeler01.eulerAngles.y < faceAngle - 2 || snorkeler01.eulerAngles.y > faceAngle + 2)
        {
            //Debug.Log("Mom should be rotating");
            moveToFaceNora.RotateToFaceObject();
            yield return null;
        }
        //Vector3 targetPosition = xRRig.transform.position - xRRig.transform.forward * 1.5f - xRRig.transform.up * 1.15f;
        //Vector3 finalDirection = xRRig.transform.forward;

        //while (snorkeler01.position.z < targetPosition.z - 0.05 || snorkeler01.position.z > targetPosition.z + 0.05)
        //{
        //    snorkeler01.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        //    yield return null;
        //}

        //claraAudioSource.PlayOneShot(voiceoverClips[4]);

        //while (snorkeler01.eulerAngles.y > xRRig.transform.eulerAngles.y + 5 || snorkeler01.eulerAngles.y < xRRig.transform.eulerAngles.y - 5)
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(finalDirection), rotationSpeed * Time.deltaTime);
        //    yield return null;
        //}

        //Activate head movement mirror and counter

        activateIdentitySwap.snorkelerActive = true;
        interactWithSnorkelerPanel.DOFade(1,1);
        yield return new WaitForSeconds(4);
        interactWithSnorkelerPanel.DOFade(0, 1);

        //fake transition to coroutine 4
        //StartCoroutine(coroutine04.SwitchBodies());
    }

    public IEnumerator SnorkelerSlide(Transform snorkeler, int index)
    {

        slidings[index] = true;
        snorkeler.GetComponent<Animator>().SetTrigger("Trigger_Slide");
        yield return new WaitForSeconds(0.5f);
        float time = 0;
        while (time < 1.8f)
        {
            snorkeler.Translate((snorkeler.transform.right - snorkeler.transform.up) * speed * Time.deltaTime, Space.Self);
            time += Time.deltaTime;
            yield return null;
        }

        while (snorkeler.position.y < 0.1f)
        {
            snorkeler.Translate((snorkeler.transform.forward + snorkeler.transform.up) * speed);
            yield return null;
        }

        var moveSnorkeler = snorkeler.GetComponent<MoveToObject>();
        moveSnorkeler.distance = Vector3.Distance(snorkeler.position, moveSnorkeler.targetTransform.position);
        while (moveSnorkeler.distance > moveSnorkeler.minDistance)
        {
            moveSnorkeler.MoveToMinimumDistance();
            yield return null;
        }
        while (moveSnorkeler.transform.eulerAngles.y > moveSnorkeler.targetTransform.eulerAngles.y + 5 || moveSnorkeler.transform.eulerAngles.y < moveSnorkeler.targetTransform.eulerAngles.y - 5)
        {
            moveSnorkeler.RotateToAlign();
            yield return null;
        }

        slidings[index] = false;

    }

    private IEnumerator WaitForClaraClipToEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        claraTalkAnimation.isTalking = false;
    }
}

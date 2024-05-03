using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using JetBrains.Annotations;

public class DEMOCoroutine4 : MonoBehaviour
{
    public float rotateSpeed = 0.5f;
    public Transform orcaMom;
    public Transform orcaMomFinal;
    public Transform xRRig;
    public Transform clara;
    public Transform claraFinal;
    public ParticleSystem swirl;
    public ParticleSystem scales;
    public FogManager fogManager;
    public List<AudioClip> voiceoverClips;
    public AudioClip swirlMusic;
    public MoveToObject moveToLadder;
    public Transform ladderTargetTransform;
    public GameObject leftSnorkelerController;
    public GameObject rightSnorkelerController;
    public GameObject leftFinController;
    public GameObject rightFinController;
    public GameObject snorkeler;
    public GameObject noraPrefab;
    public GameObject cameraFilter;
    public Transform identitySwapTunnel;
    //public Canvas areYouReady;
    public GameObject snorkeler02;
    public GameObject snorkeler03;
    public GameObject snorkeler02BackToBoat;
    public GameObject snorkeler03BackToBoat;
    public AudioSource backgroundMusic;
    public AudioSource swirlAudioSource;
    public AudioSource claraAudioSource;
    public AudioSource momAudioSource;
    //private float voiceover41Duration = 3.2f;
    //private float voiceover42Duration = 14.8f;
    //private float voiceover43Duration = 14.0f;
    private GameObject nora;
    private Material cameraFilterMaterial;
    private Color cameraFilterColour;
    //private float cameraFilterAlpha;
    private Color darkness;
    public CanvasGroup grabLadder;
    public GameObject ladder;
    public GameObject leftLadderGrabPoint;
    public GameObject rightLadderGrabPoint;

    private void Awake()
    {
        cameraFilterMaterial = cameraFilter.GetComponent<MeshRenderer>().material;
        cameraFilterColour = cameraFilterMaterial.color;
        cameraFilterColour.a = cameraFilterMaterial.color.a;
        //cameraFilterAlpha = cameraFilterColour.a;
        darkness = Color.black;
        darkness.a = 1;
        //swirlAudioSource = GetComponent<AudioSource>();
        leftSnorkelerController.SetActive(false);
        rightSnorkelerController.SetActive(false);


    }

    public IEnumerator SwitchBodies()
    {
        snorkeler.transform.SetParent(null);
        //UNCOMMENT WHEN TESTING IN VR
        if (snorkeler.transform.localEulerAngles.z < 90)
        {
            while (snorkeler.transform.localEulerAngles.z > 0 && snorkeler.transform.localEulerAngles.z < 90)
            {
                float angle = snorkeler.transform.localEulerAngles.z;
                angle -= rotateSpeed * Time.deltaTime;
                snorkeler.transform.localEulerAngles = new Vector3(snorkeler.transform.localEulerAngles.x, snorkeler.transform.localEulerAngles.y, angle);
                yield return null;
            }
        }
        else
        {
            while (snorkeler.transform.localEulerAngles.z < 360 && snorkeler.transform.localEulerAngles.z > 270)
            {
                float angle = snorkeler.transform.localEulerAngles.z;
                angle += rotateSpeed * Time.deltaTime;
                snorkeler.transform.localEulerAngles = new Vector3(snorkeler.transform.localEulerAngles.x, snorkeler.transform.localEulerAngles.y, angle);
                yield return null;
            }
        }
        Debug.Log("ReadyForIdentitySwap");
        snorkeler.transform.localEulerAngles = new Vector3(snorkeler.transform.localEulerAngles.x, snorkeler.transform.localEulerAngles.y, 0);

        //instantiate orca model and hide
        nora = Instantiate(noraPrefab, snorkeler.transform.position - snorkeler.transform.forward * 1.2f - snorkeler.transform.up * 0.1f, snorkeler.transform.rotation);
        Debug.Log("NoraShouldBeInstantiated");
        nora.SetActive(false);

        fogManager.enabled = false;
        //Start swirl animation
        identitySwapTunnel.SetParent(null);
        swirl.Play();
        //Start swirl music
        swirlAudioSource.Play(); //attach swirl music to this audio source
        backgroundMusic.Stop();
        yield return new WaitForSeconds(1.5f);
        //start herring scales animation
        scales.Play();
        //Wait a bit
        yield return new WaitForSeconds(3.0f);

        //Fade lights down
        cameraFilterMaterial.DOColor(darkness, 0.5f);
        //cameraFilterMaterial.DOFade(1, 0.5f);
        
        
        yield return new WaitForSeconds(0.6f);

        //Disable snorkeler model
        snorkeler.SetActive(false);
        //Enable orca model
        nora.SetActive(true);

        //switch controller prefabs
        leftSnorkelerController.SetActive(true);
        rightSnorkelerController.SetActive(true);
        leftFinController.SetActive(false);
        rightFinController.SetActive(false);

        //return other two snorkelers to boat and move Mom back
        snorkeler02.SetActive(false);
        snorkeler03.SetActive(false);
        snorkeler02BackToBoat.SetActive(true);
        snorkeler03BackToBoat.SetActive(true);
        orcaMom.position = orcaMomFinal.position;
        orcaMom.rotation = orcaMomFinal.rotation;
        clara.position = claraFinal.position;

        //pause
        //yield return new WaitForSeconds(0.5f);

        //fade lights up
        cameraFilterMaterial.DOColor(cameraFilterColour, 1);
        //cameraFilterMaterial.DOFade(cameraFilterAlpha, 1);
        yield return new WaitForSeconds(2.5f);

        //Fade lights down
        cameraFilterMaterial.DOColor(darkness, 0.5f);
        //cameraFilterMaterial.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.6f);
        //Disable snorkeler model
        snorkeler.SetActive(true);
        //Enable orca model
        nora.SetActive(false);
        //switch controller prefabs
        leftSnorkelerController.SetActive(false);
        rightSnorkelerController.SetActive(false);
        leftFinController.SetActive(true);
        rightFinController.SetActive(true);

        ////pause
        //yield return new WaitForSeconds(0.8f);

        //fade lights up
        cameraFilterMaterial.DOColor(cameraFilterColour, 1);
        //cameraFilterMaterial.DOFade(cameraFilterAlpha, 1);
        yield return new WaitForSeconds(2.5f);

        //Fade lights down
        cameraFilterMaterial.DOColor(darkness, 0.5f);
        //cameraFilterMaterial.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.6f);
        //Disable snorkeler model
        snorkeler.SetActive(false);
        //Enable orca model
        nora.SetActive(true);
        //switch controller prefabs
        leftSnorkelerController.SetActive(true);
        rightSnorkelerController.SetActive(true);
        leftFinController.SetActive(false);
        rightFinController.SetActive(false);

        //pause
        //yield return new WaitForSeconds(0.8f);

        //fade lights up
        cameraFilterMaterial.DOColor(cameraFilterColour, 1);
        //cameraFilterMaterial.DOFade(cameraFilterAlpha, 1);
        yield return new WaitForSeconds(2.5f);

        //Fade lights down
        cameraFilterMaterial.DOColor(darkness, 0.5f);
        //cameraFilterMaterial.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.6f);
        //Disable snorkeler model
        snorkeler.SetActive(true);
        //Enable orca model
        nora.SetActive(false);
        //switch controller prefabs
        leftSnorkelerController.SetActive(false);
        rightSnorkelerController.SetActive(false);
        leftFinController.SetActive(true);
        rightFinController.SetActive(true);

        //pause
       // yield return new WaitForSeconds(0.8f);

        //fade lights up
        cameraFilterMaterial.DOColor(cameraFilterColour, 1);
        //cameraFilterMaterial.DOFade(cameraFilterAlpha, 1);
        yield return new WaitForSeconds(2.5f);
        //Fade lights down
        cameraFilterMaterial.DOColor(darkness, 0.5f);
        //cameraFilterMaterial.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.6f);
        //Disable snorkeler model
        snorkeler.SetActive(false);
        //Enable orca model
        nora.SetActive(true);
        //switch controller prefabs
        leftSnorkelerController.SetActive(true);
        rightSnorkelerController.SetActive(true);
        leftFinController.SetActive(false);
        rightFinController.SetActive(false);

        //activate ladder
        ladder.SetActive(true);
        leftLadderGrabPoint.SetActive(false);
        rightLadderGrabPoint.SetActive(false);

        //pause
        //yield return new WaitForSeconds(0.8f);

        //fade lights up
        cameraFilterMaterial.DOColor(cameraFilterColour, 1);
        //cameraFilterMaterial.DOFade(cameraFilterAlpha, 1);
        yield return new WaitForSeconds(2.5f);

        fogManager.enabled = true;
        swirl.Stop();
        scales.Stop();
        swirlAudioSource.DOFade(0, 3);

        yield return new WaitForSeconds(5);

        //move to face mom and Clara
        Vector3 faceTarget = clara.position - xRRig.position;
        float faceAngle = Quaternion.LookRotation(faceTarget).eulerAngles.y;
        
        moveToLadder.targetTransform = clara;

        while (xRRig.eulerAngles.y < faceAngle - 2 || xRRig.eulerAngles.y > faceAngle + 2)
        {
            //Debug.Log("Mom should be rotating");
            moveToLadder.RotateToFaceObject();
            yield return null;
        }


        //voiceover 17.1
        claraAudioSource.PlayOneShot(voiceoverClips[0]);
        
        
        yield return new WaitForSeconds(voiceoverClips[0].length);

        faceTarget = orcaMom.position - xRRig.position;
        faceAngle = Quaternion.LookRotation(faceTarget).eulerAngles.y;
        moveToLadder.targetTransform = orcaMom;

        while (xRRig.eulerAngles.y < faceAngle - 2 || xRRig.eulerAngles.y > faceAngle + 2)
        {
            //Debug.Log("Mom should be rotating");
            moveToLadder.RotateToFaceObject();
            yield return null;
        }

        //goodbye from Mom
        momAudioSource.PlayOneShot(voiceoverClips[1]);
        yield return new WaitForSeconds(voiceoverClips[1].length);

        //move to zodiac ladder
        moveToLadder.targetTransform = ladderTargetTransform;
        moveToLadder.minDistance = 0.2f;
        while (moveToLadder.distance > moveToLadder.minDistance)
        {
            moveToLadder.MoveToMinimumDistance();
            yield return null;
        }

        while (moveToLadder.transform.eulerAngles.y < moveToLadder.targetTransform.eulerAngles.y - 2 || moveToLadder.transform.eulerAngles.y > moveToLadder.targetTransform.eulerAngles.y + 2)
        {
            moveToLadder.RotateToAlign();
            yield return null;
        }

        //activate climb mechanic
        grabLadder.DOFade(1,1);
        leftLadderGrabPoint.SetActive(true);
        rightLadderGrabPoint.SetActive(true);

    }

}

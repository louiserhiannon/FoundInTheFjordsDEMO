using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;
using JetBrains.Annotations;

public class IdentitySwap : MonoBehaviour
{
    public float rotateSpeed = 0.5f;
    public ParticleSystem swirl;
    public ParticleSystem scales;
    public FogManager fogManager;
    public AudioClip voiceover41;
    public AudioClip voiceover42;
    public AudioClip voiceover43;
    public AudioClip swirlMusic;
    public GameObject leftSnorkelerController;
    public GameObject rightSnorkelerController;
    public GameObject leftFinController;
    public GameObject rightFinController;
    public GameObject snorkeler;
    public GameObject noraPrefab;
    public GameObject cameraFilter;
    public Canvas areYouReady;
    public Transform snorkeler02;
    public Transform snorkeler03;
    public Transform snorkeler02BackToBoat;
    public Transform snorkeler03BackToBoat;
    public AudioSource backgroundMusic;


    private AudioSource audioSource;
    //private float voiceover41Duration = 3.2f;
    private float voiceover42Duration = 14.8f;
    private float voiceover43Duration = 14.0f;
    private GameObject nora;
    private Material cameraFilterMaterial;
    private Color cameraFilterColour;
    private float cameraFilterAlpha;
    private void Awake()
    {
        cameraFilterMaterial = cameraFilter.GetComponent<MeshRenderer>().material;
        cameraFilterColour = cameraFilterMaterial.color;
        cameraFilterAlpha = cameraFilterColour.a;
        audioSource = GetComponent<AudioSource>();
        leftSnorkelerController.SetActive(false);
        rightSnorkelerController.SetActive(false);
        

    }

    public IEnumerator SwitchBodies()
    {

        

        //snorkeler.transform.SetParent(null);
        if (snorkeler.transform.eulerAngles.z < 90)
        {
            while (snorkeler.transform.eulerAngles.z > 0 && snorkeler.transform.eulerAngles.z < 90)
            {
                float angle = snorkeler.transform.eulerAngles.z;
                angle -= rotateSpeed * Time.deltaTime;
                snorkeler.transform.eulerAngles = new Vector3(snorkeler.transform.eulerAngles.x, snorkeler.transform.eulerAngles.y, angle);
                yield return null;
            }
        }
        else
        {
            while (snorkeler.transform.eulerAngles.z < 360 && snorkeler.transform.eulerAngles.z > 270)
            {
                float angle = snorkeler.transform.eulerAngles.z;
                angle += rotateSpeed * Time.deltaTime;
                snorkeler.transform.eulerAngles = new Vector3(snorkeler.transform.eulerAngles.x, snorkeler.transform.eulerAngles.y, angle);
                yield return null;
            }
        }

        snorkeler.transform.eulerAngles = new Vector3(snorkeler.transform.eulerAngles.x, snorkeler.transform.eulerAngles.y, 0);

        //instantiate orca model and hide
        nora = Instantiate(noraPrefab, snorkeler.transform.position - snorkeler.transform.forward * 1.2f - snorkeler.transform.up * 0.1f, snorkeler.transform.rotation);
        nora.SetActive(false);

        fogManager.enabled = false;
        //Start swirl animation
        swirl.Play();
        //Start swirl music
        audioSource.PlayOneShot(swirlMusic, 0.4f);
        backgroundMusic.Stop();
        yield return new WaitForSeconds(1.5f);
        //start herring scales animation
        scales.Play();
        //Wait a bit
        yield return new WaitForSeconds(3.0f);
        //voiceover 41
        audioSource.PlayOneShot(voiceover41);
        //yield return new WaitForSeconds(voiceover41Duration);

        //Fade lights down
        cameraFilterMaterial.DOColor(Color.black, 0.5f);
        cameraFilterMaterial.DOFade(1, 0.5f);
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

        //return other two snorkelers to boat
        //stop animation
        snorkeler02.GetComponent<Animator>().SetTrigger("Trigger_StopSwim");
        snorkeler03.GetComponent<Animator>().SetTrigger("Trigger_StopSwim");
        //move back to boat
        Vector3 targetPosition02 = snorkeler02BackToBoat.position;
        Vector3 targetRotation02 = snorkeler02BackToBoat.eulerAngles;
        snorkeler02.position = targetPosition02;
        snorkeler02.eulerAngles = targetRotation02;
        Vector3 targetPosition03 = snorkeler03BackToBoat.position;
        Vector3 targetRotation03 = snorkeler03BackToBoat.eulerAngles;
        snorkeler03.position = targetPosition03;
        snorkeler03.eulerAngles = targetRotation03;

        //pause
        //yield return new WaitForSeconds(1.2f);

        //fade lights up
        cameraFilterMaterial.DOColor(cameraFilterColour, 1);
        cameraFilterMaterial.DOFade(cameraFilterAlpha, 1);
        yield return new WaitForSeconds(1.1f);

        //Fade lights down
        cameraFilterMaterial.DOColor(Color.black, 0.5f);
        cameraFilterMaterial.DOFade(1, 0.5f);
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
        //yield return new WaitForSeconds(1.2f);

        //fade lights up
        cameraFilterMaterial.DOColor(cameraFilterColour, 1);
        cameraFilterMaterial.DOFade(cameraFilterAlpha, 1);
        yield return new WaitForSeconds(1.1f);

        //Fade lights down
        cameraFilterMaterial.DOColor(Color.black, 0.5f);
        cameraFilterMaterial.DOFade(1, 0.5f);
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
        //yield return new WaitForSeconds(1.2f);

        //fade lights up
        cameraFilterMaterial.DOColor(cameraFilterColour, 1);
        cameraFilterMaterial.DOFade(cameraFilterAlpha, 1);
        yield return new WaitForSeconds(1.2f);

        //Fade lights down
        cameraFilterMaterial.DOColor(Color.black, 0.5f);
        cameraFilterMaterial.DOFade(1, 0.5f);
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
        //yield return new WaitForSeconds(1.2f);

        //fade lights up
        cameraFilterMaterial.DOColor(cameraFilterColour, 1);
        cameraFilterMaterial.DOFade(cameraFilterAlpha, 1);
        yield return new WaitForSeconds(1.1f);
        //Fade lights down
        cameraFilterMaterial.DOColor(Color.black, 0.5f);
        cameraFilterMaterial.DOFade(1, 0.5f);
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
        //yield return new WaitForSeconds(1.2f);

        //fade lights up
        cameraFilterMaterial.DOColor(cameraFilterColour, 1);
        cameraFilterMaterial.DOFade(cameraFilterAlpha, 1);
        yield return new WaitForSeconds(1.1f);

        //voiceover 42
        audioSource.PlayOneShot(voiceover42);
        yield return new WaitForSeconds(voiceover42Duration);

        //voiceover 43
        audioSource.PlayOneShot(voiceover43);
        yield return new WaitForSeconds(voiceover43Duration);

        //are you ready? panel
        foreach (CanvasGroup panel in areYouReady.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(1, 1);
            panel.blocksRaycasts = true;
            panel.interactable = true;
        }

        fogManager.enabled = true;
    }

}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroLevelController : MonoBehaviour
{
    //controls the zoom animation and voiceover for the intro scene
    public Transform xRRig;
    public GameObject earth;
    public Animator xrRigZoomAnimator;
    public Animator pinAnimator;
    public float cameraZoom01Duration = 17f;
    public float cameraZoom02Duration;
    public float cameraZoom03Duration;
    public AudioSource audioSource;
    public AudioClip sDGIntro;
    public AudioClip introVoiceover01;
    public AudioClip introVoiceover02;
    public AudioClip introVoiceover03;
    public AudioClip introVoiceover04;
    public AudioClip splash;
    public float clipDuration01;
    public float clipDuration02;
    public float clipDuration03;
    public float clipDuration04;
    public GameObject orca;
    public GameObject earthPlain;
    public GameObject earthOrcaDistribution;
    public GameObject locationPin;
    public GameObject underwaterPanorama;
    public CanvasGroup fishingBoat;
    public CanvasGroup containerBoat;
    public CanvasGroup tourists;
    
    public CanvasGroup subtitle;
    public CanvasGroup sDG01;
    public CanvasGroup sDG10;
    public CanvasGroup sDG13;
    public Image fITFLogo;
    public Image sDGTitleNoWheel;
    public Image sDGTitleWheel;
    public Image sDGWheelEmpty;
    public Image sDGWheelIcons;
    public Image sDG14Background;
    public Image spriteAnimation;
    public Image spriteFinal;
    public Image sDGLogoStart;
    public Image sDGLogoFinalFish;
    public Image sDGLogoFinalWaves;
    public Image sDGLogoAnim;
    public Image sDG14Title;
    public Image orcaImage;
    public Image humpback;
    public Image fish1;
    public Image fish2;
    public Image fish3;
    public Image fish4;
    public Image fish5;
    public Image fish6;
    public Image boat;
    public RectTransform maskTransform;
    public Animator sDGanimator;
    public Animator orcaAnimator;
    public Animator humpbackAnimator;
    

    private float sDG14StartScale = 1.053f;
    private float sDG14EndScale = 1.3f;
    private float sDGWheelEndScale = 1.0f;
    private float sDGBackgroundEndScaleX = 1.143f;
    private float sDGBackgroundEndScaleY = 1.571f;
    private float sDGLogoEndScaleX = 0.54f;
    private float sDGLogoEndScaleY = 0.36f;
    private Vector3 sDGlogoEndPosition = new Vector3(819.2f, -368f, 0);
    private Vector3 sDGBackgroudEndRotation = new Vector3(0, 0, 0);
    private Vector3 orcaEndPosition = new Vector3(2200, -604f, 0);
    private Vector3 humpbackEndPosition = new Vector3(2200f, -140, 0);
    private Vector3 fish1EndPosition = new Vector3(2200f, -208f, 0);
    private Vector3 fish2EndPosition = new Vector3(2200f, -404f, 0);
    private Vector3 fish3EndPosition = new Vector3(2200f, -612f, 0);
    private Vector3 fish4EndPosition = new Vector3(2200f, -328f, 0);
    private Vector3 fish5EndPosition = new Vector3(2200f, -588f, 0);
    private Vector3 fish6EndPosition = new Vector3(2200f, -472f, 0);
    private Vector3 boatEndPosition = new Vector3(2200f, 553f, 0);
    private Vector3 titleEndPosition = new Vector3(-192f, 1312f, 0);
    
    //public Material underwaterSkybox;




    void Awake()
    {
        //set initial position of Rig
        xRRig.position = new Vector3(-35, 255, -875.5f);
        xRRig.eulerAngles = new Vector3(15.73f, 2.293f, 0f);
        
        
        //initialize game objects and materials
        orca.SetActive(false);
        earthPlain.SetActive(true);
        earthOrcaDistribution.SetActive(false);
        locationPin.SetActive(false);
        underwaterPanorama.SetActive(false);
        fishingBoat.alpha = 0;
        containerBoat.alpha = 0;
        tourists.alpha = 0;
        

        //fade appropriate images
        sDGWheelIcons.DOFade(0, 0);
        sDG14Background.DOFade(0, 0);
        spriteAnimation.DOFade(0, 0);
        spriteFinal.DOFade(0, 0);
        sDGLogoStart.DOFade(0, 0);
        sDGLogoFinalFish.DOFade(0, 0);
        sDGLogoFinalWaves.DOFade(0, 0);
        sDGLogoAnim.DOFade(0, 0);
        sDG14Title.DOFade(0, 0);
        sDGTitleWheel.DOFade(0, 0);
        sDG01.DOFade(0,0);
        sDG10.DOFade(0,0);
        sDG13.DOFade(0,0);

        
        //Start zoom coroutine
        StartCoroutine(IntroLevel());
    }

    protected IEnumerator IntroLevel()
    {
        //Pause for a few seconds
        yield return new WaitForSeconds(4f);

        //Scale up wheel and fade FitF and Text
        audioSource.PlayOneShot(sDGIntro);
        //yield return new WaitForSeconds(0.25f);
        sDGWheelEmpty.GetComponent<RectTransform>().DOScale(sDGWheelEndScale, 2);
        fITFLogo.DOFade(0, 1);
        sDGTitleNoWheel.DOFade(0, 1);
        subtitle.DOFade(0, 1);

        yield return new WaitForSeconds(2f);

        //Fade in icons and centre title
        sDGWheelIcons.DOFade(1, 2);
        sDG14Background.DOFade(1, 2);
        sDGTitleWheel.DOFade(1, 2);
        sDGLogoStart.DOFade(1, 2f);
        sDG01.DOFade(1, 2f);
        sDG10.DOFade(1, 2f);
        sDG13.DOFade(1, 2f);

        //3 seconds down

        yield return new WaitForSeconds(9.75f);
        sDG01.GetComponent<RectTransform>().DOScale(sDG14EndScale, 0.5f);
        yield return new WaitForSeconds(0.5f);
        sDG01.GetComponent<RectTransform>().DOScale(sDG14StartScale, 0.5f);
        yield return new WaitForSeconds(0.5f);
        sDG10.GetComponent<RectTransform>().DOScale(sDG14EndScale, 0.5f);
        yield return new WaitForSeconds(0.5f);
        sDG10.GetComponent<RectTransform>().DOScale(sDG14StartScale, 0.5f);
        yield return new WaitForSeconds(0.5f);
        sDG13.GetComponent<RectTransform>().DOScale(sDG14EndScale, 0.5f);
        yield return new WaitForSeconds(0.5f);
        sDG13.GetComponent<RectTransform>().DOScale(sDG14StartScale, 0.5f);
        
        yield return new WaitForSeconds(3.25f);

        //17 seconds down

        //Pulse background image a few times
        sDG14Background.GetComponent<RectTransform>().DOScale(sDG14EndScale, 0.5f);
        yield return new WaitForSeconds(0.5f);
        sDG14Background.GetComponent<RectTransform>().DOScale(sDG14StartScale, 0.5f);
        yield return new WaitForSeconds(0.5f);
        sDG14Background.GetComponent<RectTransform>().DOScale(sDG14EndScale, 0.5f);
        yield return new WaitForSeconds(0.5f);
        sDG14Background.GetComponent<RectTransform>().DOScale(sDG14StartScale, 0.5f);
        yield return new WaitForSeconds(0.5f);
        //sDG14Background.GetComponent<RectTransform>().DOScale(sDG14EndScale, 0.5f);
        //yield return new WaitForSeconds(0.5f);
        //sDG14Background.GetComponent<RectTransform>().DOScale(sDG14StartScale, 0.5f);
        //yield return new WaitForSeconds(0.5f);

        //fade in animation image
        sDGWheelEmpty.DOFade(0, 0);
        spriteAnimation.DOFade(1, 0f);
        sDGLogoAnim.DOFade(1, 0f);
        sDGLogoStart.DOFade(0, 0);
        sDG14Background.DOFade(0, 0);


        //Play animation
        sDGanimator.SetTrigger("PlayAnimation");
        yield return new WaitForSeconds(0.225f);


        //Switch final and animation panels
        spriteAnimation.DOFade(0, 0);
        sDGLogoAnim.DOFade(0, 0);
        spriteFinal.DOFade(1, 0f);
        sDGLogoFinalFish.DOFade(1, 0);
        sDGLogoFinalWaves.DOFade(1, 0);

        //Rotate panel
        var transformBG = spriteFinal.GetComponent<RectTransform>();
        transformBG.DOLocalRotate(sDGBackgroudEndRotation, 1);
        
        //Scale background
        transformBG.DOScaleX(sDGBackgroundEndScaleX, 1);
        transformBG.DOScaleY(sDGBackgroundEndScaleY, 1);
        //yield return new WaitForSeconds(2);

        //Scale and move logo
        var transformLogoFish = sDGLogoFinalFish.GetComponent<RectTransform>();
        transformLogoFish.DOScaleX(sDGLogoEndScaleX, 1);
        transformLogoFish.DOScaleY(sDGLogoEndScaleY, 1);
        transformLogoFish.DOLocalMove(sDGlogoEndPosition, 1);

        var transformLogoWaves = sDGLogoFinalWaves.GetComponent<RectTransform>();
        transformLogoWaves.DOScaleX(sDGLogoEndScaleX, 1);
        transformLogoWaves.DOScaleY(sDGLogoEndScaleY, 1);
        transformLogoWaves.DOLocalMove(sDGlogoEndPosition, 1);

        //fade out wheel
        sDGWheelIcons.DOFade(0, 1);
        sDGTitleWheel.DOFade(0, 0);
        sDG01.DOFade(0, 0);
        sDG10.DOFade(0, 0);
        sDG13.DOFade(0, 0);


        yield return new WaitForSeconds(1f);


        //Fade in title
        sDG14Title.DOFade(1, 1);
        yield return new WaitForSeconds(8);

        //Parent title,fish and waves to Mask
        transformLogoFish.SetParent(maskTransform);
        transformLogoWaves.SetParent(maskTransform);
        RectTransform transformTitle = sDG14Title.GetComponent<RectTransform>();
        transformTitle.SetParent(maskTransform);
        

        Vector3 wavesEndPosition = new Vector3(0.014f, 1320, 0);
        Vector3 bigFishEndPosition = new Vector3(2200f, -244.84f, 0);

        //Move title (1s)
        transformTitle.DOLocalMove(titleEndPosition, 1);
        yield return new WaitForSeconds(1);

        //move waves out of the way (1)
        transformLogoWaves.DOLocalMove(wavesEndPosition, 2);
        yield return new WaitForSeconds(3);

        //Wait out rest of voiceover (20s)

        //move fish to final destination
        transformLogoFish.DOLocalMove(bigFishEndPosition, 5f);
        yield return new WaitForSeconds(1);

        //move orca to final destination and start animation
        orcaAnimator.SetTrigger("PlayAnimation");
        orcaImage.GetComponent<RectTransform>().DOLocalMove(orcaEndPosition, 10f);
        yield return new WaitForSeconds(3f);

        //move humback to final destination and start animation
        humpbackAnimator.SetTrigger("PlayAnimation");
        humpback.GetComponent<RectTransform>().DOLocalMove(humpbackEndPosition, 12f);
        yield return new WaitForSeconds(3f);

        // move boat and fish to final destination and start animation
        boat.GetComponent<RectTransform>().DOLocalMove(boatEndPosition, 14f);
        fish1.GetComponent<RectTransform>().DOLocalMove(fish1EndPosition, 14.15f);
        fish2.GetComponent<RectTransform>().DOLocalMove(fish2EndPosition, 13.9f);
        fish3.GetComponent<RectTransform>().DOLocalMove(fish3EndPosition, 14.1f);
        fish4.GetComponent<RectTransform>().DOLocalMove(fish4EndPosition, 13.85f);
        fish5.GetComponent<RectTransform>().DOLocalMove(fish5EndPosition, 13.95f);
        fish6.GetComponent<RectTransform>().DOLocalMove(fish6EndPosition, 14.05f);



        yield return new WaitForSeconds(8f);

        //Start Zoom01 animation
        if (xrRigZoomAnimator != null)
        {
            xrRigZoomAnimator.SetTrigger("CameraZoom01");
        }
        //Wait for seconds (length of zoom 1 animation (15 s)
        yield return new WaitForSeconds(cameraZoom01Duration);

        //play intro voiceover 1
        audioSource.PlayOneShot(introVoiceover01);
        //wait for length of voice over 01
        yield return new WaitForSeconds(clipDuration01);

        //play voiceover 2a
        audioSource.PlayOneShot(introVoiceover02);
        //wait fraction of a second
        yield return new WaitForSeconds(4.0f);
        //activate orca model (with locomotion animation)
        orca.SetActive(true);

        //Wait some seconds
        yield return new WaitForSeconds(clipDuration02-1);
        //play voiceover 2b
        audioSource.PlayOneShot(introVoiceover03);
        StartCoroutine(ShowPictures());
        //Deactivate plain model, activate orca distribution model
        earthPlain.SetActive(false);
        earthOrcaDistribution.SetActive(true);
        //wait until end of voiceover 2 b
        yield return new WaitForSeconds(clipDuration03);

        //Deactivate orca distribution model and reactivate plain model
        earthPlain.SetActive(true);
        earthOrcaDistribution.SetActive(false);
        //Deactivate orca model
        orca.SetActive(false);

        //Wait some seconds
        yield return new WaitForSeconds(2f);
        //play voiceover 3
        audioSource.PlayOneShot(introVoiceover04);
        //Start Zoom02 animation
        if (xrRigZoomAnimator != null)
        {
            xrRigZoomAnimator.SetTrigger("CameraZoom02");
        }
        //wait 0.5 s
        yield return new WaitForSeconds(0.5f);
        
        //yield return new WaitForSeconds(10);
        while(earth.transform.eulerAngles.y > 90)
        {
            yield return null;
        }
        earth.GetComponent<EarthRotation>().isRotating = false;
        //Wait 15s
        yield return new WaitForSeconds(15);
        //Activate pin
        locationPin.SetActive(true);
        //Wait until almost the end of the clip
        yield return new WaitForSeconds(7.5f);

        //Start Zoom03 animation
        if (xrRigZoomAnimator != null)
        {
            xrRigZoomAnimator.SetTrigger("CameraZoom03");
        }
        if (pinAnimator!= null)
        {
            pinAnimator.SetTrigger("Trigger_ScaleDown");
        }
        //wait for end of animation
        yield return new WaitForSeconds(cameraZoom03Duration-0.5f);
        //change skybox
        //RenderSettings.skybox = underwaterSkybox;
        underwaterPanorama.SetActive(true);
        SwitchOnFog();
        earth.SetActive(false);
        //leftHand.SetActive(true);
        //rightHand.SetActive(true);
        //audioSource.PlayOneShot(splash);
        //yield return new WaitForSeconds(splash.length);
        //Load next scene
        ChangeScene.instance.SceneSwitch("MainMenu");

    }

    private IEnumerator ShowPictures()
    {
        yield return new WaitForSeconds(17.5f);
        fishingBoat.DOFade(1,0.5f);
        yield return new WaitForSeconds(0.5f);
        containerBoat.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        tourists.DOFade(1, 0.5f);
        yield return new WaitForSeconds(6.5f);
        fishingBoat.DOFade(0, 0.5f);
        containerBoat.DOFade(0, 0.5f);
        tourists.DOFade(0, 0.5f);

    }

    private void SwitchOnFog()
    {
        RenderSettings.fog = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;

public class IntroAnimation : MonoBehaviour
{
    private RectTransform canvas;
    public Image fITFLogo;
    public Image sDGTitleNoWheel;
    public Image sDGTitleWheel;
    public Image sDGWheelEmpty;
    public Image sDGWheelIcons;
    public Image sDG14Background;
    public Image spriteAnimation;
    public Image spriteFinal;
    public Image sDGLogoStart;
    public Image sDGLogoFinal;
    public Image sDGLogoAnim;
    public Image sDG14Title;
    private Animator animator;

    private float sDG14StartScale = 1.053f;
    private float sDG14EndScale = 1.3f;
    private float sDGWheelEndScale = 1.0f;
    private float sDGBackgroundEndScaleX = 4.55f;
    private float sDGBackgroundEndScaleY = 6.27f;
    private float sDGLogoEndScaleX = 1.35f;
    private float sDGLogoEndScaleY = 0.90f;
    private Vector3 sDGlogoEndPosition = new Vector3(51.2f,-23f,0);
    private Vector3 sDGBackgroudEndRotation = new Vector3(0,0,0);



    void Start()
    {
        //fade appropriate images
        sDGWheelIcons.DOFade(0, 0);
        sDG14Background.DOFade(0, 0);
        spriteAnimation.DOFade(0, 0);
        spriteFinal.DOFade(0, 0);
        sDGLogoStart.DOFade(0, 0);
        sDGLogoFinal.DOFade(0, 0);
        sDGLogoAnim.DOFade(0, 0);
        sDG14Title.DOFade(0, 0);
        sDGTitleWheel.DOFade(0, 0);
        //Set Scale

        //grab Animator Componente
        animator = GetComponentInChildren<Animator>();

        //Start animation coroutine
        StartCoroutine(PlayIntroAnimation());

        canvas = GetComponent<RectTransform>();
    }

    
    IEnumerator PlayIntroAnimation()
    {
        //Pause for a few seconds
        yield return new WaitForSeconds(2f);

        //Scale up wheel and fade FitF and Text
        sDGWheelEmpty.GetComponent<RectTransform>().DOScale(sDGWheelEndScale, 3);
        fITFLogo.DOFade(0, 1);
        sDGTitleNoWheel.DOFade(0, 1);

        yield return new WaitForSeconds(4);

        //Fade in icons and centre title
        sDGWheelIcons.DOFade(1, 2);
        sDG14Background.DOFade(1, 2);
        sDGTitleWheel.DOFade(1, 2);
        sDGLogoStart.DOFade(1, 2f);

        yield return new WaitForSeconds(2);

        //Pulse background image a few times
        sDG14Background.GetComponent<RectTransform>().DOScale(sDG14EndScale, 1);
        yield return new WaitForSeconds(1);
        sDG14Background.GetComponent<RectTransform>().DOScale(sDG14StartScale, 1);
        yield return new WaitForSeconds(1);
        sDG14Background.GetComponent<RectTransform>().DOScale(sDG14EndScale, 1);
        yield return new WaitForSeconds(1);
        sDG14Background.GetComponent<RectTransform>().DOScale(sDG14StartScale, 1);
        yield return new WaitForSeconds(1);
        sDG14Background.GetComponent<RectTransform>().DOScale(sDG14EndScale, 1);
        yield return new WaitForSeconds(1);
        sDG14Background.GetComponent<RectTransform>().DOScale(sDG14StartScale, 1);
        yield return new WaitForSeconds(1);

        //fade in animation image
        sDGWheelEmpty.DOFade(0, 0);
        spriteAnimation.DOFade(1, 0f);
        sDGLogoAnim.DOFade(1, 0f);
        sDGLogoStart.DOFade(0, 0);
        sDG14Background.DOFade(0, 0);


        //Play animation
        animator.SetTrigger("PlayAnimation");
        yield return new WaitForSeconds(0.45f);


        //Switch final and animation panels
        spriteAnimation.DOFade(0, 0);
        sDGLogoAnim.DOFade(0, 0);
        spriteFinal.DOFade(1, 0f);
        sDGLogoFinal.DOFade(1, 0);

        //Rotate panel
        var transformBG = spriteFinal.GetComponent<RectTransform>();
        transformBG.DORotate(sDGBackgroudEndRotation, 2);
        //yield return new WaitForSeconds(2);

        //Deparent logo
        //sDGLogo.GetComponent<RectTransform>().SetParent(canvas);

        //Scale background
        transformBG.DOScaleX(sDGBackgroundEndScaleX, 4);
        transformBG.DOScaleY(sDGBackgroundEndScaleY, 4);
        //yield return new WaitForSeconds(2);

        //Scale and move logo
        var transformLogo = sDGLogoFinal.GetComponent<RectTransform>();
        transformLogo.DOScaleX(sDGLogoEndScaleX, 4);
        transformLogo.DOScaleY(sDGLogoEndScaleY, 4);
        transformLogo.DOLocalMove(sDGlogoEndPosition, 4);

        yield return new WaitForSeconds(7);

        //Fade in title
        sDG14Title.DOFade(1, 2);

        yield return null;
    }
}

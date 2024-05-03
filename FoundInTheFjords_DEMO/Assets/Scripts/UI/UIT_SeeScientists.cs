using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class UIT_SeeScientists : UITransition
{
    public GameObject orcaMom;
    public GameObject xRRig;
    public GameObject momInteractionSignifier;
    public MovementControls moveControls;
    public MoveToObject moveZodiac;
    public MoveToObject moveToClara;
    public Transform claraTargetTransform;
    public AudioSource claraAudioSource;
    public RopeDescent ctd;
    public RopeDescent hydrophone;
    public AudioClip voiceover26;
    public AudioClip voiceover27;
    public AudioClip voiceover28;
    public AudioClip voiceover29;
    public AudioClip voiceover30;
    public AudioSource bgmSource;
    public CanvasGroup panel;
    public List<GameObject> orcas;
    
    private float voiceover26Duration = 12.3f;
    private float voiceover27Duration = 9.5f;
    private float voiceover28Duration = 8.8f;
    private float voiceover29Duration = 12.3f;
    private float voiceover30Duration = 9.1f;
    
    //public CanvasGroup introduceSnorkelersPanel;
    //public CanvasGroup introduceSnorkelersButton;
    public override void UINext()
    {
        StartCoroutine(SeeScientists());

    }

    public IEnumerator SeeScientists()
    {
        //Fade Panel
        panel.DOFade(0, 1);
        panel.blocksRaycasts = false;
        panel.interactable= false;
        //deactivate movement controls
        moveControls.DeactivateMovementControls();
        //disable mom's interactability
        //orcaMom.GetComponentInChildren<XRSimpleInteractable>().enabled = false;
        momInteractionSignifier.SetActive(false);
        //Move Zodiac into place
        StartCoroutine(MoveZodiac());
        //swim to Clara (by the Zodiac)
        //make Nora child
        xRRig.transform.SetParent(orcaMom.transform);
        //update target parameters
        moveToClara.targetTransform = claraTargetTransform;
        moveToClara.minDistance = 5f;
        //move
        orcaMom.GetComponent<Animator>().SetTrigger("Trigger_Swim");
        moveToClara.distance = Vector3.Distance(moveToClara.targetTransform.position, moveToClara.transform.position);
        while (moveToClara.distance > moveToClara.minDistance)
        {
            moveToClara.MoveToMinimumDistance();
            yield return null;
        }

        //Add rotate to align
        while (orcaMom.transform.eulerAngles.y < moveToClara.targetTransform.eulerAngles.y - 2 || orcaMom.transform.eulerAngles.y > moveToClara.targetTransform.eulerAngles.y + 2)
        {
            moveToClara.RotateToAlign();
            yield return null;
            //xRRig.transform.rotation != moveToMom.targetTransform.rotation
        }
        orcaMom.GetComponent<Animator>().SetTrigger("Trigger_StopSwim");

        //deparent Nora from Mom
        xRRig.transform.SetParent(null);

        //Deactivate other orca
        for(int i = 0; i < orcas.Count; i++)
        {
            orcas[i].SetActive(false);
        }

        //activate movement controls
        moveControls.ActivateMovementControls();

        //Play voiceover 26
        claraAudioSource.PlayOneShot(voiceover26);
        yield return new WaitForSeconds(voiceover26Duration);
        //Play voiceover 27
        claraAudioSource.PlayOneShot(voiceover27);
        yield return new WaitForSeconds(voiceover27Duration);
        //play hydrophone animation
        StartCoroutine(LowerHydrophone());

        yield return new WaitForSeconds(10f);
        //play voiceover 28
        claraAudioSource.PlayOneShot(voiceover28);
        yield return new WaitForSeconds(voiceover28Duration);
        //play CTD animation
        StartCoroutine(LowerCTD());
        yield return new WaitForSeconds(10f);
        //play voiceover 29
        claraAudioSource.PlayOneShot(voiceover29);
        yield return new WaitForSeconds(voiceover29Duration);
        // Play voiceover 30
        claraAudioSource.PlayOneShot(voiceover30);
        yield return new WaitForSeconds(voiceover30Duration);
        // Start explore music
        StartCoroutine(FadeAudioSource.StartFade(bgmSource, 5f, 1f));
        //reset and enable mom's interactability
        orcaMom.GetComponentInChildren<JellyInteractions>().coroutineName = "IntroduceSnorkelers";
        //orcaMom.GetComponentInChildren<XRSimpleInteractable>().enabled = true;
        momInteractionSignifier.SetActive(true);
        //activate movement controls
        moveControls.ActivateMovementControls();

        StartCoroutine(RaiseHydrophone());
        StartCoroutine(RaiseCTD());

        yield return null;
    }

    public IEnumerator MoveZodiac()
    {
        moveZodiac.distance = Vector3.Distance(moveZodiac.targetTransform.position, moveZodiac.transform.position);
        while (moveZodiac.distance > moveZodiac.minDistance)
        {
            moveZodiac.MoveToMinimumDistance();
            yield return null;
        }
    }

    public IEnumerator LowerCTD()
    {
        ctd.speed = 0.5f;
        while (ctd.equipment.transform.position.y > -9)
        {
            ctd.LowerItem();
            yield return null;
        }
    }

    public IEnumerator LowerHydrophone()
    {
        hydrophone.speed = 0.5f;
        while (hydrophone.equipment.transform.position.y > -6)
        {
            hydrophone.LowerItem();
            yield return null;
        }
    }

    public IEnumerator RaiseCTD()
    {
        ctd.speed = -0.5f;
        while (ctd.equipment.transform.position.y < -1)
        {
            ctd.LowerItem();
            yield return null;
        }
    }

    public IEnumerator RaiseHydrophone()
    {
        hydrophone.speed = -0.5f;
        while (hydrophone.equipment.transform.position.y < -1)
        {
            hydrophone.LowerItem();
            yield return null;
        }
    }
}

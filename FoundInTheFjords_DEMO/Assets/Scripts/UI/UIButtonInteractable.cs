using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;


public class UIButtonInteractable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private float hoverStartAnimationDuration = 0.2f;
    private float hoverEndAnimationDuration = 0.1f;
    private float scaleIconSize = 1.02f;
    private Vector3 startScale;

    //public Canvas infoUI;
    public Image icon;

    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip selectSound;

    public UnityEvent onClick;

    private void OnEnable()
    {
        startScale = transform.localScale;
        icon.enabled = true;
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        // kill all animations first
        transform.DOKill();

        // hover animations
        transform.DOScale(scaleIconSize, hoverStartAnimationDuration);


        // hover sounds
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(hoverSound, 0.25f);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UISystemProfilerApi.AddMarker("Button.onClick", this);

        //play select sound
        audioSource.PlayOneShot(selectSound, 0.25f);

        onClick?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // kill all animations first
        transform.DOKill();

        // exit hover animations
        transform.DOScale(startScale, hoverEndAnimationDuration);
    }


}

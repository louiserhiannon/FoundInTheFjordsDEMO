using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using static UnityEngine.Rendering.DebugUI;


public class UICTAControl : MonoBehaviour
{
    public Canvas cTACanvas;
    public Canvas learnMoreCanvas;
    public CanvasGroup introPanel;
    public CanvasGroup keyboard;
    public List<CanvasGroup> actionPanels;
    public AudioClip intro;
    private AudioSource uISource;
    public float fadeAlpha;
    

    void Start()
    {
        foreach (CanvasGroup panel in cTACanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.alpha = 0f;
            panel.interactable = false;
            panel.blocksRaycasts = false;
        }
        foreach (CanvasGroup panel in learnMoreCanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.alpha = 0f;
            panel.interactable = false;
            panel.blocksRaycasts = false;
        }

        keyboard.alpha = 0f;
        keyboard.interactable = false;
        keyboard.blocksRaycasts = false;
        
        uISource = GetComponent<AudioSource>();

        StartCoroutine(UIAppear());
    }

    public void FadeAllPanels()
    {
        foreach (CanvasGroup panel in cTACanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(0f, 1f);
            panel.interactable = false;
            panel.blocksRaycasts = false;
        }

        foreach (CanvasGroup panel in learnMoreCanvas.GetComponentsInChildren<CanvasGroup>())
        {
            panel.DOFade(0f, 1f);
            panel.interactable = false;
            panel.blocksRaycasts = false;
        }
    }

    public void PartialFadeOut()
    {
        for (int i = 0; i < actionPanels.Count; i++)
        {
            actionPanels[i].DOFade(fadeAlpha, 1f);
            actionPanels[i].interactable = false;
            actionPanels[i].blocksRaycasts = false;
        }

    }

    public void PanelFadeIn()
    {
        for (int i = 0; i < actionPanels.Count; i++)
        {
            actionPanels[i].DOFade(1f, 1.5f);
            actionPanels[i].interactable = true;
            actionPanels[i].blocksRaycasts = true;
        }
    }

    public IEnumerator UIAppear()
    {
        yield return new WaitForSeconds(3f);
        
        uISource.PlayOneShot(intro);
        
        yield return new WaitForSeconds(intro.length - 4.5f);

        //introPanel.DOFade(1, 2);

        //yield return new WaitForSeconds(5f);

        //introPanel.DOFade(0, 2);

        PanelFadeIn();
    }
}

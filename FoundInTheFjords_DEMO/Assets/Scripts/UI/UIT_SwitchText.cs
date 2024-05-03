using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIT_SwitchText : MonoBehaviour
{
    public List<CanvasGroup> textBlocks;
    public CanvasGroup thisText;
    public void SwitchText()
    {
        for(int i = 0; i < textBlocks.Count; i++)
        {
            textBlocks[i].alpha = 0;
            textBlocks[i].interactable = false;
            textBlocks[i].blocksRaycasts = false;
        }

        thisText.DOFade(1, 1);
    }
}

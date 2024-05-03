using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShowArrows : MonoBehaviour
{
    public CanvasGroup arrows;
    public void ShowArrows()
    {
        StartCoroutine(MakeArrowsVisible());
    }

    private IEnumerator MakeArrowsVisible()
    {
        yield return new WaitForSeconds(1.1f);
        arrows.DOFade(1, 1.5f);
    }
}

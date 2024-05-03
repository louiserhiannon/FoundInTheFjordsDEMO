using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasDepthController : MonoBehaviour
{
    private CanvasGroup panel;
    public DepthCalculator depthCalculator;
    private bool panelShowing = false;

    // Start is called before the first frame update
    void Start()
    {
        panel = GetComponent<CanvasGroup>();
        panel.alpha = 0;
        panel.interactable = false;
        panel.blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(depthCalculator.headsetDepthCorrected >= -15)
        {
            if (!panelShowing)
            {
                panel.DOFade(1, 1);
                panelShowing = true;
            }
        }
        else
        {
            if (panelShowing)
            {
                panel.DOFade(0, 1);
                panelShowing = false;
            }
        }
        
    }
}

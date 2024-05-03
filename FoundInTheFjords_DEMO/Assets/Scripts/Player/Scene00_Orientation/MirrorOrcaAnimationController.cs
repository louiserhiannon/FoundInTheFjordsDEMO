using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UIElements;
using System.Xml.Schema;
using DG.Tweening;

public class MirrorOrcaAnimationController : PlayerInputController
{
    //Script to convert controller inputs into mirror animations on the orca reflection during orientation   
    private Animator mirrorOrcaAnimator;
    private bool firstBite = true;
    public CanvasGroup biteInstructionsPanel;
    
    
    

    protected override void Awake()
    {
        base.Awake();
        mirrorOrcaAnimator = GetComponent<Animator>();
    }

    

      protected override void Update()
    {
        base.Update();
        Swim(yvalue, xvalue);
        TailSlap(gripValue);  
    }

    public override void Bite(InputAction.CallbackContext context)
    {
        if(mirrorOrcaAnimator!= null)
        {
            mirrorOrcaAnimator.SetTrigger("Trigger_Bite");
            if (firstBite)
            {
                biteInstructionsPanel.DOFade(0, 1);
                firstBite = false;
            }
        }
    }

    public override void TailSlap(float value)
    {
        base.TailSlap(value);
        

        if(value < 0.9f)
        {
            for(int i = 0; i < chargedSliders.Count; i++)
            {
                chargedSliders[i].enabled = false;
            }
            
            
        }

        if (value < 0.05f)
        {
            if (tailCharged == true)
            {
                if (mirrorOrcaAnimator != null)
                {
                    mirrorOrcaAnimator.SetTrigger("Trigger_TailSlap");
                    chargedText.text = string.Empty;
                    StartCoroutine(ShowReadyText());
                    
                }
            }
            slapCharge = 0f;
            tailCharged = false;
            
            //tailChargeText.text = string.Empty;
            


        }
        

        
    }

    public override void Swim(float xValue, float yValue)
    {
              
        if (mirrorOrcaAnimator != null)
        {
            if (xValue > 0.5f || xValue < -0.5f || yValue > 0.5f || yValue < -0.5f)
            {
                mirrorOrcaAnimator.SetTrigger("Trigger_Swim");
                
                
            }
            else
            {
                mirrorOrcaAnimator.SetTrigger("Trigger_StopSwim");
            }
                
        }
    }

    private IEnumerator ShowReadyText()
    {
        yield return new WaitForSeconds(4f);
        if(chargedText.text == string.Empty)
        {
            chargedText.text = "Ready";
        }
        
    }

    

}

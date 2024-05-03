using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BoundingNetController : MonoBehaviour
{
    public CanvasGroup goBackPanel;
    //public bool canMove = true;
    //public ActionBasedSnapTurnProvider snapTurnProvider;
    private void Awake()
    {
        //snapTurnProvider.enableTurnLeftRight = true;
        this.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(true);
            goBackPanel.DOFade(1, 2);
            //canMove = false;
            //Debug.Log("canMove = false");
            //if (snapTurnProvider != null)
            //{
            //    snapTurnProvider.enableTurnLeftRight = false;
            //}

        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            goBackPanel.DOFade(0, 2);
            //canMove = true;
            //Debug.Log("canMove = true");
            //if (snapTurnProvider != null)
            //{
            //    snapTurnProvider.enableTurnLeftRight = true;
            //}
        }
        
    }
}

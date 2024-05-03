using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace FoundInTheFjordsSceneManager
{
    public class SceneManager : MonoBehaviour
    {
        public List<Canvas> canvasList;
        public GameObject orcaMom;
        protected Animator orcaMomAnimator;
        public GameObject xRRig;
        public MovementControls moveControls;
        
        


        protected virtual void Awake()
        {
            //Disable Panels
            for (int i = 0; i < canvasList.Count; i++)
            {
                foreach (CanvasGroup panel in canvasList[i].GetComponentsInChildren<CanvasGroup>())
                {
                    panel.alpha = 0;
                    panel.interactable = false;
                    panel.blocksRaycasts = false;
                }
            }


            //Disable Move Controls
            moveControls.DeactivateMovementControls();

            //Disable Eat Controller
            moveControls.DeActivateEatControls();

            //Get orca mom animator component
            if(orcaMom != null)
            {
                orcaMomAnimator = orcaMom.GetComponent<Animator>();
            }



        }


    }
}


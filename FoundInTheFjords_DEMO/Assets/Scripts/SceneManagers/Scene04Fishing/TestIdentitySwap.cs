using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIdentitySwap : MonoBehaviour
{
    public List<Canvas> canvases;
    public IdentitySwap identitySwap;
    //public MovementControls moveControl;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < canvases.Count; i++)
        {
            foreach (CanvasGroup panel in canvases[i].GetComponentsInChildren<CanvasGroup>())
            {
                panel.alpha = 0;
                panel.interactable = false;
                panel.blocksRaycasts= false;
            }
        }

        StartCoroutine(identitySwap.SwitchBodies());
        //moveControl.ActivateMovementControls();
    }

    
}

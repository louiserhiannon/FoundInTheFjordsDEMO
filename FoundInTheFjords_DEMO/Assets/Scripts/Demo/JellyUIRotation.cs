using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyUIRotation : MonoBehaviour
{
    public Camera player;
    protected List<CanvasGroup> canvasGroups;

    void Start()
    {
        canvasGroups = new List<CanvasGroup>(GetComponentsInChildren<CanvasGroup>());   //makes a list of all the panels in the canvas
    }

    void Update()
    {
        //Sets Canvas to move to look at player but only if there is a visible panel
        for (int i = 0; i < canvasGroups.Count;i++)
        {
            if (canvasGroups[i].alpha == 1)
            {
                transform.LookAt(player.transform.position);
                transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
                return;//Finishes function as soon as a visible panel is found

            }
        }
        
        
    }
}

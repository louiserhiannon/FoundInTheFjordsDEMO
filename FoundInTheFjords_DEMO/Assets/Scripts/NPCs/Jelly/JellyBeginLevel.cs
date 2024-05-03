using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyBeginLevel : UITransition
{
    //Activate first panel and start first audio dialogue.

    void Awake()
    {
        StartCoroutine(SwitchPanel());
    }

    
}

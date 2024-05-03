using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnorkelerAnimationController : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Animator>().SetTrigger("Trigger_Slide");
    }

    
}

using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class HumpbackSwimAnimation : MonoBehaviour
{
    [SerializeField] private Animator humpbackAnimator;
    
    void Awake()
    {
        humpbackAnimator = GetComponent<Animator>();
        
    }

    private void OnDisable()
    {
        humpbackAnimator.SetTrigger("Trigger_StopSwim");
    }

    public void StartSwim()
    {

        humpbackAnimator.SetTrigger("Trigger_Swim");
    }

    public void StopSwim()
    {

        humpbackAnimator.SetTrigger("Trigger_StopSwim");
    }
    public void StartGulp()
    {

        humpbackAnimator.SetTrigger("Trigger_Eat");
    }


}

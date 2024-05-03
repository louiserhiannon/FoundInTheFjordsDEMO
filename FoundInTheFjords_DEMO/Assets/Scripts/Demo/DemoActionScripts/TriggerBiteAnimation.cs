using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBiteAnimation : MonoBehaviour
{
    private Animator orcaAnimator;
    private bool isFeeding;
    private ParticleSystem bubbles;

    void Start()
    {
        orcaAnimator= GetComponent<Animator>();
        bubbles = GetComponentInChildren<ParticleSystem>();
        isFeeding = true;
        StartCoroutine(NomNomNom());
    }

    private IEnumerator NomNomNom()
    {
        if (orcaAnimator != null)
        {
            while (isFeeding)
            {
                
                yield return new WaitForSeconds(Random.Range(5f, 15f));
                orcaAnimator.SetTrigger("Trigger_Bite");
                yield return new WaitForSeconds(Random.Range(3f, 6f));
                bubbles.Play();
                
                yield return new WaitForSeconds(Random.Range(1f, 2f));
                bubbles.Stop();
                yield return null;
            }
            
        }
    }

    //void Update()
    //{
    //    if(orcaAnimator != null)
    //    {
    //        while(isFeeding)
    //        {

    //        }
    //        if(Random.Range(1,1000) < 5)
    //        {
    //            orcaAnimator.SetTrigger("Trigger_Bite");
    //        }
    //    }
    //}
}

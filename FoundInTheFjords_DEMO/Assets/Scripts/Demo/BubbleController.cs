using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField] private bool makeBubbles = true;
    private ParticleSystem bubbles;
    void Start()
    {
        bubbles = GetComponentInChildren<ParticleSystem>();
        StartCoroutine(PlayBubbles());
    }

    //private void Update()
    //{
    //    if(!isPlaying)
    //    {
    //        StartCoroutine(PlayBubbles());
    //    }
    //}

    private IEnumerator PlayBubbles()
    {
        while (makeBubbles)
        {
            yield return new WaitForSeconds(Random.Range(7f, 15f));
            bubbles.Play();
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            bubbles.Stop();
            yield return null;
        }
        //isPlaying = true;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooClose : MonoBehaviour
{
    private Material netMaterial;
    private Color hoverColor;
    private Color nonHoverColor;
    public List<AudioClip> tooCloseClips;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        netMaterial = GetComponent<MeshRenderer>().materials[1];
        hoverColor = Color.red;
        nonHoverColor = Color.white;
    }

    public void OnHoverStart()
    {
        netMaterial.color = hoverColor;
        //play too close audio
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(tooCloseClips[Random.Range(0, tooCloseClips.Count - 1)]);
        }
        
    }

    public void OnHoverEnd()
    {
        netMaterial.color = nonHoverColor;
    }
}

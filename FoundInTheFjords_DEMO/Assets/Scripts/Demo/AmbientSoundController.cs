using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundController : MonoBehaviour
{
    public List<AudioSource> sources;
    public List<AudioClip> clips;
    // Start is called before the first frame update
    void Awake()
    {
        if(sources.Count == clips.Count)
        {
            for (int i = 0; i < clips.Count; i++)
            {
                sources[i].PlayOneShot(clips[i]);
                //sources[i].clip = clips[i];
                //sources[i].Play();
            }
        }
        
    }

    
}

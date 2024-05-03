using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour
{
    public static SplashController instance;
    private bool playSplash = true;
    private AudioSource splashAudioSource;
    public AudioClip splash;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        splashAudioSource = GetComponent<AudioSource>();
        if (playSplash)
        {
            splashAudioSource.PlayOneShot(splash);
            playSplash = false;

        }
    }

    
}

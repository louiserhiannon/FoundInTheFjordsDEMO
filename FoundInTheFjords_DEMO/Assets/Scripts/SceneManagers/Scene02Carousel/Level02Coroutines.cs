using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Level02Coroutines : MonoBehaviour
{
    public AudioSource momAudioSource;
    public AudioClip voiceover18;
    private float voiceover18Duration = 6.5f;
    public AudioSource bgmSource;
    public CanvasGroup mainMenu;
    public void StartCustomCoroutine(string name)
    {
        StartCoroutine(name);
    }

    public IEnumerator ReadyToTravel()
    {
        // Fade music out
        StartCoroutine(FadeAudioSource.StartFade(bgmSource, 5f, 0f));
        //play voiceover 17
        momAudioSource.PlayOneShot(voiceover18);
        //wait a bit
        yield return new WaitForSeconds(voiceover18Duration);
        //ChangeScene
        if (EatingController.EC != null)
        {
            Destroy(EatingController.EC);
        }
        if (MovementControls.MC != null)
        {
            Destroy(MovementControls.MC);
        }
        if (HerringSpawner.HS != null)
        {
            Destroy(HerringSpawner.HS);
        }
        //bgmSource.Stop();

        mainMenu.DOFade(1, 1);
        mainMenu.interactable = true;
        mainMenu.blocksRaycasts = true; 
        //ChangeScene.instance.SceneSwitch("MainMenu");
    }
}

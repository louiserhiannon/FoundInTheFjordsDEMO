using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager_Locomotion : MonoBehaviour
{
    public AudioSource mom;
    public AudioSource nora;
    public ParticleSystem momBubbles;
    public List<AudioClip> voiceovers;
    public List<float> voiceoverDurations;
    public CanvasGroup mainMenu;
    

    private void Start()
    {
        
        StartCoroutine(GoTravelling());
    }

    private IEnumerator GoTravelling()
    {
        yield return new WaitForSeconds(5f);
        mom.PlayOneShot(voiceovers[0]);
        momBubbles.Play();
        yield return new WaitForSeconds(voiceoverDurations[0]);
        momBubbles.Stop();
        
        nora.PlayOneShot(voiceovers[1]);
        yield return new WaitForSeconds(voiceoverDurations[1]);
        
        
        mom.PlayOneShot(voiceovers[2]);
        momBubbles.Play();
        yield return new WaitForSeconds(voiceoverDurations[2]);
        momBubbles.Stop();

        mom.PlayOneShot(voiceovers[3]);
        momBubbles.Play();
        yield return new WaitForSeconds(voiceoverDurations[3]);
        momBubbles.Stop();

        mom.PlayOneShot(voiceovers[4]);
        momBubbles.Play();
        yield return new WaitForSeconds(voiceoverDurations[4]);
        momBubbles.Stop();

        nora.PlayOneShot(voiceovers[5]);
        yield return new WaitForSeconds(voiceoverDurations[5]);

        mom.PlayOneShot(voiceovers[6]);
        momBubbles.Play();
        yield return new WaitForSeconds(voiceoverDurations[6]);
        momBubbles.Stop();

        mom.PlayOneShot(voiceovers[7]);
        momBubbles.Play();
        yield return new WaitForSeconds(voiceoverDurations[7]);
        momBubbles.Stop();

        nora.PlayOneShot(voiceovers[8]);
        yield return new WaitForSeconds(voiceoverDurations[8]);

        yield return new WaitForSeconds(15f);

        nora.PlayOneShot(voiceovers[9]);
        yield return new WaitForSeconds(voiceoverDurations[9]);

        mom.PlayOneShot(voiceovers[10]);
        momBubbles.Play();
        yield return new WaitForSeconds(voiceoverDurations[10]);
        momBubbles.Stop();

        mom.PlayOneShot(voiceovers[11]);
        momBubbles.Play();
        yield return new WaitForSeconds(voiceoverDurations[11]);
        momBubbles.Stop();

        mainMenu.DOFade(1, 1);
        mainMenu.blocksRaycasts = true;
        mainMenu.interactable = true;
        //ChangeScene.instance.SceneSwitch("Scene04-Fishing");

        yield return null;
    }
}

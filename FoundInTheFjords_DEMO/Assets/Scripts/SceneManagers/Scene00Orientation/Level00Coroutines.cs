using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level00Coroutines : MonoBehaviour
{
    public AudioSource claraAudioSource;
    public List<AudioClip> voiceoverClips; //voiceovers 7-11
    private float voiceover07Duration = 13.1f;
    private float voiceover08Duration = 11.3f;
    private float voiceover09Duration = 13.8f;
    private float voiceover10Duration = 21.2f;
    private float voiceover11Duration = 15.3f;
    public CanvasGroup subtitlePanel;
    public List<CanvasGroup> subtitleSnippets;
    public CanvasGroup ready;
    public GameObject interactionSignifier;

    private void Awake()
    {
        interactionSignifier.SetActive(false);
    }


    public void StartCustomCoroutine(string name)
    {
        StartCoroutine(name);
    }
    public IEnumerator FinishOrientation()
    {
        yield return new WaitForSeconds(1f);
        //start voiceover 07 and subtitles
        claraAudioSource.PlayOneShot(voiceoverClips[0]);
        subtitlePanel.DOFade(1, 1);
        subtitleSnippets[0].DOFade(1, 1);
        //wait for duration of clip
        yield return new WaitForSeconds(voiceover07Duration);
        subtitleSnippets[0].DOFade(0, 1);
        yield return new WaitForSeconds(1f);
        
        //start voiceover 08 and subtitles
        claraAudioSource.PlayOneShot(voiceoverClips[1]);
        subtitleSnippets[1].DOFade(1, 1);
        //wait for duration of clip
        yield return new WaitForSeconds(voiceover08Duration);
        subtitleSnippets[1].DOFade(0, 1);
        yield return new WaitForSeconds(1f);

        //start voiceover 09 and subtitles
        claraAudioSource.PlayOneShot(voiceoverClips[2]);
        subtitleSnippets[2].DOFade(1, 1);
        //wait for duration of clip
        yield return new WaitForSeconds(voiceover09Duration);
        subtitleSnippets[2].DOFade(0, 1);
        yield return new WaitForSeconds(1f);

        //start voiceover 10 and subtitles
        claraAudioSource.PlayOneShot(voiceoverClips[3]);
        subtitleSnippets[3].DOFade(1, 1);
        //wait for duration of clip
        yield return new WaitForSeconds(voiceover10Duration);
        subtitleSnippets[3].DOFade(0, 1);
        yield return new WaitForSeconds(1f);

        //start voiceover 11 and subtitles
        claraAudioSource.PlayOneShot(voiceoverClips[4]);
        StartCoroutine(ShowInteractionSignifier());
        subtitleSnippets[4].DOFade(1, 1);
        //wait for duration of clip
        yield return new WaitForSeconds(voiceover11Duration);
        subtitleSnippets[4].DOFade(0, 1);
        yield return new WaitForSeconds(1f);

        subtitlePanel.DOFade(0, 1);

        yield return new WaitForSeconds(1f);
        ready.DOFade(1, 1);
        ready.interactable = true;
        ready.blocksRaycasts = true;
    }

    private IEnumerator ShowInteractionSignifier()
    {
        yield return new WaitForSeconds(6);
        interactionSignifier.SetActive(true);
        yield return new WaitForSeconds(8);
        interactionSignifier.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;

public class Level01Coroutines : MonoBehaviour
{
    public AudioSource claraAudioSource;
    public AudioSource headsetAudioSource;
    public List<AudioClip> voiceoverClips; //voiceovers 5-9
    public List<float> voiceoverDurations;
    public CanvasGroup subtitlePanel;
    public List<CanvasGroup> subtitleSnippets;
    public CanvasGroup sDGLogo;
    public GameObject momInteractionSignifier;
    public AudioSource bgmSource;
    public AudioClip exploreMusic;
    public void StartCustomCoroutine(string name)
    {
        StartCoroutine(name);
    }
    public IEnumerator OrcasAreGreat()
    {
        yield return new WaitForSeconds(1f);
        //start voiceover 05 and subtitles
        claraAudioSource.PlayOneShot(voiceoverClips[0]);
        subtitlePanel.DOFade(1, 1);
        subtitleSnippets[0].DOFade(1, 1);
        //wait for duration of subtitle snippet
        yield return new WaitForSeconds(voiceoverDurations[0]);
        subtitleSnippets[0].DOFade(0, 1);
        subtitleSnippets[1].DOFade(1, 1);
        sDGLogo.DOFade(1, 1);
        yield return new WaitForSeconds(voiceoverDurations[1]);
        subtitleSnippets[1].DOFade(0, 1);
        yield return new WaitForSeconds(1f);

        //start voiceover 06

        headsetAudioSource.PlayOneShot(voiceoverClips[1]);
        subtitleSnippets[10].DOFade(1, 1);
        yield return new WaitForSeconds(voiceoverDurations[2]);
        subtitleSnippets[10].DOFade(0, 1);

        //start voiceover 07 and subtitles
        claraAudioSource.PlayOneShot(voiceoverClips[2]);
        subtitlePanel.DOFade(1, 1);
        subtitleSnippets[2].DOFade(1, 1);
        //wait for duration of subtitle snippet
        yield return new WaitForSeconds(voiceoverDurations[3]);
        subtitleSnippets[2].DOFade(0, 1);
        subtitleSnippets[3].DOFade(1, 1);
        yield return new WaitForSeconds(voiceoverDurations[4]);
        subtitleSnippets[3].DOFade(0, 1);
        subtitleSnippets[4].DOFade(1, 1);
        yield return new WaitForSeconds(voiceoverDurations[5]);
        subtitleSnippets[4].DOFade(0, 1);
        subtitleSnippets[5].DOFade(1, 1);
        yield return new WaitForSeconds(voiceoverDurations[6]);
        subtitleSnippets[5].DOFade(0, 1);
        subtitleSnippets[6].DOFade(1, 1);
        yield return new WaitForSeconds(voiceoverDurations[7]);
        subtitleSnippets[6].DOFade(0, 1);
        subtitleSnippets[7].DOFade(1, 1);
        yield return new WaitForSeconds(voiceoverDurations[8]);
        subtitleSnippets[7].DOFade(0, 1);
        subtitleSnippets[8].DOFade(1, 1);
        yield return new WaitForSeconds(voiceoverDurations[9]);
        subtitleSnippets[8].DOFade(0, 1);
        sDGLogo.DOFade(0, 1);
        yield return new WaitForSeconds(1f);

        //start voiceover 08 and subtitles
        headsetAudioSource.PlayOneShot(voiceoverClips[3]);
        subtitleSnippets[11].DOFade(1, 1);
        yield return new WaitForSeconds(voiceoverDurations[10]);
        subtitleSnippets[11].DOFade(0, 1);

        //start voiceover 09 and subtitles
        claraAudioSource.PlayOneShot(voiceoverClips[4]);
        subtitleSnippets[9].DOFade(1, 1);
        //wait for duration of clip
        yield return new WaitForSeconds(voiceoverDurations[11]);
        subtitleSnippets[9].DOFade(0, 1);
        yield return new WaitForSeconds(1f);
        bgmSource.clip = exploreMusic;
        bgmSource.loop = true;
        bgmSource.Play();
        StartCoroutine(FadeAudioSource.StartFade(bgmSource, 5f, 1f));

        subtitlePanel.DOFade(0, 1);
        momInteractionSignifier.SetActive(true);
    }

}

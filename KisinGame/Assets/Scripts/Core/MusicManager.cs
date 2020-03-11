using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> musicList = new List<AudioClip>();
    [SerializeField] float fadeInTime = 3;
    [SerializeField] float fadeOutTime = 3;

    AudioSource audioSource;
    int currentAudioClipIndexPlaying = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void WantsToPlayThisMusicNext(int newAudioClipIndex, float volume, bool loop)
    {
        if (currentAudioClipIndexPlaying == newAudioClipIndex) return;
        if (audioSource.isPlaying)
        {
            StartCoroutine(MusicChange(newAudioClipIndex, volume, loop));
        }
        else
        {
            StartCoroutine(FadeIn(newAudioClipIndex, volume, loop));
        }
        currentAudioClipIndexPlaying = newAudioClipIndex;
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutC());
        currentAudioClipIndexPlaying = 0;
    }

    IEnumerator MusicChange(int newAudioClipIndex, float volume, bool loop)
    {

        yield return FadeOutC();
        yield return FadeIn(newAudioClipIndex, volume, loop);
    }

    IEnumerator FadeIn(int newAudioClipIndex, float maxVolume, bool loop)
    {
        ChangeTrack(newAudioClipIndex,loop);
        float volume = 0;
        while (volume < maxVolume)
        {
            volume += ((Time.deltaTime/fadeInTime)*maxVolume);
            if (volume > maxVolume) volume = maxVolume;
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeOutC()
    {
        float volume = audioSource.volume;
        float maxVolume = volume;
        while (volume > 0)
        {
            volume -= ((Time.deltaTime / fadeOutTime) * maxVolume);
            if (volume < 0) volume = 0;
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }
        audioSource.Stop();
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayMakerFSM>().SendEvent("MusicFadedOut");
    }

    void ChangeTrack(int newAudioClipIndex, bool loop)
    {
        audioSource.volume = 0;
        audioSource.clip = musicList[newAudioClipIndex];
        audioSource.loop = loop;
        audioSource.Play();
    }
}

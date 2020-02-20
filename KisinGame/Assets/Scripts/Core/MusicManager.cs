using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class MusicManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> musicList = new List<AudioClip>();
    [SerializeField] float fadeInTime = 3;
    [SerializeField] float fadeOutTime = 3;

    public static MusicManager musicManagerInstance;

    AudioSource audioSource;

    private void Awake()
    {
        if (musicManagerInstance != null && musicManagerInstance != this) Destroy(this.gameObject);
        musicManagerInstance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void WantsToPlayThisMusicNext(int index, float volume, bool loop)
    {
        print(audioSource.isPlaying);
        if (audioSource.isPlaying)
        {
            StartCoroutine(MusicChange(index, volume, loop));
        }
        else
        {
            StartCoroutine(FadeIn(index, volume, loop));
        }
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutC());
    }

    IEnumerator MusicChange(int index, float volume, bool loop)
    {

        yield return FadeOutC();
        yield return FadeIn(index, volume, loop);
    }

    IEnumerator FadeIn(int index, float maxVolume, bool loop)
    {
        ChangeTrack(index,loop);
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

    void ChangeTrack(int index, bool loop)
    {
        audioSource.volume = 0;
        audioSource.clip = musicList[index];
        audioSource.loop = loop;
        audioSource.Play();
    }
}

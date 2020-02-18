using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [Tooltip("Index of the track to play in the Music List in MusicManager")]
    [SerializeField] int index = 0;
    [Tooltip("Volume of the track to play in the Music List")]
    [SerializeField] float volume = 1;
    [Tooltip("If the track will be looped or not")]
    [SerializeField] bool loop = false;
    [Tooltip("If the track will be played on Awake or not")]
    [SerializeField] bool playOnAwake = true;


    static int currentIndexPlaying=1000;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        if (playOnAwake)
        {
            PlayThisMusic();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void PlayThisMusic()
    {
        if (currentIndexPlaying == index) return;
        GameObject.FindObjectOfType<MusicManager>().WantsToPlayThisMusicNext(index, volume, loop);
        currentIndexPlaying = index;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerIdentifer>())
        {
            PlayThisMusic();
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    enum TriggerType
    {
        PlayOnAwake, OnTriggerEnter, OnUse
    }
    [SerializeField] TriggerType triggerType;
    [Tooltip("Index of the track to play in the Music List in MusicManager")]
    [SerializeField] int index = 0;
    [Tooltip("Volume of the track to play in the Music List")]
    [SerializeField] float volume = 1;
    [Tooltip("If the track will be looped or not")]
    [SerializeField] bool loop = false;

    IEnumerator Start()
    {
        int triggerID = (int)triggerType;
        yield return new WaitForSeconds(1f);
        
        if (triggerID == 0)
        {
            PlayThisTrack();
            DisableCollider();
        }
        if (triggerID == 1)
        {

            yield break;
        }
        if(triggerID == 2)
        {
            DisableCollider();
            yield break;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerIdentifer>())
        {
            PlayThisTrack();
        }
    }

    public void PlayThisTrack()
    {
        GameObject.FindObjectOfType<MusicManager>().WantsToPlayThisMusicNext(index, volume, loop);
    }

    void DisableCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    //Called from the BossRoomTransitiion script
    public void PlayerLevelTrack()
    {
        GameObject.FindObjectOfType<MusicManager>().WantsToPlayThisMusicNext(index-1, volume, loop);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossRoomTransition : MonoBehaviour
{
    CinemachineVirtualCamera bossRoomCamera;

    private void Start()
    {
        bossRoomCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void EnableBossCamera()
    {
        bossRoomCamera.Priority = 100;
    }

    void SwitchToBossRoomMusic()
    {
        GetComponentInChildren<MusicPlayer>().PlayThisTrack();
    }

    private void DisableBossCamera()
    {
        bossRoomCamera.Priority = 0;
    }

    void SwitchToLevelMusic()
    {
        GetComponentInChildren<MusicPlayer>().PlayerLevelTrack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Player entered boss room");
            EnableBossCamera();
            SwitchToBossRoomMusic();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            print("Player exited boss room");
            DisableBossCamera();
            SwitchToLevelMusic();
        }
    }


}

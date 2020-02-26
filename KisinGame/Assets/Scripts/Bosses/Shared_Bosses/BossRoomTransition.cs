using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossRoomTransition : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera bossRoomCamera;
    VirtualCamera followCamera;
    AudioClip levelMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Player entered boss room");
            followCamera = collision.GetComponent<PlayerIdentifer>().GetVCamera();
            followCamera.DisableVCameraForBossRoom();
            EnableBossCamera();
            SwitchToBossRoomMusic();
        }
    }

    private void EnableBossCamera()
    {
        bossRoomCamera.Priority = 20;
    }

    void SwitchToBossRoomMusic()
    {
        GetComponentInChildren<MusicPlayer>().PlayThisTrack();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            print("Player exited boss room");
            followCamera = collision.GetComponent<PlayerIdentifer>().GetVCamera();
            followCamera.EnableVCamera();
            DisableBossCamera();
            SwitchToLevelMusic();
        }
    }

    private void DisableBossCamera()
    {
        bossRoomCamera.Priority = 5;
    }
    void SwitchToLevelMusic()
    {
        GetComponentInChildren<MusicPlayer>().PlayAnotherTrack(1);
    }
}

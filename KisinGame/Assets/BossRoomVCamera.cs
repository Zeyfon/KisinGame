using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossRoomVCamera : MonoBehaviour
{
    VirtualCamera followCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Player entered boss room");
        if (collision.CompareTag("Player"))
        {
            print("Player entered boss room");
            followCamera = collision.GetComponent<PlayerIdentifer>().GetVCamera();
            followCamera.DisableVCameraForBossRoom();
            EnableBossCamera();
        }
    }

    private void EnableBossCamera()
    {
        transform.parent.GetComponent<CinemachineVirtualCamera>().Priority = 20;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("Player exited boss room");
        if (collision.CompareTag("Player"))
        {
            print("Player entered boss room");
            followCamera = collision.GetComponent<PlayerIdentifer>().GetVCamera();
            followCamera.EnableVCamera();
            DisableBossCamera();
        }
    }

    private void DisableBossCamera()
    {
        transform.parent.GetComponent<CinemachineVirtualCamera>().Priority = 5;
    }
}

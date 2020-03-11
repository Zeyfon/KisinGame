using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamera : MonoBehaviour
{
    static bool firstCamera= false;
    static GameObject camera;
    [SerializeField] SpriteRenderer foreground;

    
    // Start is called before the first frame update
    void Start()
    {
        if (!firstCamera)
        {
            EnableVCamera();
            firstCamera = true;
        }
        else
        {
            EnableForeground();
        }

    }

    public void EnableVCamera()
    {
        Transform playerTransform = FindObjectOfType<PlayerIdentifer>().transform;
        CinemachineVirtualCamera cam = GetComponent<CinemachineVirtualCamera>();
        cam.Priority = 20;
        cam.Follow = playerTransform;
        DisableForeground();
    }

    private void DisableForeground()
    {
        foreground.enabled = false;
    }

    public void DisableVCamera()
    {
        //print("Disabling this camera " + gameObject.name);
        CinemachineVirtualCamera cam = GetComponent<CinemachineVirtualCamera>();
        cam.Priority = 5;
        cam.Follow = null;
        EnableForeground();
    }
    public void DisableVCameraForBossRoom()
    {
        CinemachineVirtualCamera cam = GetComponent<CinemachineVirtualCamera>();
        cam.Priority = 5;
        cam.Follow = null;
    }

    private void EnableForeground()
    {
        foreground.enabled = true;
    }
}

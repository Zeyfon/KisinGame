using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeController : MonoBehaviour
{
    public void ShakeCamera()
    {
        print("CameraWantsToShake");
        CinemachineImpulseSource cameraShake = GetComponent<CinemachineImpulseSource>();
        print(cameraShake);
        cameraShake.GenerateImpulse();
    }
}

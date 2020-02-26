using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeController : MonoBehaviour
{
    public void ShakeCamera()
    {
        GetComponent<CinemachineImpulseSource>().GenerateImpulse();
    }
}

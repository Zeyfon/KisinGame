using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ShakeCamera()
    {
        GetComponent<CinemachineImpulseSource>().GenerateImpulse();
    }
}

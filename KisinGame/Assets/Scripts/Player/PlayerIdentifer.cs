using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentifer : MonoBehaviour
{
    [SerializeField] VirtualCamera activeVCamera;

    public VirtualCamera GetVCamera()
    {
        return activeVCamera;
    }

    public void SetVCamera(VirtualCamera cam)
    {
        activeVCamera = cam;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomExternalCollider : MonoBehaviour
{
    BossCameraControl cameraControl;

    void Start()
    {
        cameraControl = transform.parent.GetComponent<BossCameraControl>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerIdentifer>())
        {
            cameraControl.ExternalCollision();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsFollows : MonoBehaviour
{
    Vector3 offset;
    Transform objectToFollow;
    bool canFollow = false;  

    public void StartFollowing(Transform targetTransform)
    {
        offset = transform.localPosition;
        objectToFollow = targetTransform;
        canFollow = true;
    }

    private void Update()
    {
        if (!canFollow) return;
        transform.position = objectToFollow.position + (Vector3)offset;
    }
}

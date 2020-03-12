using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsFollows : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    Vector3 localOffset;
    Transform objectToFollow;
    bool canFollow = false;  

    public void StartFollowing(Transform targetTransform)
    {
        localOffset = transform.localPosition;
        objectToFollow = targetTransform;
        canFollow = true;
    }

    private void Update()
    {
        if (!canFollow) return;
        transform.position = objectToFollow.position + (Vector3)localOffset + offset;
    }
}

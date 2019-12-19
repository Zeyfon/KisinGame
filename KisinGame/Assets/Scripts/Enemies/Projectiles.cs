using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Projectiles : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;
    Skeleton skeleton;
    Spine.AnimationState spineAnimationState;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.skeleton;
    }

    void Flip(float value)
    {
        skeleton.ScaleX = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SameColorDamageEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public void SameColorEventEffect()
    {
        SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.state.SetAnimation(0, "SameCOLOR", false);
    }
}

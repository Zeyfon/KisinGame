using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class StunEventDamageEffect : MonoBehaviour
{
    
    public void StunEventEffect()
    {
        SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.state.SetAnimation(0, "StunEvent", false);
    }
}

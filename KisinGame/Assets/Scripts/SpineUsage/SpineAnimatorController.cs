using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class SpineAnimatorController : MonoBehaviour
{
    #region Inpsector
    [SpineSkin] public string actualSkin;
    #endregion

    SkeletonAnimation skeletonAnimation;
    Skeleton skeleton;
    Spine.AnimationState spineAnimationState;
    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.skeleton;
        actualSkin = "AMARILLA";
        skeleton.SetSkin("AMARILLA");
    }

    void Flip(float value)
    {
        if (value >= 0)
        {
            skeleton.ScaleX = 1;
        }
        else skeleton.ScaleX = -1;
    }

   void ChangeWeaknessProperty(int weakness)
    {
        print("Changing Skin");
        if (weakness == 1) actualSkin = "AMARILLA"; //skeleton.SetSkin("amarilla");
        else if (weakness == 2) actualSkin = "AZUL";// skeleton.SetSkin("roja");
        else if (weakness == 3) actualSkin = "ROJA";// skeleton.SetSkin("azul");

        skeleton.SetSkin(actualSkin);
    }
}

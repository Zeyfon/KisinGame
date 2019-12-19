using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;


public class SpineActionsEnemy : MonoBehaviour
{
    #region Inpsector
    [SpineSkin] public string actualSkin;
    #endregion
    public int skin = 0;
    SkeletonAnimation skeletonAnimation;
    Skeleton skeleton;
    Spine.AnimationState spineAnimationState;


    void Start()
    {
        skeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.skeleton;

    }


    void Flip(float value)
    {
        skeleton.ScaleX = value;
    }

    void ChangeColor()
    {
        Debug.Log("Entramos");
        switch (actualSkin)
        {
             case "ROJA":
                actualSkin = "AMARILLA";
                skeleton.SetSkin(actualSkin);
                break;
            case "AMARILLA":
                actualSkin = "AZUL";
                skeleton.SetSkin(actualSkin);
                break;
            case "AZUL":
                actualSkin = "AMARILLA";
                skeleton.SetSkin(actualSkin);
                break;
            default:
                actualSkin = "ROJA";
                skeleton.SetSkin(actualSkin);
                break;
        }
    }

}

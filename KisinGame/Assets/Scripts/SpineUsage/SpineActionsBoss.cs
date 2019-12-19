using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using HutongGames.PlayMaker;


public class SpineActionsBoss : MonoBehaviour
{
    #region Inpsector
    [SpineSkin] public string actualSkin;
    #endregion
    SkeletonAnimation skeletonAnimation;
    Skeleton skeleton;
    Spine.AnimationState spineAnimationState;
    PlayMakerFSM pM;


    void Start()
    {
        GameObject weakness;
        skeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.skeleton;
        weakness = gameObject.transform.parent.GetChild(2).gameObject;
        pM = weakness.GetComponent<PlayMakerFSM>(); //Gets the PlayMaker Component of the Weakness child
    }


    void Flip(float value)
    {
        skeleton.ScaleX = value;
    }

    void ChangeColor()
    {
        switch (actualSkin)
        {
             case "ROJA":
                actualSkin = "AMARILLA";
                skeleton.SetSkin(actualSkin);
                pM.SendEvent("ChangeToYellow");
                break;
            case "AMARILLA":
                actualSkin = "AZUL";
                skeleton.SetSkin(actualSkin);
                pM.SendEvent("ChangeToBlue");
                break;
            case "AZUL":
                actualSkin = "ROJA";
                skeleton.SetSkin(actualSkin);
                pM.SendEvent("ChangeToRed");
                break;
            default:
                actualSkin = "AMARILLA";
                skeleton.SetSkin(actualSkin);
                pM.SendEvent("ChangeToYellow");
                break;
        }
    }

}

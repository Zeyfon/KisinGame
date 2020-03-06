using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class BossRoomDoor : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;

    private void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.AnimationState.Complete += IdleState;
    }

    public void CloseDoor()
    {
        print("ClosingDoor");
        skeletonAnimation.state.SetAnimation(0, "CloseDoor", false);

    }

    public void OpenDoor()
    {
        print("Opening door for " + gameObject.name);
        skeletonAnimation.state.SetAnimation(0, "OpenDoor", false);

    }


    void IdleState(Spine.TrackEntry trackEntry)
    {
        print(trackEntry.Animation.Name+ "  Animation Ended");
        //Debug.Break();
        if(trackEntry.Animation.Name == "CloseDoor")
        {
            skeletonAnimation.state.SetAnimation(0, "ClosedLoop", false);
            return;
        }
        else if(trackEntry.Animation.Name == "OpenDoor")
        {
            skeletonAnimation.state.SetAnimation(0, "OpenedLoop", false);
            return;
        }
    }

    public void OpenedState()
    {
        skeletonAnimation.state.SetAnimation(0, "OpenedLoop", false);
        GetComponent<Collider2D>().enabled = false;
    }

    public void ClosedState()
    {
        skeletonAnimation.state.SetAnimation(0, "ClosedLoop", false);
        print("Set to Closed");
    }
}

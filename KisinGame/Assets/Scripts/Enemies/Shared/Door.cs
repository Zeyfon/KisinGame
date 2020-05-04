﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Door : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;

    private void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.AnimationState.Complete += IdleState;
    }

    public void CloseDoor()
    {
        //print("ClosingDoor");
        skeletonAnimation.state.SetAnimation(0, "CloseDoor", false);
        GetComponent<Collider2D>().enabled = true;

    }

    public void OpenDoor()
    {
        //print("Opening door for " + gameObject.name);
        skeletonAnimation.state.SetAnimation(0, "OpenDoor", false);
        GetComponent<Collider2D>().enabled = false;
    }


    void IdleState(Spine.TrackEntry trackEntry)
    {
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
        //print("Set to Closed");
    }
}
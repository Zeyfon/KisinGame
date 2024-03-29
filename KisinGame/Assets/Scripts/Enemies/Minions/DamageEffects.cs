﻿using Spine.Unity;
using System.Collections;
using UnityEngine;

public class DamageEffects : MonoBehaviour
{
    [SerializeField] Transform sameColorTransform;
    [SerializeField] Transform stunEventTransform;
    [SerializeField] Transform stunStateTransform;

    [SerializeField] PlayMakerFSM damageSoundsFSM;
    CameraShakeController cameraShaker;
    Transform playerTransform;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        playerTransform = FindObjectOfType<PlayerIdentifer>().transform;
    }

    public void SameColorEffect(int weakness)
    {
        FlipProcess();
        SkeletonAnimation skeletonAnimation = sameColorTransform.GetComponent<SkeletonAnimation>();
        SetSkin(weakness, skeletonAnimation);
        damageSoundsFSM.SendEvent("SameColor");
        skeletonAnimation.state.SetAnimation(0, "SameCOLOR", false);
    }

    public void StunEventEffect(int weakness)
    {
        FlipProcess();
        if (!cameraShaker) cameraShaker = transform.parent.transform.parent.GetComponent<CameraShakeController>();
        cameraShaker.ShakeCamera();
        SkeletonAnimation skeletonAnimation = stunEventTransform.GetComponent<SkeletonAnimation>();
        SetSkin(weakness, skeletonAnimation);
        damageSoundsFSM.SendEvent("StunEvent");
        skeletonAnimation.state.SetAnimation(0, "StunEvent", false);

    }

    public void NotSameColorEffect(int weakness)
    {
        damageSoundsFSM.SendEvent("NotSameColor");
    }

    public void StunStateEffect(int weakness)
    {
        FlipProcess();
        SkeletonAnimation skeletonAnimation = stunStateTransform.GetComponent<SkeletonAnimation>();
        SetSkin(weakness, skeletonAnimation);
        damageSoundsFSM.SendEvent("StunState");
        skeletonAnimation.state.SetAnimation(0, "SuntState", false);
    }

    private void SetSkin(int i, SkeletonAnimation skeletonAnimation)
    {
        switch (i)
        {
            case 1:
                skeletonAnimation.skeleton.SetSkin("AMARILLA");
                break;
            case 2:
                skeletonAnimation.skeleton.SetSkin("AZUL");
                break;
            case 3:
                skeletonAnimation.skeleton.SetSkin("ROJA");
                break;
            default:
                break;
        }
    }

    private void FlipProcess()
    {
        float parentLocalScaleX = GetParentScale();
        float playerRelativePosition = GetPlayerRelativeXPosition();
        Flip(playerRelativePosition, parentLocalScaleX);
    }
    private float GetParentScale()
    {
        return transform.parent.transform.localScale.x;
    }

    private float GetPlayerRelativeXPosition()
    {
        return playerTransform.position.x - transform.position.x;
    }
    private void Flip(float playerRelativePosition, float parentLocalScaleX)
    {
        if (playerRelativePosition <= 0 && parentLocalScaleX <= 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            return;
        }
        if (playerRelativePosition <= 0 && parentLocalScaleX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            return;
        }
        if (playerRelativePosition > 0 && parentLocalScaleX <= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            return;
        }
        if (playerRelativePosition > 0 && parentLocalScaleX > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            return;
        }
    }
}

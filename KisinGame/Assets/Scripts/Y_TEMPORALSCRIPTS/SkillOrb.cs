﻿using Spine.Unity;
using System.Collections;
using UnityEngine;
using HutongGames.PlayMaker;
using PixelCrushers.DialogueSystem;

public class SkillOrb : MonoBehaviour
{
    [SerializeField] float movingTime = 1;
    Rigidbody2D rb;

    [Header("SkillGottenSound")]
    [UnityEngine.Tooltip("Sound will do when the crystal touches Zyana")]
    [SerializeField] AudioClip skillGottenSound;
    [UnityEngine.Tooltip("The volume will have the sound")]
    [SerializeField] float volume = 1;


    public int dialogueIndex = 0;

    IEnumerator Start()
    {
        InitialSetup();
        yield return new WaitForSeconds(3);
        StartDoubleJumpEvent(dialogueIndex);
    }

    private void InitialSetup()
    {
        rb = GetComponent<Rigidbody2D>();
        FsmEventData myfsmEventData = new FsmEventData();
        myfsmEventData.BoolData = false;
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        FindObjectOfType<DialogueSystemController>().transform.GetComponent<PlayMakerFSM>().SendEvent("DisablePlayerControl");
    }

    public void StartDoubleJumpEvent(int i)
    {
        dialogueIndex = i;
        rb.simulated = true;
        Transform playerTransform = FindObjectOfType<PlayerIdentifer>().transform;
        GetComponent<ParabolicJump>().PerformParabolicJump(rb, playerTransform.position, movingTime, 0);
        StartCoroutine(OrbMoving(rb, playerTransform));
    }
    IEnumerator OrbMoving(Rigidbody2D rb,Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        yield return new WaitForSeconds(movingTime - 0.2f);
        while (distance > 0.4f)
        {
            distance = Vector3.Distance(transform.position, target.position);
            yield return new WaitForEndOfFrame();
        }
        rb.simulated = false;
        SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.state.SetAnimation(0, "explosion", false);
        skeletonAnimation.AnimationState.End += ExplosionEnded;
        PlaySkillUnlockedSound();
    }

    private void PlaySkillUnlockedSound()
    {
        GetComponent<AudioSource>().PlayOneShot(skillGottenSound, volume);
    }

    private void ExplosionEnded(Spine.TrackEntry trackEntry)
    {
        FindObjectOfType<BossRoomController>().BossDead(dialogueIndex);
    }
}

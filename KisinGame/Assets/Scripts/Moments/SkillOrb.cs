using Spine.Unity;
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
        StartCoroutine(InitialSetup());
        yield return new WaitForSeconds(3);
        StartDoubleJumpEvent(dialogueIndex);
    }

    IEnumerator InitialSetup()
    {
        PlayMakerFSM fsm = FindObjectOfType<DialogueSystemController>().transform.GetComponent<PlayMakerFSM>();
        rb = GetComponent<Rigidbody2D>();
        FsmEventData myfsmEventData = new FsmEventData();
        myfsmEventData.BoolData = false;
        fsm.SendEvent("DisableAutomaticPlayerControlRecover");
        yield return new WaitForEndOfFrame();
        fsm.SendEvent("DisablePlayerControl");
    }

    public void StartDoubleJumpEvent(int i)
    {
        dialogueIndex = i;
        rb.simulated = true;
        Transform playerTransform = FindObjectOfType<PlayerIdentifer>().transform;
        Vector3 targetPosition = playerTransform.position + Vector3.up;
        GetComponent<ParabolicJump>().PerformParabolicJump(rb, targetPosition, movingTime, 0);
        StartCoroutine(OrbMoving(rb, targetPosition));
    }
    IEnumerator OrbMoving(Rigidbody2D rb,Vector3 targetPosition)
    {
        float distance = Vector3.Distance(transform.position+ Vector3.up, targetPosition);
        yield return new WaitForSeconds(movingTime - 0.2f);
        while (distance > 0.4f)
        {
            distance = Vector3.Distance(transform.position, targetPosition);
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

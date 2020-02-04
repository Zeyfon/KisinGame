using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class FruitBombBarrageAttack : MonoBehaviour
{
    [SerializeField] Transform middleSpot;
    [SerializeField] float jumpToMiddleTime = 1f;

    Transform bombBarrage;
    PlayMakerFSM myHealthListenerFSM;
    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        myHealthListenerFSM = GetComponent<PlayMakerFSM>();
        bombBarrage = FindObjectOfType<BombBarrage>().transform;
    }
    public void GetPlayerTransform(Transform transform)
    {
        playerTransform = transform;
        //print(playerTransform);
    }

    //Animation Event.
    void FlipToTarget_BombBarrageAttack()
    {
        GetComponent<BossesSupActions>().UpdateCurrentTargetTransform(middleSpot);
        GetComponent<BossesSupActions>().FlipToTarget_SupActions();
    }
    //Animation Event. Produce Sound
    void JumpToTarget_BombBarrageAttack()
    {
        GetComponent<SoundsConnection>().SendSoundEventToFSM("Jump");
        GetComponent<BossesSupActions>().DoParabolicJump(middleSpot.position, jumpToMiddleTime, false);
    }
    //Animation Event
    void AttackStarts_BombBarrageAttack()
    {
        bombBarrage.GetComponent<BombBarrage>().AttackStarts(GetComponent<Level1Boss>().phase);
        if (bombBarrage.GetComponent<BombBarrage>().bossTransform) return;
        bombBarrage.GetComponent<BombBarrage>().bossTransform = transform;
    }

    //Called from BombBarraga Script in FruitBombBarrage GO
    public void AttackEnded()
    {
        GetComponent<Animator>().SetInteger("Attack", 90);
    }

    //AnimationEvent
    void BombBarrageStartSound_BombBarrageAttack()
    {
        GetComponent<SoundsConnection>().SendSoundEventToFSM("BombBarrageStarts");
        print("startSound");
    }
    //AnimationEvent
    void BombBarrageEndsSound_BombBarrageAttack()
    {
        GetComponent<SoundsConnection>().SendSoundEventToFSM("BombBarrageEnds");
        print("EndSound");
    }

    //Animation Event
    void InvulnerabilityOn()
    {
        FsmEventData myfsmEventData = new FsmEventData();
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        myHealthListenerFSM.Fsm.Event("InvulnerabilityOn");
    }
    //Animation Event
    void InvulnerabilityOff()
    {
        FsmEventData myfsmEventData = new FsmEventData();
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        myHealthListenerFSM.Fsm.Event("InvulnerabilityOff");
    }
}

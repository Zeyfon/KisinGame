using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Boss : MonoBehaviour, BossStarter
{
    [Header("Internal Values")]
    [SerializeField] BossRoomPlayerSpotter playerSpotter;
    public int phase = 1;

    Rigidbody2D rb;
    Animator anim;
    StressReceiver stressReceiver;
    Level3BossControlTimers bossTimers;
    EnableBossUI bossUI;
    Transform playerTransform;
    BossDialogues bossDialogues;

    int attackCounter = 0;
    public bool colorChange = false;
    public bool canThrustAttack = false;
    public bool canCrystalRain = false;

    // Start is called before the first frame update
    void Start()
    {

        bossDialogues = GetComponent<BossDialogues>();
        bossDialogues.SetPlayerSpotter(playerSpotter);
        bossTimers = GetComponent<Level3BossControlTimers>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetInteger("Phase", phase);

        StartCoroutine(SetVariablesInDependencies());
        if (phase != 3) playerSpotter.SetCurrentBoss(this);
    }
    IEnumerator SetVariablesInDependencies()
    {
        yield return new WaitForSeconds(1);
        playerTransform = GameObject.FindObjectOfType<PlayerIdentifer>().transform;
        GetComponent<ComboAttackL3B>().GetPlayerTransform(playerTransform);
        GetComponent<BossesSupActions>().GetPlayerTransform(playerTransform);
        GetComponent<ThrustAttack>().GetPlayerTransform(playerTransform);
    }

    #region Control

    public void StartActions()
    {
        StartCoroutine(StartBossCoroutine());
    }

    IEnumerator StartBossCoroutine()
    {
        GetComponent<EnableBossUI>().ActivateBossUI();
        yield return new WaitForSeconds(1);
        if (phase == 3)
        {
            bossTimers.TimerForNextCrystalRainAttack();
            bossTimers.TimerForNextWeaknessChange();
        }
        DecideNextAction();
        bossTimers.TimerForNextThrustAttack();
    }

    void DecideNextAction()
    {
        if (!colorChange && !canThrustAttack && !canCrystalRain && attackCounter < (5 - phase))
        {
            print("Combo Attack");
            attackCounter++;
            StartCoroutine(CloseAttack());
            return;
        }
        else if (!colorChange && !canThrustAttack && !canCrystalRain)
        {
            print("CrystalBall Attack");
            StartCoroutine(CrystalBallsAttack());
            attackCounter = 0;
            return;
        }
        else if (!colorChange && !canCrystalRain)
        {
            print("Thrust Attack");
            StartCoroutine(ThrustAttack());
            return;
        }
        else if (!colorChange)
        {
            print("CrystalRain Attack");
            StartCoroutine(CrystalRainAttack());
            return;
        }
        else
        {
            StartCoroutine(WeaknessChange());
            return;
        }

    }
    #endregion

    #region Actions
    IEnumerator CloseAttack()
    {
        anim.SetInteger("Attack", 0);
        float distance = GetComponent<ComboAttackL3B>().DistanceFromPlayer();
        while (distance > 1)
        {
            distance = GetComponent<ComboAttackL3B>().MoveTowardsPlayer();
            yield return new WaitForEndOfFrame();
        }
        rb.velocity = new Vector2(0, 0);
        anim.Play("AttackPrep1");
        while (anim.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        anim.SetInteger("Attack", 0);
        StartCoroutine(ActionFinished(1));
    }

    IEnumerator CrystalBallsAttack()
    {
        anim.SetInteger("Attack", 0);
        anim.Play("JumpToMiddle");
        while (anim.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        StartCoroutine(ActionFinished(.5f));

        yield return null;
    }

    IEnumerator ThrustAttack()
    {
        GetComponent<ThrustAttack>().AdjustAttackTrigger();
        anim.SetInteger("Attack", 0);
        anim.Play("JumpToSide");
        bossTimers.TimerForNextThrustAttack();
        while (anim.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        anim.SetInteger("Attack", 0);
        StartCoroutine(ActionFinished(.5f));
        yield return null;
    }

    IEnumerator CrystalRainAttack()
    {
        anim.SetInteger("Attack", 0);
        anim.Play("CrystalRainStarts");
        bossTimers.TimerForNextCrystalRainAttack();
        while (anim.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        print("Attack Ended");
        StartCoroutine(ActionFinished(.5f));
        yield return null;
    }

    IEnumerator WeaknessChange()
    {
        anim.SetInteger("Attack", 0);
        anim.Play("WeaknessChange");
        bossTimers.TimerForNextWeaknessChange();
        while (anim.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        //print("ColorChanged");
        StartCoroutine(ActionFinished(.5f));
    }

    IEnumerator ActionFinished(float timeforNextAction)
    {
        //print("Action Finished");
        yield return new WaitForSeconds(timeforNextAction);
        DecideNextAction();
    }
    #endregion

    #region Interruption && Stun && Dead States
    //HealthFSM Event Call
    void Interrupted()
    {
        StoppingOtherCoroutines();
        StartCoroutine(InterruptionStarts());
        GetComponent<SoundsConnection>().SendSoundEventToFSM("Interrupted");
    }

    IEnumerator InterruptionStarts()
    {
        anim.SetInteger("Stun", 0);
        anim.Play("Interruption");
        //print("Interruption Started");
        while (anim.GetInteger("Stun") != 100)
        {
            yield return null;
        }
        //print("Interruption Finished");
        StartCoroutine(ActionFinished(1));
    }

    //HealthFSM Event Call
    void Stunned()
    {
        StoppingOtherCoroutines();
        StartCoroutine(StunStarts());
        GetComponent<SoundsConnection>().SendSoundEventToFSM("Stunned");
    }

    //StunCircle EventCall
    void StunFinished()
    {
        GetComponent<Animator>().SetInteger("Stun", 90);
        //print("StunCirclesDepleted");
    }

    IEnumerator StunStarts()
    {
        anim.SetInteger("Stun", 0);
        anim.Play("StunStarts");
        //print("Stun Starts");
        while (anim.GetInteger("Stun") != 100)
        {
            yield return null;
        }
        //print("Stun Finished");
        StartCoroutine(ActionFinished(1));
    }

    //HealthFSM Event Call
    void Dead()
    {
        StoppingOtherCoroutines();
        StartCoroutine(DeadStarts());
        GetComponent<SoundsConnection>().SendSoundEventToFSM("Dead");
    }

    IEnumerator DeadStarts()
    {
        anim.Play("Dead");
        yield return null;
    }

    //Animation Event
    void Dies()
    {
        bossDialogues.AfterBossDiesActions();
    }

    void StoppingOtherCoroutines()
    {
        StopAllCoroutines();
        GetComponent<CrystalBallAttack>().StoppingAllCoroutines();
        GetComponent<CrystalRainAttack>().Cancel();
    }





    #endregion

    #region PhaseChange
    //Event called from Health FSM in same GO this Script is 
    void PhaseChange(int nextPhase)
    {
        phase = nextPhase;
        anim.SetInteger("Phase", phase);
        colorChange = true;
        if(phase == 2)
        {
            canCrystalRain = true;
        }

        bossTimers.ReduceTimers(phase);
        //print("Phase Change");
    }


    #endregion

}

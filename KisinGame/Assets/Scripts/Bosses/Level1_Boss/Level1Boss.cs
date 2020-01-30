using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level1Boss : MonoBehaviour, BossStarter
{
    #region Inspector
    public int phase = 0;
    [SerializeField] BossRoomController bossRoomController;
    [SerializeField] GameObject pixanDrops;

    [Header("Control Timer Variables")]
    public bool canChangeWeakness = false;
    public bool canThrustAttack = false;
    #endregion

    int closeAttackCounter = 0;

    Boss1ControlTimers bossControlTimers;
    Animator anim;

    void Start()
    {
        bossControlTimers = GetComponent<Boss1ControlTimers>();
        anim = GetComponent<Animator>();
        anim.SetInteger("Phase", phase);
        StartCoroutine(SetVariablesInDependencies());
    }

    IEnumerator SetVariablesInDependencies()
    {
        yield return new WaitForSeconds(1);
        Transform playerTransform = GameObject.FindObjectOfType<PlayerIdentifer>().transform;
        GetComponent<BossesSupActions>().GetPlayerTransform(playerTransform);
        GetComponent<ComboAttackL1B>().GetPlayerTransform(playerTransform);
        GetComponent<FruitBombBarrageAttack>().GetPlayerTransform(playerTransform);
        GetComponent<ThrustAttack>().GetPlayerTransform(playerTransform);
        bossRoomController.SetCurrentBoss(this);
    }

    #region Control
    public void StartActions()
    {
        anim.SetInteger("Phase", phase);
        DecideNextAction();

    }

    void DecideNextAction()
    {
        if (!canChangeWeakness && !canThrustAttack && closeAttackCounter < 3)
        {
            print("Close Attack Combo");
            closeAttackCounter++;
            StartCoroutine(CloseComboAttack());
            return;
        }
        if (!canChangeWeakness && !canThrustAttack)
        {
            print("Fruit Bomb Barrage Attack");
            StartCoroutine(BombBarrageAttack());
            closeAttackCounter = 0;
            return;
        }
        if(!canChangeWeakness)
        {
            print("LaserTime");
            StartCoroutine(ThrustAttack());
            return;
        }
        else
        {
            print("Changing Weakness");
            StartCoroutine(WeaknessChange());
            return;
        }
    }
    #endregion

    #region Actions

    IEnumerator CloseComboAttack()
    {
        anim.SetInteger("Attack", 0);
        float distance = GetComponent<ComboAttackL1B>().DistanceFromPlayer();
        while (distance > 1)
        {
            distance = GetComponent<ComboAttackL1B>().MoveTowardsPlayer();
            yield return new WaitForEndOfFrame();
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        anim.Play("AttackPrep1");
        while (anim.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        anim.SetInteger("Attack", 0);
        StartCoroutine(ActionFinished(1));
    }

    IEnumerator BombBarrageAttack()
    {
        anim.SetInteger("Attack", 0);
        anim.Play("JumpToMiddle");
        while (anim.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        anim.SetInteger("Attack", 0);
        StartCoroutine(ActionFinished(1));
    }

    IEnumerator ThrustAttack()
    {
        anim.SetInteger("Attack", 0);
        anim.Play("JumpToSide");
        bossControlTimers.TimerForNextThrustAttack();
        canThrustAttack = false;
        while (anim.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        anim.SetInteger("Attack", 0);
        StartCoroutine(ActionFinished(1));
    }

    IEnumerator WeaknessChange()
    {
        print("WeaknessChange Started");
        anim.SetInteger("Attack", 0);
        anim.Play("WeaknessChange");
        bossControlTimers.TimerForNextWeaknessChange();
        canChangeWeakness = false;
        while (anim.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        print("WeaknessChange Finished");
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
        while (anim.GetInteger("Stun") != 100)
        {
            yield return null;
        }
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
    }

    IEnumerator StunStarts()
    {
        anim.SetInteger("Stun", 0);
        anim.Play("StunStarts");
        while (anim.GetInteger("Stun") != 100)
        {
            yield return null;
        }
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
        StartCoroutine(AfterBossDiesActions());
    }

    IEnumerator AfterBossDiesActions()
    {   //This int is used by the BossRoomController to know which dialogue
        //in the Dialogue List will get. It uses the int as an index for the List
        int dialogue1 = 1;
        yield return new WaitForSeconds(5f);
        print("Wants to start dialogue");
        bossRoomController.BossDead(dialogue1);
        yield return new WaitForSeconds(1.5f);
        DestroyBoss();
        yield return null;
    }

    public void DestroyBoss()
    {
        Destroy(gameObject);
    }

    void StoppingOtherCoroutines()
    {
        StopAllCoroutines();
    }
    #endregion

    #region PixanDrops Creation
    //Animation Event
    void CreatePixanDrops()
    {
        StartCoroutine(CreatePixanDrops_Coroutine());
    }

    IEnumerator CreatePixanDrops_Coroutine()

    {
        for (int i = 0; i < 6; i++)
        {
            Instantiate(pixanDrops, transform.position, Quaternion.identity, transform.parent.GetChild(0));
            yield return new WaitForSeconds(0.2f);
        }

    }

    #endregion

    #region PhaseChange
    //Event called from Health FSM in same GO this Script is 
    void PhaseChange(int nextPhase)
    {
        phase = nextPhase;
        anim.SetInteger("Phase", phase);
        canChangeWeakness = true;
        if (phase == 2)
        {
            canThrustAttack = true;
            return;
        }

        bossControlTimers.ReduceTimers(phase);
        //print("Phase Change");
    }


    #endregion
}

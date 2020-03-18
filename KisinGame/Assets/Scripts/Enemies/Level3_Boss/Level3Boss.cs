using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Boss : MonoBehaviour, IBossStarter
{
    enum Dialogues
    {
        IzelDialogue=1, MitlanDialogue=2
    }
    [Header("Internal Values")]
    [SerializeField] bool mitlanActive = false;
    public BossRoomController bossRoomController;
    [SerializeField] Dialogues dialogues;
    public int phase = 1;
    public bool colorChange = false;
    public bool canThrustAttack = false;
    public bool canCrystalRain = false;


    Rigidbody2D rb;
    Animator animIzel;
    Animator animMitlan;
    StressReceiver stressReceiver;
    Level3BossControlTimers bossControlTimers;
    Transform playerTransform;
    int dialogueID;


    int attackCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        dialogueID = (int)dialogues;
        bossControlTimers = GetComponent<Level3BossControlTimers>();
        rb = GetComponent<Rigidbody2D>();
        animIzel = GetComponent<Animator>();
        animIzel.SetInteger("Phase", phase);
        if (mitlanActive)
        {
            animMitlan = transform.GetChild(4).GetComponent<Animator>();
            animMitlan.SetInteger("Phase", phase);
        }

        StartCoroutine(SetVariablesInDependencies());
        print("Setting " + gameObject.name + "  " + bossRoomController);
        bossRoomController.SetCurrentBoss(this);
    }
    IEnumerator SetVariablesInDependencies()
    {
        yield return new WaitForSeconds(1);
        playerTransform = GameObject.FindObjectOfType<PlayerIdentifer>().transform;
        GetComponent<ComboAttackL3B>().GetPlayerTransform(playerTransform);
        GetComponent<BossesSupActions>().GetPlayerTransform(playerTransform);
        GetComponent<ThrustAttack>().GetPlayerTransform(playerTransform);
    }
    
    public bool MitlanIsActive()
    {
        return mitlanActive;
    }
    #region Control
    // Called from the Dialogue Level3Dialogue 1 Conversation Ended Event
    public void IStartActions()
    {
        if (phase > 1)
        {
            canCrystalRain = true;
        }
        StartCoroutine(StartBossCoroutine());
    }

    IEnumerator StartBossCoroutine()
    {
        yield return new WaitForSeconds(1);
        if (phase == 3)
        {
            bossControlTimers.TimerForNextCrystalRainAttack();
            bossControlTimers.TimerForNextWeaknessChange();
        }
        DecideNextAction();
        bossControlTimers.TimerForNextThrustAttack();
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
        animIzel.SetInteger("Attack", 0);
        float distance = GetComponent<ComboAttackL3B>().DistanceFromPlayer();
        while (distance > 1)
        {
            distance = GetComponent<ComboAttackL3B>().MoveTowardsPlayer(mitlanActive);
            yield return new WaitForEndOfFrame();
        }
        animIzel.Play("AttackPrep1");
        if (mitlanActive) animMitlan.Play("AttackPrep1");
        while (animIzel.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        animIzel.SetInteger("Attack", 0);
        StartCoroutine(ActionFinished(1));
    }

    IEnumerator CrystalBallsAttack()
    {
        if(mitlanActive)
        {
            animMitlan.SetInteger("Attack", 0);
            animMitlan.Play("JumpToMiddle");
        }
        animIzel.SetInteger("Attack", 0);
        animIzel.Play("JumpToMiddle");
        while (animIzel.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        StartCoroutine(ActionFinished(.5f));

        yield return null;
    }

    IEnumerator ThrustAttack()
    {
        GetComponent<ThrustAttack>().AdjustAttackTrigger();
        if (mitlanActive)
        {
            animMitlan.SetInteger("Attack", 0);
            animMitlan.Play("JumpToSide");
        }
        animIzel.SetInteger("Attack", 0);
        animIzel.Play("JumpToSide");
        bossControlTimers.TimerForNextThrustAttack();
        while (animIzel.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        StartCoroutine(ActionFinished(.5f));
        yield return null;
    }

    IEnumerator CrystalRainAttack()
    {
        if (mitlanActive)
        {
            animMitlan.SetInteger("Attack", 0);
            animMitlan.Play("CrystalRainStarts");
        }
        animIzel.SetInteger("Attack", 0);
        animIzel.Play("CrystalRainStarts");
        bossControlTimers.TimerForNextCrystalRainAttack();
        while (animIzel.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        print("Attack Ended");
        StartCoroutine(ActionFinished(.5f));
        yield return null;
    }

    IEnumerator WeaknessChange()
    {
        animIzel.SetInteger("Attack", 0);
        animIzel.Play("WeaknessChange");
        bossControlTimers.TimerForNextWeaknessChange();
        while (animIzel.GetInteger("Attack") != 100)
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
        if (mitlanActive)
        {
            animMitlan.SetInteger("Stun", 0);
            animMitlan.Play("Interruption");
        }
        animIzel.SetInteger("Stun", 0);
        animIzel.Play("Interruption");
        //print("Interruption Started");
        while (animIzel.GetInteger("Stun") != 100)
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
        if (mitlanActive)
        {
            animMitlan.SetInteger("Stun", 0);
            animMitlan.Play("StunStarts");
        }
        animIzel.SetInteger("Stun", 0);
        animIzel.Play("StunStarts");
        //print("Stun Starts");
        while (animIzel.GetInteger("Stun") != 100)
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
        if (mitlanActive)
        {
            animMitlan.Play("Dead");
        }
        animIzel.Play("Dead");
        yield return null;
    }

    //Animation Event
    void Dies()
    {
        StartCoroutine(AfterBossDiesActions());
    }

    IEnumerator AfterBossDiesActions()
    {
        yield return new WaitForSeconds(0.5f);
        bossRoomController.BossDead(dialogueID);
        yield return new WaitForSeconds(5f);
    }
    public void DestroyBoss()
    {
        Destroy(gameObject);
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
        animIzel.SetInteger("Phase", phase);
        colorChange = true;
        if(phase == 2)
        {
            canCrystalRain = true;
        }

        bossControlTimers.ReduceTimers(phase);
        //print("Phase Change");
    }


    #endregion

}

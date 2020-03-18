using HutongGames.PlayMaker;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class Level2Boss : MonoBehaviour, IBossStarter
{
    #region Inspector
    [Header("Internal Values")]
    [SerializeField] BossRoomController bossRoomController;
    [UnityEngine.Tooltip("Current Boss Phase")]
    [SerializeField] int phase = 0;

    [Header("Necessary Prefabs")]
    [SerializeField] GameObject pixanDrops;
    [SerializeField] TilesManager tileManager;
    #endregion

    Level2BossControlTimers bossControlTimers;
    Animator anim;

    [Header("Control Timer Script variables")]
    public bool canLaserAttack = false;
    public bool canChangeWeakness = false;

    public int missileCounter = 0;
    int closeAttackCounter = 0;


    void Start()
    {
        bossRoomController = FindObjectOfType<BossRoomController>();
        bossControlTimers = GetComponent<Level2BossControlTimers>();
        anim = GetComponent<Animator>();
        anim.SetInteger("Phase", phase);
        StartCoroutine(SetVariablesInDependencies());
    }

    IEnumerator SetVariablesInDependencies()
    {
        yield return new WaitForSeconds(1);
        Transform playerTransform = GameObject.FindObjectOfType<PlayerIdentifer>().transform;
        GetComponent<BossesSupActions>().GetPlayerTransform(playerTransform);
        GetComponent<ComboAttackL2B>().GetPlayerTransform(playerTransform);
        GetComponent<MissilesAttackL2B>().GetPlayerTransform(playerTransform);
        GetComponent<LaserAttackL2B>().GetPlayerTransform(playerTransform);
        bossRoomController.SetCurrentBoss(this);

    }

    #region Control
    public void IStartActions()
    {
        anim.SetInteger("Phase", phase);
        DecideNextAction();
        bossControlTimers.TimerforNextLaserAttack();
        if(phase>1) tileManager.StartTileLoop();
    }

    void DecideNextAction()
    {
        //print("CanLaserAttack  " + canLaserAttack);
        if (!canChangeWeakness && !canLaserAttack && closeAttackCounter < 3)
        {
            print("Close Attack Combo");
            closeAttackCounter++;
            StartCoroutine(CloseComboAttack());
            return;
        }
        if (!canChangeWeakness && !canLaserAttack)
        {
            print("Missile Attack");
            StartCoroutine(MissileAttack());
            closeAttackCounter = 0;
            return;
        }
        if (!canChangeWeakness)
        {
            print("LaserTime");
            StartCoroutine(LaserAttack());
            return;
        }
        else
        {
            print("Changing Weakness");
            StartCoroutine(ChangeWeaknessC());
            return;
        }
    }
    #endregion

    #region Actions
    IEnumerator CloseComboAttack()
    {
        anim.SetInteger("Attack", 0);
        float distance = GetComponent<ComboAttackL2B>().DistanceFromPlayer();
        while (distance > 1)
        {
            distance = GetComponent<ComboAttackL2B>().MoveTowardsPlayer();
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

    IEnumerator MissileAttack()
    {
        GetComponent<MissilesAttackL2B>().SetValuesForMissileAttack();
        anim.SetInteger("Attack", 0);
        missileCounter = 0;
        anim.Play("LateralJump_ML");
        while (missileCounter < 2 * phase)
        {
            yield return null;
        }
        StartCoroutine(ActionFinished(.5f));
    }

    IEnumerator LaserAttack()
    {
        GetComponent<LaserAttackL2B>().SetValuesForLaserAttack();
        anim.SetInteger("Attack", 0);
        anim.Play("LateralJump_LS");
        bossControlTimers.TimerforNextLaserAttack();
        canLaserAttack = false;
        while (anim.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        StartCoroutine(ActionFinished(.5f));
    }

    //Method used to perform the Change Weakness Animation
    IEnumerator ChangeWeaknessC()
    {
        anim.SetInteger("Attack", 0);
        anim.Play("WeaknessChange");
        bossControlTimers.TimerForNextWeaknessChange();
        canChangeWeakness = false;
        while (anim.GetInteger("Attack") != 100)
        {
            yield return null;
        }
        anim.SetInteger("Attack", 0);
        StartCoroutine(ActionFinished(0.5f));
    }

    IEnumerator ActionFinished(float timeforNextAction)
    {
        //print("Action Finished");
        yield return new WaitForSeconds(timeforNextAction);
        DecideNextAction();
    }
    #endregion

    #region Interruption && Stun && Dead States

    //Health FSM Event
    public void Stunned()
    {
        StoppinpAllOtherActions();
        StartCoroutine(StunStarts());
    }

    IEnumerator StunStarts()
    {
        print("StunStarts");
        anim.Play("StunStart");
        while (anim.GetInteger("Stun") < 100)
        {
            yield return null;
        }
        print("StunFinishes");
        anim.SetInteger("Stun", 0);
        DecideNextAction();
        yield return null;
    }

    public void StunFinished()
    {
        anim.SetInteger("Stun", 5);
    }

    //Health FSM Event
    public void Interrupted()
    {
        StoppinpAllOtherActions();
        StartCoroutine(InterruptionStarts());
    }

    IEnumerator InterruptionStarts()
    {
        print("Interrupted");
        anim.Play("Interrupted");
        while (anim.GetInteger("Stun") < 100)
        {
            yield return null;
        }
        anim.SetInteger("Stun", 0);
        DecideNextAction();
        yield return null;
    }

    //Health FSM Event
    public void Dead()
    {
        StopAllCoroutines();
        StartCoroutine(BeingDead());
    }

    IEnumerator BeingDead()
    {
        print("Dead");
        anim.Play("Dead");
        yield return null;
    }

    //Animation Event
    void Dies()
    {
        StartCoroutine(AfterBossDiesActions());
    }
    IEnumerator AfterBossDiesActions()
    {
        //This int is to tell the BossRoomController 
        //to run the item in the index dialogue0 = 0
        yield return new WaitForSeconds(2.8f);
        print("Wants to start dialogue");
        bossRoomController.BossDead(1);
        yield return new WaitForSeconds(0.5f);
        DestroyBoss();
    }

    public void DestroyBoss()
    {
        Destroy(transform.parent.gameObject);
    }

    void StoppinpAllOtherActions()
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
    //Health FSM Event
    void PhaseChange (int nextPhase)
    {
        phase = nextPhase;
        anim.SetInteger("Phase", phase);
        if (phase == 2)
        {
            canChangeWeakness = true;
        }
        if (phase == 2)
        {
            tileManager.StartTileLoop();
        }
        bossControlTimers.ReduceTimers(phase);
    }
    #endregion
}

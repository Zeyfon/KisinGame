using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using Random = UnityEngine.Random;

public class CrystalBallAttack : MonoBehaviour
{
    #region Inspector Variables
    [Header("Internal Variables")]
    [UnityEngine.Tooltip("Set at runtinme")]
    [SerializeField] Transform middleSpot = null;
    [SerializeField] float jumpToInitialPositionTime = 1;

    [Header("CrystalBalls Variables")]
    [Range(0, 5)]
    [UnityEngine.Tooltip("The rotation velocity of the balls")]
    [SerializeField] float angularVelocity = 8;
    [UnityEngine.Tooltip("The time the balls will be rotating")]
    [SerializeField] float rotationTime = 6;
    [UnityEngine.Tooltip("Distance from the Boss")]
    [SerializeField] float distanceFromOrigin = 2;

    [SerializeField] List<Transform> crystalBalls = new List<Transform>();
    List<int> weaknesses = new List<int> { 1, 2, 3 };
    [UnityEngine.Tooltip("This counter will be adding the destroyed crystalballs by the player")]
    public int ballCounter = 0;
    #endregion

    PlayMakerFSM myHealthListenerFSM;
    Coroutine ballTimerCoroutine;
    Coroutine rotationCoroutine;
    bool attack = false;
    bool mitlanActive = false;
    int counter = 0;
    int currentActiveBalls = 0;

    private void Start()
    {
        BossRoomController bossRoomController = GetComponent<Level3Boss>().bossRoomController;
        middleSpot = bossRoomController.transform.parent.GetChild(2);
        myHealthListenerFSM = GetComponent<PlayMakerFSM>();
        mitlanActive = GetComponent<Level3Boss>().MitlanIsActive();
    }

    //Called from Animation. Produce Sound 
    //This functions starts the whole crystalballs movements and animations
    void CrystalBalls_Start()
    {
        GetComponent<SoundsConnection>().SendSoundEventToFSM("CrystalBallActivation");
        ballTimerCoroutine = StartCoroutine(BallTimer());
        SetWeaknessToBalls();
        SetCrystalBallPosition();
        EnableCrystalBalls();
        CrystalBall_Rotation();
    }

    #region SetWeaknesstoBalls
    void SetWeaknessToBalls()
    {
        List<int> usedWeaknesses = new List<int>();
        List<Transform> usedCrystalBalls = new List<Transform>();
        int phase = GetComponent<Level3Boss>().phase;

        int currentWeakness = GetPhaseOneWeakness(usedWeaknesses);
        SetPhaseOneCrystalBalls(usedCrystalBalls, phase, currentWeakness);

        if (phase < 2) return;
        currentWeakness = GetNewWeakness(usedWeaknesses, currentWeakness, phase);
        SettWeaknesssToBall(usedCrystalBalls, currentWeakness, phase);

        if (phase < 3) return;
        currentWeakness = GetNewWeakness(usedWeaknesses, currentWeakness, phase);
        SettWeaknesssToBall(usedCrystalBalls, currentWeakness, phase);
    }

    int GetPhaseOneWeakness(List<int> usedWeaknesses)
    {
        int currentWeakness = weaknesses[Random.Range(0, weaknesses.Count)];
        usedWeaknesses.Add(currentWeakness);
        return currentWeakness;
    }

    void SetPhaseOneCrystalBalls(List<Transform> usedCrystalBalls, int phase, int currentWeakness)
    {
        int ballsQuantityForFirstSetup = 4 - phase;
        for (int i = 0; i < ballsQuantityForFirstSetup; i++)
        {
            crystalBalls[i].GetComponent<CrystalBall>().weakness = currentWeakness;
            usedCrystalBalls.Add(crystalBalls[i]);
        }
    }

    int GetNewWeakness(List<int> temporalWeaknesses, int currentWeakness, int phase)
    {
        for (int i = 0; i < weaknesses.Count; i++)
        {
            if (!temporalWeaknesses.Contains(weaknesses[i]))
            {
                currentWeakness = weaknesses[i];
                temporalWeaknesses.Add(currentWeakness);
                return currentWeakness;
            }
        }
        return currentWeakness;
    }

    void SettWeaknesssToBall(List<Transform> currentCrystalBalls, int currentWeakness, int phase)
    {
        foreach (Transform crystalBall in crystalBalls)
        {
            if (!currentCrystalBalls.Contains(crystalBall))
            {
                crystalBall.GetComponent<CrystalBall>().weakness = currentWeakness;
                currentCrystalBalls.Add(crystalBall);
                return;
            }
        }
    }
    #endregion

    void SetCrystalBallPosition()
    {
        float angle = 120 * (Mathf.PI / 180);
        crystalBalls[0].localPosition = new Vector3(0, distanceFromOrigin, 0);
        for (int i = 1; i < 3; i++)
        {
            crystalBalls[i].localPosition =
            new Vector3((crystalBalls[i - 1].localPosition.x * Mathf.Cos(angle)) - (crystalBalls[i - 1].localPosition.y * Mathf.Sin(angle)),
                        (crystalBalls[i - 1].localPosition.x * Mathf.Sin(angle)) + (crystalBalls[i - 1].localPosition.y * Mathf.Cos(angle)),
                        0);
        }
    }
    void EnableCrystalBalls()
    {
        foreach(Transform crystalball in crystalBalls)
        {
            crystalball.gameObject.SetActive(true);
        }
    }

    #region CrystalBall_Rotation
    void CrystalBall_Rotation()
    {
        counter = 0;
        attack = false;
        rotationCoroutine = StartCoroutine(CrystalBallsRotation());
    }

    IEnumerator CrystalBallsRotation()
    {
        float angle = 120 * (Mathf.PI / 180);
        float initialAngle = angularVelocity * (Mathf.PI / 180);
        float timer = 0;
        bool flag = false;
        while (true)
        {
            crystalBalls[0].localPosition = new Vector3((crystalBalls[0].localPosition.x * Mathf.Cos(initialAngle)) - (crystalBalls[0].localPosition.y * Mathf.Sin(initialAngle)),
                     (crystalBalls[0].localPosition.x * Mathf.Sin(initialAngle)) + (crystalBalls[0].localPosition.y * Mathf.Cos(initialAngle)),
                     0);
            for (int i = 1; i < 3; i++)
            {
                crystalBalls[i].localPosition =
                    new Vector3((crystalBalls[i - 1].localPosition.x * Mathf.Cos(angle)) - (crystalBalls[i - 1].localPosition.y * Mathf.Sin(angle)),
                                (crystalBalls[i - 1].localPosition.x * Mathf.Sin(angle)) + (crystalBalls[i - 1].localPosition.y * Mathf.Cos(angle)),
                                0);
            }
            timer += Time.deltaTime;
            if(timer > rotationTime && !flag)
            {
                GetComponent<Animator>().SetInteger("Attack", 80);
                if (mitlanActive) transform.GetChild(4).GetComponent<Animator>().SetInteger("Attack", 80);
                flag = true;
            }
            yield return null;
        }
    }

    //Animation Event.
    void FlipToTarget_CrystalBallAttack()
    {
        GetComponent<BossesSupActions>().UpdateCurrentTargetTransform(middleSpot);
        GetComponent<BossesSupActions>().FlipToTarget_SupActions();
    }
    //Animation Event. Produce Sound
    void JumpToTarget_CrystalBallAttack()
    {
        GetComponent<SoundsConnection>().SendSoundEventToFSM("Jump");
        GetComponent<BossesSupActions>().DoParabolicJump(middleSpot.position, jumpToInitialPositionTime, false);
    }

    //Animation Event. Produce Sound
    void CrystalBallAttack_Starts()
    {
        GetComponent<SoundsConnection>().SendSoundEventToFSM("CrystalBallAttackStart");
        SetInvulnerabilityInBalls();
        StopCoroutine(rotationCoroutine);
        StartCoroutine(CrystalBallAttackStarts());
    }

    void SetInvulnerabilityInBalls()
    {
        foreach(Transform crystalBall in crystalBalls)
        {
            if (crystalBall.gameObject.activeInHierarchy)
            {
                crystalBall.GetComponent<CrystalBall>().SetInvulnerabilityToBalls();
            }
        }
    }

    IEnumerator CrystalBallAttackStarts()
    {
        currentActiveBalls = 0;
        StopCoroutine(ballTimerCoroutine);
        yield return new WaitForSeconds(0.4f);
        foreach (Transform crystalBall in crystalBalls)
        {
            if (crystalBall.gameObject.activeInHierarchy)
            {
                if (crystalBall.gameObject.activeInHierarchy)
                {
                    crystalBall.GetComponent<CrystalBall>().StartAttack();
                    currentActiveBalls++;
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
    #endregion

    IEnumerator BallTimer()
    {
        ballCounter = 0;
        while (true)
        {
            if (ballCounter == 3)
            {
                StopCoroutine(ballTimerCoroutine);
                FsmEventData myfsmEventData = new FsmEventData();
                HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
                myHealthListenerFSM.Fsm.Event("CompleteCircleDamage");
                yield break;
            }
            yield return null;
        }
    }

    public void StoppingAllCoroutines()
    {
        StopAllCoroutines();
    }

    public void FinishedBallAttack()
    {
        counter++;
        if(counter == currentActiveBalls)
        {
            GetComponent<Animator>().SetInteger("Attack", 90);
            if (mitlanActive) transform.GetChild(5).GetComponent<Animator>().SetInteger("Attack", 90);

        }
    }

    void InvulnerabilityOn()
    {
        FsmEventData myfsmEventData = new FsmEventData();
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        myHealthListenerFSM.Fsm.Event("InvulnerabilityOn");
    }
    void InvulnerabilityOff()
    {
        FsmEventData myfsmEventData = new FsmEventData();
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        myHealthListenerFSM.Fsm.Event("InvulnerabilityOff");
    }

}

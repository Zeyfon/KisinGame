using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3BossControlTimers : MonoBehaviour
{
    [Header("Thrust Attack Variables")]
    [SerializeField] float thrustTimerThreshold = 30;

    [Header("Crystal Rain Attack Variables")]
    [SerializeField] float rainTimerThreshold = 30;

    [Header("Weakness Change Variables")]
    [SerializeField] float weaknessTimerThreshold = 20;

    Level3Boss boss;

    private void Start()
    {
        boss = GetComponent<Level3Boss>();
    }

    #region Action Timers
    public void TimerForNextWeaknessChange()
    {
        StartCoroutine(TimerForNextWeaknessChange_Coroutine());
    }
    IEnumerator TimerForNextWeaknessChange_Coroutine()
    {
        boss.colorChange = false;
        float timer = 0;
        while (timer < weaknessTimerThreshold)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        boss.colorChange = true;
    }

    public void TimerForNextThrustAttack()
    {
        StartCoroutine(TimerForNextThrustAttack_Coroutine());
    }
    IEnumerator TimerForNextThrustAttack_Coroutine()
    {
        boss.canThrustAttack = false;
        float timer = 0;
        while (timer < thrustTimerThreshold)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        boss.canThrustAttack = true;
    }

    public void TimerForNextCrystalRainAttack()
    {
        StartCoroutine(TimerForNextCrystalRainAttack_Coroutine());
    }
    IEnumerator TimerForNextCrystalRainAttack_Coroutine()
    {
        boss.canCrystalRain = false;
        float timer = 0;
        while (timer < rainTimerThreshold)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        boss.canCrystalRain = true;
    }
    #endregion

    public void ReduceTimers(float phase)
    {
        weaknessTimerThreshold -= 5;
        thrustTimerThreshold -= 20;
        rainTimerThreshold -= 15;
    }
}

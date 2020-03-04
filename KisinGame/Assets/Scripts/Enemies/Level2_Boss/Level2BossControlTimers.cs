using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2BossControlTimers : MonoBehaviour
{
    [Header("Laser Attack Variables")]
    [SerializeField] float laserTimerThreshold = 30;

    [Header("Weakness Change Variables")]
    [SerializeField] float weaknessTimerThreshold = 30;

    Level2Boss boss;
    private void Start()
    {
        boss = GetComponent<Level2Boss>();
    }

    #region ActionTimers
    public void TimerforNextLaserAttack()
    {
        StartCoroutine(LaserTimer());
    }
    IEnumerator LaserTimer()
    {
        print("Laser Timer Starts");
        float timer = 0;
        while (timer < laserTimerThreshold)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        print("Laser Timer Ends");
        boss.canLaserAttack = true;
    }

    public void TimerForNextWeaknessChange()
    {
        StartCoroutine(WeaknessTimer());
    }
    IEnumerator WeaknessTimer()
    {
        float timer = 0;
        while (timer < weaknessTimerThreshold)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        boss.canChangeWeakness = true;

    }
    #endregion

    public void ReduceTimers(float phase)
    {
        weaknessTimerThreshold -= 1 / phase * weaknessTimerThreshold;
        laserTimerThreshold -= 1 / phase * laserTimerThreshold;
    }
}

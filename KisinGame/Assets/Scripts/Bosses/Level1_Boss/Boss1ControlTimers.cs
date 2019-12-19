using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1ControlTimers : MonoBehaviour
{
    [Header("Weakness Change Time Interval")]
    [SerializeField] float weaknessTimerThreshold = 30;
    [Header("Thrust Attack Time Interval")]
    [SerializeField] float thrustTimerThreshold = 30;

    Level1Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<Level1Boss>();
    }

    #region ActionTimers

    public void TimerForNextThrustAttack()
    {
        StartCoroutine(ThrustAttack());
    }
    IEnumerator ThrustAttack()
    {
        float timer = 0;
        while (timer < thrustTimerThreshold)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        boss.canThrustAttack = true;
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
        thrustTimerThreshold -= 1 / phase * thrustTimerThreshold;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttackL2B : MonoBehaviour
{
    [Header("Laser Mechanics Stuff")]
    [SerializeField] GameObject laserBulletGO;
    [SerializeField] int laserBulletDamage = 20;
    [SerializeField] float laserBulletSpeed = 5;
    [UnityEngine.Tooltip("Time between Laser Attacks")]
    [SerializeField] float laserTimerLimit = 50;
    [UnityEngine.Tooltip("Time the boss will spend in Laser Targeting State")]
    [SerializeField] float laserTargetingTime = 4;

    [SerializeField] float jumpTimeLaserAttack = 1;
    [SerializeField] Transform laserSpawnPosition = null;

    Vector3 jumpTargetPosition;
    Transform playerTransform;

    public void GetPlayerTransform(Transform target)
    {
        playerTransform = target;
    }

    public void SetValuesForLaserAttack()
    {
        jumpTargetPosition = GetComponent<BossesSupActions>().GetFartestSideFromTarget(playerTransform.position);
        GetComponent<Animator>().SetFloat("FlightTime", 1 / jumpTimeLaserAttack);
    }
    //Animation Event
    void JumpToTarget_LaserAttack()
    {
        GetComponent<BossesSupActions>().FlipToTarget_SupActions();
        GetComponent<SoundsConnection>().SendSoundEventToFSM("Jump");
        GetComponent<BossesSupActions>().DoParabolicJump(jumpTargetPosition, jumpTimeLaserAttack, false);
    }

    void StartTargetingTimer_LaserAttack()
    {
        StartCoroutine(TargetingPlayer());
    }

    IEnumerator TargetingPlayer()
    {
        float timer = 0;
        while (timer <= laserTargetingTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        //The Attack variable in the AnimController will allow the transition from Loop State to Fire State in the Laser Shot SubStateMachine.
        GetComponent<Animator>().SetInteger("Attack", 50);
    }

    //Animation Event
    #region Laser Bullet
    void LaserBullet_Create()
    {
        Vector3 targetDirection;
        if (transform.localScale.x == 1) targetDirection = new Vector3(1, 0, 0);
        else targetDirection = new Vector3(-1, 0, 0);
        /*if (playerTransform.transform.position.x > transform.position.x) targetDirection = new Vector3(1, 0, 0);
        else targetDirection = new Vector3(-1, 0, 0);*/
        float angle = 0;
        /*if (targetDirection.x >= 0) targetDirection = new Vector3(1, 0, 0);
        else targetDirection = new Vector3(-1, 0, 0);*/
        angle = GetAngle(targetDirection);
        GetComponent<SoundsConnection>().SendSoundEventToFSM("LaserSpawn");
        print(targetDirection.x);
        GameObject laserBulletClone = Instantiate(laserBulletGO, laserSpawnPosition.position, Quaternion.Euler(0,0,angle)/*Quaternion.Euler(0, 0, angle)*/, transform.parent.parent.transform.GetChild(1));
        LaserBullet laserBullet = laserBulletClone.GetComponent<LaserBullet>();
        laserBullet.SetParameters(laserBulletDamage, laserBulletSpeed, targetDirection);
        //Debug.Break();
    }

    float GetAngle(Vector3 targetDirection)
    {
        float angle = 0;
        if (targetDirection.x >= 0 && targetDirection.y >= 0)
        {
            angle = 0;
        }
        else if (targetDirection.x <= 0 && targetDirection.y >= 0)
        {
            angle = 180;
        }
        else if (targetDirection.x <= 0 && targetDirection.y <= 0)
        {
            angle = 180;
        }
        else if (targetDirection.x >= 0 && targetDirection.y <= 0)
        {
            angle = 0;
        }
        return angle;
    }
    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilesAttackL2B : MonoBehaviour
{
    [Header("Internal Values")]
    [SerializeField] GameObject missile;
    [SerializeField] float jumpTimeMissileAttack = 1;

    [Header("Missiles Spawn Locations")]
    [SerializeField] Transform location1;
    [SerializeField] Transform location2;
    [SerializeField] Transform location3;
    [SerializeField] Transform location4;
    [SerializeField] Transform location5;
    [SerializeField] Transform location6;

    Vector3 jumpTargetPosition;
    Transform playerTransform;
    public void GetPlayerTransform(Transform target)
    {
        playerTransform = target;
    }


    public void SetValuesForMissileAttack()
    {
        jumpTargetPosition =GetComponent<BossesSupActions>().GetFartestSideFromTarget(playerTransform.position);
        GetComponent<Animator>().SetFloat("FlightTime", 1 / jumpTimeMissileAttack);
    }

    //Animation Event
    void JumpToTarget_MissileAttack()
    {
        GetComponent<BossesSupActions>().FlipToTarget_SupActions();
        GetComponent<SoundsConnection>().SendSoundEventToFSM("Jump");
        GetComponent<BossesSupActions>().DoParabolicJump(jumpTargetPosition, jumpTimeMissileAttack, false);
    }

    //Animation Event
    void MissileCreate_MissileAttack(int number)
    {
        switch (number)
        {
            case 1:
                CreateMissile(location1);
                break;
            case 2:
                CreateMissile(location2);
                break;
            case 3:
                CreateMissile(location3);
                break;
            case 4:
                CreateMissile(location4);
                break;
            case 5:
                CreateMissile(location5);
                break;
            case 6:
                CreateMissile(location6);
                break;
        }

    }

    void CreateMissile(Transform missileSpawner)
    {
        GetComponent<SoundsConnection>().SendSoundEventToFSM("MissileSpawn");
        GameObject missileClone = Instantiate(missile, missileSpawner.position, Quaternion.identity, transform.parent.parent.transform.GetChild(1));
        HomingMissile homingMissile = missileClone.GetComponent<HomingMissile>();
        homingMissile.bossTransform = transform;
        homingMissile.playerTransform = playerTransform;
        GetComponent<Level2Boss>().missileCounter++;
        print(GetComponent<Level2Boss>().missileCounter);

    }

    public void MissileDestroyed()
    {
        //GetComponent<Level2Boss>().missileCounter++;
        //print(GetComponent<Level2Boss>().missileCounter);

    }

}

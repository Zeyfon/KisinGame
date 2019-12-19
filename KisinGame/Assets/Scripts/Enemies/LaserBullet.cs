using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class LaserBullet : MonoBehaviour
{
    PlayMakerFSM[] pmFSMs;
    int damage;
    float speed;

    public void SetParameters(int laserDamage,float laserSpeed, Vector3 targetDirection)
    {
        speed = laserSpeed;
        transform.GetChild(0).GetComponent<AttackTrigger>().damage = laserDamage;
        StartCoroutine(LaserMove(targetDirection));
    }

    IEnumerator LaserMove(Vector3 targetDirection)
    {
        float timer = 0;
        while (GetComponent<Animator>().GetInteger("Attack") < 2)
        {
            yield return null;
        }
        while (timer < 7f)
        {
            transform.position += targetDirection * speed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}

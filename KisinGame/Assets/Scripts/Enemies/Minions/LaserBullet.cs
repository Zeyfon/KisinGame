using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using Spine;
using Spine.Unity;

public class LaserBullet : MonoBehaviour
{
    PlayMakerFSM[] pmFSMs;
    int damage;
    float speed;
    Vector3 direction;
    private void Awake()
    {
        transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
    }

    public void SetParameters(int laserDamage,float laserSpeed, Vector3 targetDirection)
    {
        speed = laserSpeed;
        transform.GetChild(0).GetComponent<AttackTrigger>().damage = laserDamage;
        direction = targetDirection;
    }

    void StartMoving()
    {
        StartCoroutine(LaserMove(direction));
    }

    IEnumerator LaserMove(Vector3 targetDirection)
    {
        print("Moving");
        transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
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

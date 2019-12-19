using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class LaserShot : MonoBehaviour
{
    Coroutine coroutine;
    PlayMakerFSM[] pmFSMs;
    int damage = 5;
    float speed = 5;
    Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopCoroutine(coroutine);
        BoxCollider2D boxColl = GetComponent<BoxCollider2D>();
        boxColl.enabled = false;
        rb.velocity = new Vector2(0,0);
        if (collision.CompareTag("PlayerBody"))
        {
            GameObject player;
            player = collision.transform.parent.gameObject;
            pmFSMs = player.GetComponents<PlayMakerFSM>();
            FsmEventData myfsmEventData = new FsmEventData();
            myfsmEventData.IntData = damage;
            myfsmEventData.GameObjectData = gameObject;
            HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
            pmFSMs[1].Fsm.Event("_PlayerDamaged");
        }
        Animator animator = GetComponent<Animator>();
        animator.Play("ObjectHit");

    }

    public void LaserFlies2(Vector3 laserDirection, int laserDamage, float laserSpeed)
    {
        damage = laserDamage;
        speed = laserSpeed;
        coroutine = StartCoroutine(LaserFlies(laserDirection));
    }

    IEnumerator LaserFlies(Vector3 laserDirection)
    {
        Vector2 newLaserDirection = laserDirection;

        rb = GetComponent<Rigidbody2D>();
        while (true)
        {
            rb.velocity = laserDirection *speed*Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }
    }

    void GameObject_Destroy()
    {
        
        Destroy(gameObject);
    }
}


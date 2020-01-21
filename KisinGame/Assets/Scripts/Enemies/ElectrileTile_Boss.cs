using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class ElectrileTile_Boss : MonoBehaviour
{
    [UnityEngine.Tooltip("Damage the Player will receive")]
    [SerializeField] int damage = 22;

    Animator animator;
    GameObject player;
    PlayMakerFSM[] pmFSMs;
    Collider2D coll;
    AudioSource audioSource;

    public void StartAttack_Signal()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartAttack());
    }

    IEnumerator StartAttack()
    {
        animator.Play("PrepareAttack");
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            if (!player)
            {
                player = collision.transform.parent.gameObject;
                pmFSMs = player.GetComponents<PlayMakerFSM>();
            }
            coll.enabled = false;
            FsmEventData myfsmEventData = new FsmEventData();
            myfsmEventData.IntData = damage;
            myfsmEventData.GameObjectData = gameObject;
            HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
            pmFSMs[1].Fsm.Event("_PlayerDamaged");

        }
    }

    void DamageCollider_Enable()
    {
        if (!coll)
        {
            coll = GetComponent<Collider2D>();
        }
        coll.enabled = true;
    }

    void DamageCollider_Disable()
    {
        coll.enabled = false;
    }

    void Electric_Sound()
    {
        if(audioSource == null) audioSource = transform.parent.parent.GetComponent<AudioSource>();
        print(audioSource.gameObject.name);
        audioSource.Play();
    }

}

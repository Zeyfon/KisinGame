using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class ElectrileTile : MonoBehaviour
{
    [Header("Tunning Values")]
    [UnityEngine.Tooltip("Damage the Player will receive")]
    [SerializeField] int damage = 22;
    [UnityEngine.Tooltip("How long until starts its looping process")]
    [SerializeField] float startTime = 1;
    [UnityEngine.Tooltip("How long until will stay in Idle state(Looping Process)")]
    [SerializeField] float idleTime = 5;

    Animator animator;
    GameObject player;
    PlayMakerFSM[] pmFSMs;
    Collider2D coll = null;
    HazardBatch hazardBatch;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        hazardBatch = transform.parent.GetComponent<HazardBatch>();
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
    }
    //Function called by the parent (HazardManager)
    public void StartActions()
    {
        StartCoroutine(TimerToStart());
    }

    void DamageCollider_Enable()
    {
        coll.enabled = true;
    }

    void DamageCollider_Disable()
    {
        coll.enabled = false;
        StartCoroutine(Idling());
    }

    IEnumerator TimerToStart()
    {
        yield return new WaitForSeconds(startTime);
        StartCoroutine(Idling());
    }

    IEnumerator Idling()
    {
        yield return new WaitForSeconds(idleTime);
        animator.Play("PrepareAttack");
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D collision)
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

    void Electric_Sound()
    {
        hazardBatch.StartElectricTSound();
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class DroneBBomb : MonoBehaviour
{
    [SerializeField] int damage = 0;
    [Header("DiegoSounds")]
    [SerializeField] AudioClip explosionClip;
    [SerializeField] float volume = 1;

    Collider2D floorDetector;
    Collider2D playerDetector;
    Coroutine coroutine;
    Animator animator;
    PlayMakerFSM[] pmFSMs;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player) Debug.LogWarning("Player wasn't found");
        animator = GetComponent<Animator>();
        floorDetector = GetComponent<Collider2D>();
        playerDetector = transform.GetChild(0).GetComponent<Collider2D>();
        pmFSMs = player.GetComponents<PlayMakerFSM>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Starts detonation");
        animator.Play("Detonation");
    }
    
    //Animation Event
    void Explosion_Sound()
    {
        print("Explosion Sound " + gameObject);
        GetComponent<AudioSource>().PlayOneShot(explosionClip, volume);
    }

    //Animation Event
    void ExplosionCollider_Enable()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.WakeUp();
        playerDetector.enabled = true;
    }

    //Animation Event
    void ExplosionCollider_Disable()
    {
        playerDetector.enabled = false;
    }

    //Animation Event
    void GameObject_Destroy()
    {
        Destroy(gameObject);
    }

    public void SetDamage(int damage1)
    {
        damage = damage1;
    }

    public int SendDamage()
    {
        return damage;
    }
    public Transform SendTransform()
    {
        return player.transform;
    }
    void SendDamageToPlayer()
    {
        FsmEventData myfsmEventData = new FsmEventData();
        myfsmEventData.IntData = damage;
        myfsmEventData.GameObjectData = gameObject;
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        pmFSMs[1].Fsm.Event("_PlayerDamaged");
    }

}

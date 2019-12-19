using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
    [Header("Tunning Variables")]
    [SerializeField] float height = 5f;                 [UnityEngine.Tooltip("height the missile with get before targeting the player")]
    [SerializeField] float upwardSpeed = 5f;            [UnityEngine.Tooltip("Speed at which flies upward")]
    [SerializeField] float targetSpeed = 5f;            [UnityEngine.Tooltip("Speed at which flies folowwing the player")]
    [SerializeField] float  initialRotateSpeed = 50f;   [UnityEngine.Tooltip("Speed at which rotates. Faster at first")]
    [SerializeField] float finalRotateSpeed = 5f;       [UnityEngine.Tooltip("Speed at which rotates. Lower to stop it from pursuing too much the player")]
    [SerializeField] float timeToStopRotate = 2f;       [UnityEngine.Tooltip("Time after it begings to rotate towards the player to stop it to rotate")]
    [SerializeField] int damage = 20;                    [UnityEngine.Tooltip("Damage done to the player")]
    public Transform bossTransform = null;
    public Transform playerTransform = null;
                

    Rigidbody2D rb;
    Animator animator;
    Coroutine coroutine;
    bool canDamage = true;

    //HomingMissile homingMissile;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(MovingUpwards());
    }

    #region Actions
    IEnumerator MovingUpwards()
    {
        float initialYPos = transform.position.y;
        float currentYPos=0; 
        while (currentYPos < (initialYPos + height))
        {
            yield return new WaitForFixedUpdate();
            rb.velocity = transform.up * upwardSpeed;
            currentYPos = transform.position.y;

        }
        yield return new WaitForSeconds(0.2f);
        animator.Play("MovetoTarget");
        yield return new WaitForSeconds(0.3f);
        GetComponent<Collider2D>().enabled = true;
        coroutine = StartCoroutine(Rotation());
    }

    IEnumerator Rotation()
    {
        bool changeRotationSpeed = false;
        float timer = 0;
        float currentRotationSpeed = initialRotateSpeed;
        
         while(true)
         {
             yield return new WaitForFixedUpdate();
            Vector2 direction = (Vector2)playerTransform.position - rb.position;
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            if (!changeRotationSpeed)
            {
                timer += Time.fixedDeltaTime;
                if (timer > timeToStopRotate)
                {
                    currentRotationSpeed = finalRotateSpeed;
                    changeRotationSpeed = true;
                }
            }
            rb.angularVelocity = -rotateAmount * currentRotationSpeed;
            rb.velocity = transform.up * targetSpeed;
         }
    }
    #endregion

    #region DamageSender

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopCoroutine(coroutine);
        if (canDamage)
        {
            StartCoroutine(MissileExplosion(collision));
            canDamage = false;
        }

    }

    IEnumerator MissileExplosion(Collider2D coll  )
    {
        if (coll.CompareTag("PlayerBody"))
        {
            animator.Play("PlayerExplosion");
            GetComponent<DamageSender>().SendDamageToPlayer(damage, playerTransform);
        }
        if (coll.CompareTag("Floor") || coll.CompareTag("Wall"))
        {
            animator.Play("FloorExplosion");
        }
        yield return new WaitForSeconds(0.1f);
        rb.velocity = new Vector2(0, 0);
        rb.angularVelocity = 0;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        print("Missile Destroyed");
        if (bossTransform) bossTransform.GetComponent<MissilesAttackL2B>().MissileDestroyed();

    }

    #endregion
    //Animation Event
    void ExplosionTriggerEnabled_Missile()
    {
        transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
    }
    //Animation Event
    void ExplosionTriggerDisabled_Missile()
    {
        transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
    }
    //Animation Event
    void DestroyMissile_Missil()
    {
        Destroy(gameObject);
    }
}

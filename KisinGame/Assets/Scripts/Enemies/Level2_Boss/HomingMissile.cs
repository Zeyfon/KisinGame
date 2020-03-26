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
    public int damage = 20;                    [UnityEngine.Tooltip("Damage done to the player")]
    public Transform bossTransform = null;
    public Transform playerTransform = null;
    [SerializeField] AudioClip explosionSound;                

    Rigidbody2D rb;
    Animator animator;
    Coroutine coroutine;
    bool canDamage = true;
    Vector3 targetPosition;
    bool collided = false;

    //HomingMissile homingMissile;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        yield return MovingUpwards();
        yield return Rotation();
        targetPosition = playerTransform.position;
        GetComponent<Collider2D>().enabled = true;
        yield return MovingTowardsPlayer();
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
    }

    IEnumerator Rotation()
    {
        float timer = 0;
         while(timer <timeToStopRotate)
         {
            yield return new WaitForFixedUpdate();

            Vector2 direction = (Vector2)playerTransform.position - rb.position;
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * initialRotateSpeed;
            rb.velocity = transform.up * targetSpeed;
            timer += Time.fixedDeltaTime;
        }

    }

    IEnumerator MovingTowardsPlayer()
    {
        yield return null;
        while (!collided)
        {
            yield return new WaitForFixedUpdate();
            Vector2 direction = (Vector2)playerTransform.position - rb.position;
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * finalRotateSpeed;
            rb.velocity = transform.up * targetSpeed;
        }
        StopMovement();
    }
    #endregion

    #region DamageSender
    public void MissileTouchedPlayer()
    {
        collided = true;
        animator.Play("PlayerExplotion");
        GetComponent<AudioSource>().PlayOneShot(explosionSound);
    }

    public void MissileTouchedFloor()
    {
        collided = true;
        animator.Play("FloorExplotion");
        GetComponent<AudioSource>().PlayOneShot(explosionSound);
    }


    private void StopMovement()
    {
        rb.velocity = new Vector2(0, 0);
        rb.angularVelocity = 0;
        transform.rotation = new Quaternion(0, 0, 0, 0);
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

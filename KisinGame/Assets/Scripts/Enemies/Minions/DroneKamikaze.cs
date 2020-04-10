using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using Spine;
using Spine.Unity;


public class DroneKamikaze : MonoBehaviour
{
    #region Properties
    [Header("Tunning Variables")]
    [Range(0f, .1f)]
    [SerializeField] float xLerp = 0;
    [Range(0f, .1f)]
    [SerializeField] float yLerp = 0;
    [SerializeField] int damage = 15;
    [SerializeField] float yOffset = 0;
    [SerializeField] GameObject pixanDrops;
    [SerializeField] Transform sounds;
    [SerializeField] Transform explosionTrigger;

    Transform target;
    Coroutine coroutine;
    Animator animator;
    Rigidbody2D rb;
    Skeleton skeleton;

    bool canMove = false;
    bool playerInRange = false;
    float flip = 1;
    bool lookingRight = true;
    float relativePosition;
    float x;
    float y;
    bool dead = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (!target) Debug.LogWarning("Player is not found");
        animator = GetComponent<Animator>();
        GetSpineInfo();
    }

    // Function that will make the gameObject to move towards the player indefenitely
    IEnumerator FollowPlayer()
    {
        while (canMove)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.position.x, xLerp), Mathf.Lerp(transform.position.y, target.position.y + yOffset, yLerp), 0);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    //Functions that are activated by the triggering of the children with the player
    #region Child Triggers Connections
    // Function sent by the PlayerFinder child. This will activate the Coroutine to start moving towards the player
    public void StartFollowingPlayer()
    {

        playerInRange = true;
        canMove = true;
        coroutine = StartCoroutine(FollowPlayer());
    }

    //Function sent by the PlayerFinder2 child. Once the player is in range for detonation
    public void StartsDetonation()
    {
        StopCoroutine(coroutine);
        if (!dead) animator.Play("StartDetonation");

    }

    #endregion

    //Events coming from the animations
    #region Animation Events

     void Explosion_Sound()
    {
        sounds.GetComponent<PlayMakerFSM>().SendEvent("Explosion_Sound");
    }

    void ExplosionTrigger_Enable()
    {
        rb.WakeUp();
        Collider2D coll;
        coll = explosionTrigger.GetComponent<Collider2D>();
        coll.enabled = true;
    }
    void ExplosionTrigger_Disable()
    {
        Collider2D coll;
        coll = explosionTrigger.GetComponent<Collider2D>();
        coll.enabled = false;
    }

    //This methos is called via SendMessage from the Health_FMS in the same gameObject when the dead animation is expecting to be fired.
    void RunDeadAnimation()
    {
        dead = true;
        StopAllCoroutines();
        animator.Play("Dead");
    }

    //The last two methods are called by the explosion animation or the dead animation
    void GameObject_Destroy()
    {
        Destroy(transform.parent.gameObject);
    }

    void PixanDrops_Create()
    {
        Instantiate(pixanDrops, transform.position, Quaternion.identity, transform.parent.parent.GetChild(0));
    }
    #endregion

    //Information and functions only to work with the Spine Animations
    #region SpineInfo
    void GetSpineInfo()
    {
        SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
        Spine.AnimationState spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.skeleton;
        StartCoroutine(Flip());
    }

    //The Flip is controlled by this Coroutine. Started at Start()
    IEnumerator Flip()
    {
        while (true)
        {
            Flip2();
            yield return new WaitForEndOfFrame();
        }

    }

    void Flip2()
    {
        if (lookingRight)
        {
            relativePosition = target.position.x - transform.position.x;
            if (relativePosition < 0)
            {
                skeleton.ScaleX = -1;
                lookingRight = false;
            }
        }
        else if (!lookingRight)
        {
            relativePosition = transform.position.x - target.position.x;
            if (relativePosition < 0)
            {
                skeleton.ScaleX = 1;
                lookingRight = true;
            }
        }
    }
    #endregion
}

using System.Collections;
using UnityEngine;
using Spine;
using Spine.Unity;

public class DroneBomber : MonoBehaviour
{
    [Header("Tunning Variables")]
    [Range(0f, .1f)]
    [SerializeField] float xLerp = 0;
    [Range(0f, .1f)]
    [SerializeField] float yLerp = 0;
    [Tooltip("This damage will be passed to the bomb when created")]
    [SerializeField] int bombDamage = 15;
    [Tooltip("The target to reach above the player")]
    [SerializeField] float yOffset = 0;
    [SerializeField] GameObject bomb;
    [SerializeField] GameObject pixanDrops;
    [SerializeField] Transform sounds;

    Transform projectilePool;
    Transform target;
    Coroutine coroutine;
    Animator animator;
    Skeleton skeleton;

    bool canMove = false;
    bool playerInRange = false;
    float flip = 1;
    bool lookingRight = true;
    float relativePosition;

    // Start is called before the first frame update
    void Start()
    {
        projectilePool = transform.parent.parent.GetChild(1);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (!target) Debug.LogWarning("Player is not found");
        animator = GetComponent<Animator>();
        GetSpineInfo();
        StartCoroutine(Flip());
    }

    void GetSpineInfo()
    {
        SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
        Spine.AnimationState spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.skeleton;
    }

    //Method called from the child PlayerFinder to start following the Player
    public void StartFollowingPlayer()
    {
        playerInRange = true;
        canMove = true;
        coroutine = StartCoroutine(FollowPlayer());

    }
    



    private void Update()
    {
        if (playerInRange)
        {

            if (canMove)
            {

                if ((transform.position.x > (target.position.x - .3f)) && (transform.position.x < (target.position.x + .3f)))
                {
                    canMove = false;
                    StopCoroutine(coroutine);
                    coroutine = StartCoroutine(DeployBomb());
                }
            }
            else
            {
                if ((transform.position.x > (target.position.x + 1f)) || (transform.position.x < (target.position.x - 1f)))
                {
                    canMove = true;
                    StopCoroutine(coroutine);
                    coroutine = StartCoroutine(FollowPlayer());
                }
            }
        }
    }

    void RunDeadAnimation()
    {
        StopAllCoroutines();
        playerInRange = false;
        animator.Play("Dead");
    }

    //Animation Event
    void GameObject_Destroy()
    {
        Destroy(transform.parent.gameObject);
    }

    //Animation Event
    void PixanDrops_Create()
    {
        Instantiate(pixanDrops, transform.position, Quaternion.identity, transform.parent.parent.GetChild(0));
    }

    //Animation Event
    void Bomb_Drop()
    {
        sounds.GetComponent<PlayMakerFSM>().SendEvent("DropBombSound");
        GameObject bombClone = Instantiate(bomb, transform.position, Quaternion.identity, projectilePool);
        DroneBBomb droneBBomb = bombClone.GetComponent<DroneBBomb>();
        droneBBomb.SetDamage(bombDamage);
    }

    IEnumerator FollowPlayer()
    {

        while (canMove)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.position.x, xLerp), Mathf.Lerp(transform.position.y, target.position.y + yOffset, yLerp) , 0);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    IEnumerator DeployBomb()
    {

        while (!canMove)
        {
            yield return new WaitForSeconds(.25f);
            playerInRange = false;
            animator.Play("DropBomb");
            yield return new WaitForSeconds(1.3f);
            playerInRange = true;
        }
        yield return null;
    }

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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using Spine;
using Spine.Unity;

public class CrystalBall : MonoBehaviour
{
    [Header("Internal Values")]
    [SerializeField] float rotateSpeed = 5;
    [SerializeField] float disablingTimer = 2;
    [SerializeField] float speed = 5;
    [SerializeField] Transform soundsBoss = null;
    public int weakness;

    Transform player;
    PlayMakerFSM myHealthFSM;
    Rigidbody2D rb;

    Skeleton skeleton;
    bool settingFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerIdentifer>().transform;
        if (!myHealthFSM) myHealthFSM = GetComponent<PlayMakerFSM>();
        rb = GetComponent<Rigidbody2D>();
        GetSpineInfo();
        settingFinished = true;
        gameObject.SetActive(false);
    }

    void GetSpineInfo()
    {
        SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
        if (skeletonAnimation == null)
        {
            Debug.LogWarning("No Spine Assets are used in  " + gameObject.name);
            return;
        }
        Spine.AnimationState spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.skeleton;
        print(skeleton);
    }

    #region WeaknessUpdate
    private void OnEnable()
    {
        if (!settingFinished) return;
        transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = false;
        if(!myHealthFSM) myHealthFSM = GetComponent<PlayMakerFSM>();
        SetNewWeakness();
    }

    void SetNewWeakness()
    {
        //Yellow
        if (weakness == 1)
        {
            if(skeleton == null)
            {
                transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1);
                return;
            }
            skeleton.SetSkin("AMARILLA");
            print("Amarilla");
        }
        //Blue
        if (weakness == 2)
        {

            if (skeleton == null)
            {
                transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
                return;
            }
            skeleton.SetSkin("AZUL");
            print("Azul");

        }
        //Yellow
        if (weakness == 3)
        {
            if (skeleton == null)
            {
                transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                return;
            }
            skeleton.SetSkin("ROJA");
            print("Roja");

        }
        //Weakness Change in the Health FSM
        StartCoroutine(ChangeWeaknessEvent());

    }

    IEnumerator ChangeWeaknessEvent()
    {
        yield return new WaitForEndOfFrame();
        FsmEventData myfsmEventData = new FsmEventData();
        myfsmEventData.IntData = weakness;
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        myHealthFSM.Fsm.Event("WeaknessChange");
    }
    #endregion

    public void SetInvulnerabilityToBalls()
    {
        weakness = 0;
        StartCoroutine(ChangeWeaknessEvent());
        if(skeleton == null)
        {
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            return;
        }
        skeleton.SetSkin("NEUTRA");

    }
    //Called from CrystalBallAttack in parent. 
    //This is to initiate the movement to the player 
    //and to add the invulnerability from player attacks
    public void StartAttack()
    {
        LookAtPlayer();
        StartCoroutine(MoveTowardsPlayer());
        transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = true;
        soundsBoss.GetComponent<PlayMakerFSM>().SendEvent("CrystalBallStartMoving");
    }

    void LookAtPlayer()
    {
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position);
        //print("direction  " + direction);
        Vector2 lookUp = new Vector2(0, 1);

        float dotValue = (direction.x * lookUp.x) + (direction.y * lookUp.y);
        float magnitudes = ((Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y)) * (Mathf.Sqrt(lookUp.x + lookUp.x + lookUp.y * lookUp.y)));

        float angle = Mathf.Acos(dotValue / magnitudes) * Mathf.Rad2Deg;

        //This is to knwo always point the player no matter which side is him
        angle = AngleFix(angle);
        //print(angle);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    float AngleFix(float angle)
    {
        if (player.position.x > transform.position.x)
        {
            angle *= -1;
        }

        return angle;
    }

    IEnumerator MoveTowardsPlayer()
    {
        Vector2 direction;
        float rotateAmount;
        float timer = 0;
        GetComponent<Rigidbody2D>().WakeUp();
        while (timer <disablingTimer)
        {
            direction = (Vector2)player.position - rb.position;
            rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * speed;
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        gameObject.SetActive(false);
        transform.parent.parent.GetComponent<CrystalBallAttack>().FinishedBallAttack();
    }

    //Event Call from the HealthFSM in the same gameObject
    void CrystalBallDamaged()
    {
        GetComponentInParent<CrystalBallAttack>().ballCounter += 1;
        //print(gameObject.name + "  destroyed");
        gameObject.SetActive(false);

    }
}

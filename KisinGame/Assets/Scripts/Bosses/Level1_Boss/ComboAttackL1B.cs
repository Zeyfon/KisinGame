using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttackL1B : MonoBehaviour, IFlipValues
{
    #region Inspector
    [Header("Variables")]
    [SerializeField] Transform weapon;
    [Tooltip("Movement Speed")]
    [SerializeField] float maxSpeed = 5;
    public float airTimeAttack4 = 1;

    [Header("Attack Impulses")]
    [SerializeField] float impulse1 = 40;
    [SerializeField] float impulse2 = 40;
    [SerializeField] float impulse3 = 40;

    [Header("Attack Damages")]
    [SerializeField] int damage1 = 20;
    [SerializeField] int damage2 = 25;
    [SerializeField] int damage3 = 35;

    [Header("Camera Variables")]
    [UnityEngine.Tooltip("Shake force to the Camera")]
    [Range(0, 1)]
    [SerializeField] float cameraShake = 1;
    #endregion

    Transform playerTransform;
    StressReceiver stressReceiver;
    Rigidbody2D rb;
    AttackTrigger attackTrigger;
    BoxCollider2D attackCollider;

    private void Start()
    {
        attackCollider = weapon.GetComponent<BoxCollider2D>();
        attackTrigger = weapon.GetComponent<AttackTrigger>();
        rb = GetComponent<Rigidbody2D>();
        //stressReceiver = Camera.main.GetComponent<StressReceiver>();
        GetComponent<BossesSupActions>().SetThisMonobehavior(this);
    }

    //Player Transform set from the Level#Boss Script
    public void GetPlayerTransform(Transform target)
    {
        playerTransform = target;
        //("Player Transform passed  " + playerTransform);
    }

    public float DistanceFromPlayer()
    {
        GetComponent<BossesSupActions>().UpdateCurrentTargetTransform(playerTransform);
        GetComponent<BossesSupActions>().FlipToTarget_SupActions();
        float checker = playerTransform.position.x - transform.position.x;
        checker = Mathf.Abs(checker);
        return checker;
    }

    public float MoveTowardsPlayer()
    {
        rb.velocity = new Vector2(maxSpeed, 0);
        float distance = Mathf.Abs(playerTransform.position.x - transform.position.x);
        return distance;
    }

    //Animation Event
    void AttackTrigger_Enable(int attackType)
    {
        switch (attackType)
        {
            case 1:
                attackTrigger.damage = damage1;
                attackCollider.offset = new Vector2(0.35f, 0);
                attackCollider.size = new Vector2(3, 2);
                attackCollider.enabled = true;
                GetComponent<SoundsConnection>().SendSoundEventToFSM("CloseAttack1");
                break;
            case 2:
                attackTrigger.damage = damage2;
                attackCollider.offset = new Vector2(.6f, 0);
                attackCollider.size = new Vector2(3.3f, 2);
                attackCollider.enabled = true;
                GetComponent<SoundsConnection>().SendSoundEventToFSM("CloseAttack2");
                break;
            case 3:
                attackTrigger.damage = damage3;
                attackCollider.offset = new Vector2(.5f, .2f);
                attackCollider.size = new Vector2(3, 2.5f);
                attackCollider.enabled = true;
                GetComponent<SoundsConnection>().SendSoundEventToFSM("CloseAttack3");
                StartCoroutine(CameraShake());
                break;
            default:
                break;
        }
    }
    //Animation Event
    void AttackTrigger_Disabled()
    {
        attackCollider.enabled = false;
    }
    //Animation Event
    void Impulse_Add(int impulseType)
    {
        print("Impulse Added");
        switch (impulseType)
        {
            case 1:
                AddImpulse(impulseType);
                break;
            case 2:
                AddImpulse(impulseType);
                break;
            case 3:
                AddImpulse(impulseType);
                break;
            default:
                break;
        }
    }
    //Animation Event
    void UpdateCurrentTargetTransform_ComboAttack()
    {
        GetComponent<BossesSupActions>().UpdateCurrentTargetTransform(playerTransform);
    }

    void AddImpulse(int impulseType)
    {
        switch (impulseType)
        {
            case 1:
                rb.AddForce(new Vector2(impulse1, 0), ForceMode2D.Impulse);
                break;
            case 2:
                rb.AddForce(new Vector2(impulse2 * 1.1f, 0), ForceMode2D.Impulse);
                break;
            case 3:
                rb.AddForce(new Vector2(impulse3 * .9f, 0), ForceMode2D.Impulse);
                break;
            default:
                break;
        }
    }

    IEnumerator CameraShake()
    {
        yield return new WaitForSeconds(0.1f);
        stressReceiver.InduceStress(cameraShake);
    }

    public void FlipValues()
    {
        maxSpeed *= -1;
        impulse1 *= -1;
        impulse2 *= -1;
        impulse3 *= -1;
    }
}

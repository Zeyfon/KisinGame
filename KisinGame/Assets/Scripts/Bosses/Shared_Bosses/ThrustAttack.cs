using UnityEngine;

public class ThrustAttack : MonoBehaviour
{
    [Header(" Internal Variables")]
    [SerializeField] int damage = 30;
    [SerializeField] float attackTime = 0.5f;
    [SerializeField] float initialJumpTime = 1f;


    [Header("Objects")]
    [SerializeField] BoxCollider2D attackCollider;
    [UnityEngine.Tooltip("Set at runtinme")]
    [SerializeField] Transform rightSpot;
    [UnityEngine.Tooltip("Set at runtinme")]
    [SerializeField] Transform leftSpot;

    [Header("Trigger Values")]
    [SerializeField] Vector2 offset;
    [SerializeField] Vector2 size;


    Transform playerTransform;
    Vector3 landingSpot;
    private void Start()
    {
        GetComponent<BossesSupActions>().rightSpot = rightSpot;
        GetComponent<BossesSupActions>().leftSpot = leftSpot;
    }
    //Player Transform set from the Level#Boss Script
    public void GetPlayerTransform(Transform target)
    {
        playerTransform = target;
    }

    #region FlipToTarget

    //Animation Event
    void ThrustAttack_FlipToInitialPosition()
    {
        ThrustAttackFlip(playerTransform.position);
    }

    //Animation Event
    void ThrustAttack_NextFlips()
    {
        ThrustAttackFlip(transform.position);
    }

    void ThrustAttackFlip(Vector3 targetPosition)
    {
        landingSpot = GetComponent<BossesSupActions>().GetFartestSideFromTarget(targetPosition);
        GetComponent<BossesSupActions>().FlipToTarget_SupActions();
    }


    #endregion

    #region Thrust Attack
    //Animation Event
    void ThrustAttack_InitialPositionJump()
    {
        GetComponent<SoundsConnection>().SendSoundEventToFSM("Jump");
        ThrustAttack_Impulse(initialJumpTime);
    }
    //Animation Event. Produce Sound
    void ThrustAttack_AttackThrust()
    {
        GetComponent<SoundsConnection>().SendSoundEventToFSM("ThrustAttack");
        ThrustAttack_Impulse(attackTime);

    }

    void ThrustAttack_Impulse(float airTime)
    {
        GetComponent<BossesSupActions>().DoParabolicJump(landingSpot, airTime, false);
    }
    #endregion

    public void AdjustAttackTrigger()
    {
        attackCollider.GetComponent<AttackTrigger>().damage = damage;
        attackCollider.offset = offset;
        attackCollider.size = size;
    }
    void ThrustAttackTrigger_Enabled()
    {
        attackCollider.enabled = true;
    }
    void ThrustAttackTrigger_Disabled()
    {
        attackCollider.enabled = false;
    }

}

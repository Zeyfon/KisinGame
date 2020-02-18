using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossesSupActions : MonoBehaviour
{
    [Header("Jump To Positions")]
    public Transform rightSpot = null;
    public Transform leftSpot = null;

    Transform playerTransform;
    Transform flipToTarget;
    Rigidbody2D rb;
    bool lookingRight = true;
    IFlipValues currentMonobehaviour;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetThisMonobehavior(IFlipValues action)
    {
        currentMonobehaviour = action;
        //print("Boss set for Interface use");
    }

    //Player Transform set from the Level3Boss Script
    public void GetPlayerTransform(Transform target)
    {
        playerTransform = target;
    }


    //Give the position of the target spot and update the current transform to flip to it
    public Vector3 GetFartestSideFromTarget(Vector3 targetPosition)
    {
        if (Vector3.Distance(rightSpot.position, targetPosition) >
        Vector3.Distance(leftSpot.position, targetPosition))
        {
            flipToTarget = rightSpot;
            return rightSpot.position;
        }
        else
        {
            flipToTarget = leftSpot;
            return leftSpot.position;
        }

    }

    #region Parabolic Jump Stuff
    public void DoParabolicJump(Vector3 targetPosition, float time, bool adjust)
    {
        rb.drag = 0;
        float adjuster = 1;
        //print(targetPosition);
        Vector3 jumpVector = CalculateJumpSpeed(targetPosition, time);
        if (adjust)
        {
            print("Attack was Adjusted");
            if (jumpVector.x < 0)
            {
                jumpVector = new Vector3(jumpVector.x + adjuster, jumpVector.y, 0);
            }
            else
            {
                jumpVector = new Vector3(jumpVector.x - adjuster, jumpVector.y, 0);
            }

        }
        //print("Jump Vector "+jumpVector);
        rb.velocity = jumpVector;
        StartCoroutine(CheckForGround());
        return;
    }

    private Vector3 CalculateJumpSpeed(Vector3 targetPosition, float time)
    {
        targetPosition = new Vector3(targetPosition.x, targetPosition.y , targetPosition.z);
        //print("Calculating Speed");
        Vector3 toTarget = targetPosition - transform.position;
        //print("Target Position " + targetPosition + "My Position  " + transform.position);
        //print("ToTarget Vector3  "+  toTarget  + "time   " + time);

        float y = toTarget.y;
        float x = toTarget.x;
        float t = time;
        //print("Gravity Scale " + rb.gravityScale);
        float v0y = y / t + 0.5f * Physics2D.gravity.magnitude * (rb.gravityScale) * t;
        float v0x = x / t;

        Vector3 result = toTarget.normalized;

        result.x = v0x;
        result.y = v0y;
        result.z = 0;
        //print("Calculated speed  " + result);
        return result;
    }

    IEnumerator CheckForGround()
    {
        bool isGrounded = false;
        yield return new WaitForSeconds(0.3f);
        while (!isGrounded)
        {
            isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.25f),
                                                new Vector2(transform.position.x + 0.5f, transform.position.y), LayerMask.GetMask("Floor"));
            yield return null;
        }
        rb.velocity = new Vector2(0, 0);
        rb.drag = 1;
        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, .5f, 1, 0.8f);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y-.125f),
                        new Vector2(GetComponent<BoxCollider2D>().size.x, 0.25f));
    }

    #endregion


    #region Flip Stuff
    public void UpdateCurrentTargetTransform(Transform target)
    {
        flipToTarget = target;
    }

    public void FlipToTarget_SupActions()
    {
        FlipCheck(flipToTarget);
    }
    //Animation Event
    void FlipTowardsPlayer()
    {
        FlipCheck(playerTransform);
    }

    void FlipCheck(Transform target)
    {
        float checker = target.position.x - transform.position.x;
        if (checker >= 0 && !lookingRight)
        {
            Flip();
        }
        else if (checker < 0 && lookingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        if (lookingRight)
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (!lookingRight)
        {
            transform.localScale = new Vector3(1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        lookingRight = !lookingRight;
        currentMonobehaviour.FlipValues();
    }
    #endregion
}
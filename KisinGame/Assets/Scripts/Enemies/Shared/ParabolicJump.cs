using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicJump : MonoBehaviour
{
    public void PerformParabolicJump(Rigidbody2D rb,Vector3 targetPosition, float time, float xDistanceToAdjust)
    {
        rb.drag = 0;
        //print(targetPosition);
        Vector3 jumpVector = CalculateJumpSpeed(rb, targetPosition, time);
        print("Attack was Adjusted");
        if (jumpVector.x < 0)
        {
            jumpVector = new Vector3(jumpVector.x + xDistanceToAdjust, jumpVector.y, 0);
        }
        else
        {
            jumpVector = new Vector3(jumpVector.x - xDistanceToAdjust, jumpVector.y, 0);
        }
        //print("Jump Vector "+jumpVector);
        rb.velocity = jumpVector;
        return;
    }

     Vector3 CalculateJumpSpeed(Rigidbody2D rb, Vector3 targetPosition, float time)
    {
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
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
}

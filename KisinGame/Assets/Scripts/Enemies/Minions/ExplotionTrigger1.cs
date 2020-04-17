using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionTrigger1 : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
        if (collision.transform.parent.GetComponent<PlayerIdentifer>())
        {
            transform.parent.GetComponent<DamageSender>().SendDamageToPlayer(transform.parent.GetComponent<DroneBBomb>().SendDamage(), transform.parent.GetComponent<DroneBBomb>().SendTransform());
        }
    }
}

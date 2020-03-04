using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.transform.GetComponent<PlayerIdentifer>())
        {
            GetComponent<Collider2D>().enabled = false;
            transform.parent.GetComponent<DamageSender>().SendDamageToPlayer(damage, collision.transform.parent.transform);
        }
    }
}

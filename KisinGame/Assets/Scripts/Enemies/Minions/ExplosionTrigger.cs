using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            transform.parent.gameObject.SendMessage("SendDamageToPlayer"); // calls the function in all the monobehaviors in the parent
            Collider2D coll = GetComponent<Collider2D>();
            coll.enabled = false;
        }
    }
}

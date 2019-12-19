using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileExplosion : MonoBehaviour
{
    HomingMissile homingMissile;
    Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        homingMissile = transform.parent.GetComponent<HomingMissile>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            //homingMissile.DamagePlayer();
            coll.enabled = false;

        }
    }

    void Collider_TurnOn()
    {
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        coll.enabled = true;
        yield return new WaitForSeconds(.35f);
        coll.enabled = false;
    }
}

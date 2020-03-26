using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBodyTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HomingMissile homingMissile = transform.parent.GetComponent<HomingMissile>();

        GetComponent<Collider2D>().enabled = false;
        if (collision.transform.parent.GetComponent<PlayerIdentifer>())
        {
            homingMissile.MissileTouchedPlayer();
            transform.parent.GetComponent<DamageSender>().SendDamageToPlayer(homingMissile.damage, collision.transform.parent.transform);
        }
        else
        {
            homingMissile.MissileTouchedFloor();

        }

    }
}

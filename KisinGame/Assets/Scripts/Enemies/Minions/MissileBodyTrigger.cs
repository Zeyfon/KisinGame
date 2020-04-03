using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBodyTrigger : MonoBehaviour
{
    bool positionReached = false;
    HomingMissile homingMissile;
    Transform playerTransform;

    IEnumerator Start()
    {
        homingMissile = transform.parent.GetComponent<HomingMissile>();
        playerTransform = homingMissile.SetPlayerPosition();
        yield return new WaitForSeconds(2);
        StartCoroutine(CheckPlayerRelativePosition());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.parent.GetComponent<PlayerIdentifer>())
        {
            print("Found Player");
            homingMissile.MissileCollisioned();
            GetComponent<Collider2D>().enabled = false;
            return;
        }
        if (positionReached)
        {
            print("Found Floor");
            homingMissile.MissileCollisioned();
            GetComponent<Collider2D>().enabled = false;
            return;
        }

    }
    IEnumerator CheckPlayerRelativePosition()
    {
        while (!positionReached)
        {
            if ((transform.position.x - playerTransform.position.x) > 0.5f || (transform.position.x - playerTransform.position.x) < 0.5f)
            {
                positionReached = true;
                print("Reached Player");
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}

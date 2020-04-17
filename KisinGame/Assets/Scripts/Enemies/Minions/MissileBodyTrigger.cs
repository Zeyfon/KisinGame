using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBodyTrigger : MonoBehaviour
{
    bool positionReached = false;
    HomingMissile homingMissile;

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent == null) return;

        if (collision.transform.parent.GetComponent<PlayerIdentifer>()!= null)
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

    public void StartLookingForPlayer(Transform playerTransform)
    {   homingMissile = transform.parent.GetComponent<HomingMissile>();
        StartCoroutine(CheckPlayerRelativePosition(playerTransform));
    }

    IEnumerator CheckPlayerRelativePosition(Transform playerTransform)
    {
        while (!positionReached)
        {
            if (Mathf.Abs(playerTransform.position.y-transform.position.y)<0.5f)
            {
                positionReached = true;
                print("Reached Player");
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}

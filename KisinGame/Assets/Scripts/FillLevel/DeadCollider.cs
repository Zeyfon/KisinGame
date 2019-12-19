using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCollider : MonoBehaviour
{
    [SerializeField] int damage = 20;

    DeadColliderManager deadManager;

    // Start is called before the first frame update
    void Start()
    {
        deadManager = transform.parent.GetComponent<DeadColliderManager>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerIdentifer>())
        {
            deadManager.ReturnToCheckpoint(collision.gameObject.transform,damage);
        }
        else
        {
            Debug.LogWarning("Something besides the player is colliding with me  " + gameObject);
        }
    }
}

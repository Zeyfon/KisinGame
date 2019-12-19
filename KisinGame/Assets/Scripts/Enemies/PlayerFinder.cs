using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent.gameObject.SendMessage("StartFollowingPlayer");
            BoxCollider2D col2d = GetComponent<BoxCollider2D>();
            col2d.enabled = false;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent.gameObject.SendMessage("StartsDetonation");
            Collider2D col2d = GetComponent<Collider2D>();
            col2d.enabled = false;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    DeadColliderManager deadManager;
    // Start is called before the first frame update
    void Start()
    {
        deadManager = transform.parent.GetComponent<DeadColliderManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerIdentifer>())
        {
            deadManager.LoadNewCheckpoint(transform);
        }
        else
        {
            Debug.LogWarning("Something besides the player is colliding   " + collision.gameObject);
        }
    }
}

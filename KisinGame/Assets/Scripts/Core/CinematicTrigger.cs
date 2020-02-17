using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicTrigger : MonoBehaviour
{
    bool hasBeenActivated = false;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (hasBeenActivated || !other.CompareTag("Player")) return;
        GetComponent<PlayableDirector>().Play();
        hasBeenActivated = true;
    }
}

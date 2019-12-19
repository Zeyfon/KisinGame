using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] AudioClip breakableClip;
    [SerializeField] float volume=0.35f;
    [SerializeField] Sprite secondSprite;
    [Tooltip("Time to start the Animation")]
    [SerializeField] float timeToChange = 0.25f;
    [SerializeField] GameObject pixanShard = null;

     
    //Function received from the Health_FSM from the same gameObject this script is
    public void DamageReceived()
    {
        StartCoroutine(BreakingAction());
    }

    IEnumerator BreakingAction()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(breakableClip, volume);
        yield return new WaitForSeconds(timeToChange);
        if (pixanShard)
        {
            Instantiate(pixanShard, transform.position, Quaternion.identity, transform.parent.transform);
        }
        spriteRenderer.sprite = secondSprite;
    }
}

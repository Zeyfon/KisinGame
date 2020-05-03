using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRainSlot : MonoBehaviour
{
    [Tooltip("Time once reaching the Floor to Disable itself")]
    [SerializeField] float timeToDisable;

    [Header("Movement Limits")]
    [SerializeField] Transform initialTransform;
    [SerializeField] Transform finalTransform;

    [Header("Timing Values")]
    [Range(0,0.2f)]
    [SerializeField] float upwardLerp;
    [Range(0, 0.2f)]
    [SerializeField] float downwardLerp;
    [SerializeField] float timeBetweenUpDown = 1;

    [Header("Audio Variables")]
    [SerializeField] AudioClip slideDownSound;
    // Start is called before the first frame update

    public void StartAttack(CrystalRainAttack crystalRainAttack)
    {
        StartCoroutine(MoveRainSlot(crystalRainAttack));
    }

    public void SetInitialPosition()
    {
        transform.position = new Vector3(transform.position.x, initialTransform.position.y, 0);
    }



    IEnumerator MoveRainSlot(CrystalRainAttack crystalRainAttack)
    {
        float timer = 0;
        Vector3 initialPosition = this.initialTransform.position;
        Vector3 finalPosition = new Vector3(transform.position.x, transform.position.y+1f,0);
        while (transform.position.y <= (finalPosition.y - .05f))
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, finalPosition, upwardLerp);
            //print("Moving Upwards");
            yield return null;
        }
        yield return new WaitForSeconds(timeBetweenUpDown);
        print("falling");
        GetComponent<AudioSource>().PlayOneShot(slideDownSound, 1);
        initialPosition = transform.position;
        finalPosition = new Vector3(transform.position.x, finalTransform.position.y, 0);
        while (transform.position.y >= (finalPosition.y + .05f))
        {
            transform.position = Vector3.Lerp(transform.position, finalPosition, downwardLerp);
            //print("Moving Downwards");
            yield return null;
        }
        //print("Reached Upper Limit");
        yield return new WaitForSeconds(timeToDisable);

        //print("DisableMyself");
        RainSlotActionEnded(crystalRainAttack);
    }

    private void RainSlotActionEnded(CrystalRainAttack crystalRainAttack)
    {
        gameObject.SetActive(false);
        crystalRainAttack.disabledRainSlotQuantity++;
    }
}

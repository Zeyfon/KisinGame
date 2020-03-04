using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRainSlot : MonoBehaviour
{
    [Tooltip("Time once reaching the Floor to Disable itself")]
    [SerializeField] float timeToDisable;

    [Header("Movement Limits")]
    [SerializeField] Transform upperLimit;
    [SerializeField] Transform lowerLimit;

    [Header("Velocity Lerp Values")]
    [Range(0,0.2f)]
    [SerializeField] float upwardLerp;
    [Range(0, 0.2f)]
    [SerializeField] float downwardLerp;

    [Header("Audio Variables")]
    [SerializeField] float timeBetweenUpDown = 1;


    [SerializeField] AudioClip slideDownSound;
    // Start is called before the first frame update

    public void StartAttack(CrystalRainAttack crystalRainAttack)
    {
        StartCoroutine(MoveRainSlot(crystalRainAttack));
    }

    IEnumerator MoveRainSlot(CrystalRainAttack crystalRainAttack)
    {
        Vector3 upperLimitPosition = upperLimit.position;
        Vector3 lowerLimitPosition = lowerLimit.position;
        while (transform.position.y <= (upperLimitPosition.y - .05f))
        {
            transform.position = Vector3.Lerp(transform.position, upperLimitPosition, upwardLerp);
            //print("Moving Upwards");
            yield return null;
        }
        //print("Moved Upwards");
        yield return new WaitForSeconds(timeBetweenUpDown);

        GetComponent<AudioSource>().PlayOneShot(slideDownSound, 1);
        while (transform.position.y >= (lowerLimitPosition.y + .05f))
        {
            transform.position = Vector3.Lerp(transform.position, lowerLimitPosition, downwardLerp);
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

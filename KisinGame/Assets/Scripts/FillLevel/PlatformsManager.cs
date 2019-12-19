using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsManager : MonoBehaviour
{
    [SerializeField] float timerToStart = 3;

    void Start()
    {
        StartCoroutine(StartAllPlatforms());
    }

    IEnumerator StartAllPlatforms()
    {
        MovingPlatforms[] platforms;
        platforms = transform.GetComponentsInChildren<MovingPlatforms>();
        yield return new WaitForSeconds(timerToStart);
        foreach(MovingPlatforms movingPlatform in platforms)
        {
            movingPlatform.StartMovement();
        }
        yield return null;
    }
}

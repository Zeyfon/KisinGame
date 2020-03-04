using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHazard_Mechanim : MonoBehaviour
{
    [Header("Tunning Variables")]
    [Tooltip("Prefab of laser")] [SerializeField] GameObject laserShot;
    [Tooltip("Local direction of laser")] [SerializeField] Vector3 laserDirection;
    [Tooltip("Speed of laser")] [SerializeField] float laserSpeed = 1f;
    [Tooltip("Time between laser respawns")] [SerializeField] float respawnTime = 3f;
    [Tooltip("Damage of laser")] [SerializeField] int laserDamage = 20;


    Coroutine coroutine;
    GameObject laserShotClone;
    Transform laserSpawner;

    void Start()
    {
        laserSpawner = transform.GetChild(0);
        float x = Vector3.Angle(new Vector3(0, 0, 0), laserDirection);
        coroutine = StartCoroutine(LoopShotInstantiation());
 
    }

    IEnumerator LoopShotInstantiation()
    {
        float angle;
        LaserShot laserShot2;
        angle = Mathf.Atan2(laserDirection.y, laserDirection.x) * 180 / Mathf.PI;
        laserDirection = transform.rotation * laserDirection;
        laserDirection = Vector3.Normalize(laserDirection);  
        while (true)
        {
            laserShotClone = Instantiate(laserShot, laserSpawner.position, transform.rotation, laserSpawner);
            laserShotClone.transform.rotation = transform.rotation*Quaternion.Euler(0,0,angle);
            laserShot2 = laserShotClone.GetComponent<LaserShot>();
            laserShot2.LaserFlies2(laserDirection, laserDamage, laserSpeed);
            yield return new WaitForSeconds(respawnTime);
        }
    }
}

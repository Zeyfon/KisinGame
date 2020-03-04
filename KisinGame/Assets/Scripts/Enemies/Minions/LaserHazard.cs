using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHazard : MonoBehaviour
{
    [Header("Tunning Variables")]
    [Tooltip("Prefab of laser")] [SerializeField] GameObject laserShot;
    [Tooltip("Local direction of laser")] [SerializeField] Vector3 laserDirection;
    [Tooltip("Speed of laser")] [SerializeField] float laserSpeed = 1f;
    [Tooltip("Time between laser respawns")] [SerializeField] float idleTime = 3f;
    [Tooltip("Damage of laser")] [SerializeField] int laserDamage = 20;
    [Tooltip("Time between laser respawns")] [SerializeField] float startTime = 3f;

    GameObject laserShotClone;
    Transform laserSpawner;
    Animator animator;
    HazardBatch hazardBatch;
    float angle;

    void Start()
    {
        hazardBatch = transform.parent.GetComponent<HazardBatch>();
        animator = GetComponent<Animator>();
        laserSpawner = transform.GetChild(0);
        float x = Vector3.Angle(new Vector3(0, 0, 0), laserDirection);
        StartCoroutine(GetInformation());
    }

    public void StartActions()
    {
        StartCoroutine(StartTime());
    }

    IEnumerator StartTime()
    {
        yield return new WaitForSeconds(startTime);
        StartCoroutine(LoopShotInstantiation());
    }

    IEnumerator GetInformation()
    {
        yield return new WaitForSeconds(1);
        angle = Mathf.Atan2(laserDirection.y, laserDirection.x) * 180 / Mathf.PI;
        laserDirection = transform.rotation * laserDirection;
        laserDirection = Vector3.Normalize(laserDirection);
    }

    IEnumerator LoopShotInstantiation()
    {
        yield return new WaitForSeconds(idleTime);
        animator.Play("PrepareAttack");
    }
    //Animation Event
    void LaserShot_Create()
    {
        LaserShot laserShot2;
        laserShotClone = Instantiate(laserShot, laserSpawner.position, transform.rotation, laserSpawner);
        laserShotClone.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, angle);
        laserShot2 = laserShotClone.GetComponent<LaserShot>();
        laserShot2.LaserFlies2(laserDirection, laserDamage, laserSpeed);
        StartCoroutine(LoopShotInstantiation());
        hazardBatch.StartLaserTSound();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJMissileSpawner : MonoBehaviour
{
    [SerializeField] GameObject missile;

    void CreateMissile(Transform playerTransform)
    {
        GameObject missileClone = Instantiate(missile, transform.position, Quaternion.identity);
        missileClone.transform.SetParent(transform);
        HomingMissile homingMissile = missileClone.GetComponent<HomingMissile>();
        homingMissile.playerTransform = playerTransform;
    }
}

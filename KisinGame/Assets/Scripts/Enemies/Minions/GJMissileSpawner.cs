using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJMissileSpawner : MonoBehaviour
{
    [SerializeField] GameObject missile;
    Transform projectilePool;

    private void Start()
    {
        projectilePool =transform.parent.parent.parent.GetChild(1);
    }

    void CreateMissile(Transform playerTransform)
    {
        GameObject missileClone = Instantiate(missile, transform.position, Quaternion.identity);
        missileClone.transform.SetParent(projectilePool);
        HomingMissile homingMissile = missileClone.GetComponent<HomingMissile>();
        homingMissile.playerTransform = playerTransform;
    }
}

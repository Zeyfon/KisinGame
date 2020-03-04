using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawners : MonoBehaviour
{
    public Transform currentFruitBomb;

    static int explosionCount;

    public void CreateBomb()
    {
        Instantiate(currentFruitBomb, transform.position, Quaternion.identity, transform);
    }

    //count the explosions by the bombFruits created
    void ExplosionCount()
    {
        explosionCount++;
        if (explosionCount < 5) return;
        transform.parent.GetComponent<BombBarrage>().AttackEnded();
        explosionCount = 0;
    }

}

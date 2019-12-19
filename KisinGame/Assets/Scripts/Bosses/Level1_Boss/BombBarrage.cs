using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBarrage : MonoBehaviour
{
    [SerializeField] List<Transform> bombSpawners = new List<Transform>();
    [SerializeField] List<Transform> fruitBombs = new List<Transform>();
    [SerializeField] Transform neutralBomb;
    public Transform bossTransform;
    int spawnersQuantity;

    private void Start()
    {
        spawnersQuantity = bombSpawners.Count;
    }
    public void AttackStarts(int phase)
    {
        List<Transform> temporal = new List<Transform>(bombSpawners);
        Transform currentBomb;
        for(int i = 0; i < 4-phase; i++)
        {
            currentBomb = temporal[Random.Range(0, temporal.Count)];
            temporal.Remove(currentBomb);
            currentBomb.GetComponent<BombSpawners>().currentFruitBomb = fruitBombs[Random.Range(0, fruitBombs.Count)];
        }
        foreach(Transform bomb in temporal)
        {
            bomb.GetComponent<BombSpawners>().currentFruitBomb = neutralBomb;

        }
        foreach(Transform spawners in bombSpawners)
        {
            spawners.GetComponent<BombSpawners>().CreateBomb();
            print(spawners);
        }
    }

    public void AttackEnded()
    {
        bossTransform.GetComponent<FruitBombBarrageAttack>().AttackEnded();
    }
}
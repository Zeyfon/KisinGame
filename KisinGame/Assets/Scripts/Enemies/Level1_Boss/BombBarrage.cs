using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class BombBarrage : MonoBehaviour
{
    [SerializeField] List<Transform> bombSpawners = new List<Transform>();
    [SerializeField] List<GameObject> fruitBombs = new List<GameObject>();
    [SerializeField] GameObject neutralBomb;
    public Transform bossTransform;
    int spawnersQuantity;

    private void Start()
    {
        spawnersQuantity = bombSpawners.Count;
    }
    public void AttackStarts(int phase)
    {
        SetBombsToSpawn(phase);
        SpawnBombs();
    }

    private void SetBombsToSpawn(int phase)
    {
        List<Transform> temporalBombSpawnerList = new List<Transform>(bombSpawners);
        Transform temporalSpawner;
        for (int i = 0; i < 4 - phase; i++)
        {
            temporalSpawner = temporalBombSpawnerList[Random.Range(0, temporalBombSpawnerList.Count)];
            temporalBombSpawnerList.Remove(temporalSpawner);
            //currentBomb.GetComponent<BombSpawners>().currentFruitBomb = fruitBombs[Random.Range(0, fruitBombs.Count)];
            FsmEventData myfsmEventData = new FsmEventData();
            myfsmEventData.GameObjectData = fruitBombs[Random.Range(0, fruitBombs.Count)];
            HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
            temporalSpawner.GetComponent<PlayMakerFSM>().SendEvent("SetThisBomb");
        }
        foreach (Transform temporalSpawner2 in temporalBombSpawnerList)
        {
            //bomb.GetComponent<BombSpawners>().currentFruitBomb = neutralBomb;
            FsmEventData myfsmEventData = new FsmEventData();
            myfsmEventData.GameObjectData = neutralBomb;
            HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
            temporalSpawner2.GetComponent<PlayMakerFSM>().SendEvent("SetThisBomb");
        }
    }

    private void SpawnBombs()
    {
        foreach (Transform spawners in bombSpawners)
        {
            //spawners.GetComponent<BombSpawners>().CreateBomb();
            spawners.GetComponent<PlayMakerFSM>().SendEvent("SpawnBomb");
        }
        StartCoroutine(TimeTillAttackEnds());
    }

    IEnumerator TimeTillAttackEnds()
    {
        yield return new WaitForSeconds(4.5f);
        AttackEnded();
    }
    //Call from each SpawnerFSM
    public void AttackEnded()
    {
        print("BombExploded");
        bossTransform.GetComponent<FruitBombBarrageAttack>().AttackEnded();
    }
}
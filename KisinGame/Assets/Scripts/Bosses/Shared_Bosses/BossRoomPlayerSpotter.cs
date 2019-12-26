using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class BossRoomPlayerSpotter : MonoBehaviour
{

    [SerializeField] Transform leftDoor = null;
    [SerializeField] Transform rightDoor = null;
    [SerializeField] Transform bossUI = null;
    [SerializeField] float timeToActivateBoss = 1;

    bool initialized = false;
    BossStarter currentBoss;

    public void SetCurrentBoss(BossStarter boss)
    {
        currentBoss = boss;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (initialized) return;
        if (collision.gameObject.GetComponent<PlayerIdentifer>())
        {
            StartFight();
        }
    }

    public void StartFight()
    {
        ActivateWalls();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;
        StartCoroutine(ActivateBoss());
        initialized = true;
    }

    IEnumerator ActivateBoss()
    {
        FsmEventData myfsmEventData = new FsmEventData();
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        bossUI.GetComponent<PlayMakerFSM>().Fsm.Event("ActivateBossUI");
        yield return new WaitForSeconds(timeToActivateBoss);
        currentBoss.StartActions();
    }

    //Called from Health FSM. In Dead state
    void BossDead()
    {
        StartCoroutine(BossDeadC());
    }

    void ActivateWalls()
    {   if(leftDoor) leftDoor.gameObject.SetActive(true);
        if(rightDoor) rightDoor.gameObject.SetActive(true);
    }

    IEnumerator BossDeadC()
    {
        yield return new WaitForSeconds(2);
        if (leftDoor) leftDoor.gameObject.SetActive(false);
        if(rightDoor) rightDoor.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        FsmEventData myfsmEventData = new FsmEventData();
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        bossUI.GetComponent<PlayMakerFSM>().Fsm.Event("DisableBossUI");
    }
}

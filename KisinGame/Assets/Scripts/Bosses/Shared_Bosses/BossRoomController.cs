using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using PixelCrushers.DialogueSystem;

public class BossRoomController : MonoBehaviour
{
    enum Bosses {levelBoss, izelBoss,mitlaBoss}
    [SerializeField] string bossName = "  ";
    [SerializeField] Transform backtrackingDoor = null;
    [SerializeField] Transform otherDoor = null;
    [SerializeField] Transform bossUI = null;
    [SerializeField] float timeToActivateBoss = 1;
    [SerializeField] Transform dialogueList;

    bool initialized = false;
    BossStarter currentBoss;

    private void Start()
    {
        if (PlayerPrefs.GetInt(bossName) == 1)
        {
            BossDefeated();
            Destroy(FindObjectOfType<BossIdentifier>().gameObject);
        }
    }

    public void SetCurrentBoss(BossStarter boss)
    {
        currentBoss = boss;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (initialized) return;
        if (collision.gameObject.GetComponent<PlayerIdentifer>())
        {
            StartFight();
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.enabled = false;
            initialized = true;
        }
    }
    //Called from the Dialogue Event in the Dialogue System Trigger
    public void StartFight()
    {
        print(currentBoss + "  StartFight");
        ActivateDoors();
        ActivateUI();
        StartCoroutine(ActivateBoss());
    }
    public void StartFightDelayed(GameObject mitlanCover)
    {
        StartCoroutine(StartFightDelayedC(mitlanCover));
    }
    IEnumerator StartFightDelayedC(GameObject mitlanCover)
    {
        Destroy(mitlanCover);
        yield return new WaitForSeconds(1);
        StartFight();
    }

    void ActivateDoors()
    {
        if (backtrackingDoor) backtrackingDoor.gameObject.SetActive(true);
        if (otherDoor) otherDoor.gameObject.SetActive(true);
    }

    IEnumerator ActivateBoss()
    {
        yield return new WaitForSeconds(timeToActivateBoss);
        currentBoss.StartActions();
    }

    private void ActivateUI()
    {
        bossUI.GetComponent<PlayMakerFSM>().Fsm.Event("ActivateBossUI");
    }

    //Called from the Boss Script when Dies Events happens
    public void BossDead( int dialogueID)
    {
        print("Messave received Boss Died  " + dialogueID);
        StartCoroutine(BossDeadC(dialogueID));
    }

    IEnumerator BossDeadC(int dialogueID)
    {
        PlayerPrefs.SetInt(bossName, 1);
        BossDefeated();
        GameObject gameManager = GameObject.FindObjectOfType<SetActiveSceneAction>().gameObject;
        gameManager.GetComponent<PlayMakerFSM>().Fsm.Event("SaveGame");
        //if (bossType == (int)Bosses.levelBoss)
        //{
        //    print("LevelBoss defeated");

        //}

        yield return new WaitForSeconds(.5f);
        DisableUI();
        yield return new WaitForSeconds(.5f);

        //if (bossType == (int)Bosses.izelBoss)
        //{
        //    print("Izel Boss defeated");
        //}

        //if (bossType == (int)Bosses.mitlaBoss)
        //{
        //    print("Mitlan Boss defetead");
        //}
        if(dialogueID != 10)
        {
            RunDialogue(dialogueID);

        }
    }

    private void RunDialogue(int dialogueID)
    {
        Transform dialogue = dialogueList.GetComponent<DialoguesList>().GetDialogue(dialogueID);
        dialogue.GetComponent<DialogueSystemTrigger>().OnUse();
    }

    private void DisableUI()
    {
        bossUI.GetComponent<PlayMakerFSM>().Fsm.Event("DisableBossUI");
    }

    private void BossDefeated()
    {
        GetComponent<Collider2D>().enabled = false;
        backtrackingDoor.gameObject.SetActive(true);
        otherDoor.gameObject.SetActive(false);
    }

    public void GameFinished()
    {
        PlayMakerFSM gameManagerFSM = FindObjectOfType<SetActiveSceneAction>().GetComponent<PlayMakerFSM>();
        gameManagerFSM.Fsm.Event("GameHasFinished");
        DisableUI();
    }
}

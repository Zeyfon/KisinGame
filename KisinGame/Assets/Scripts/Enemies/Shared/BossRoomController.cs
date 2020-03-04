using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using PixelCrushers.DialogueSystem;
using System;

public class BossRoomController : MonoBehaviour
{
    enum Bosses {levelBoss, izelBoss,mitlaBoss}
    [SerializeField] string bossName = "  ";
    [SerializeField] Transform backtrackingDoor = null;
    [SerializeField] Transform otherDoor = null;
    [SerializeField] Transform bossUI = null;
    [SerializeField] float timeToActivateBoss = 1;
    [SerializeField] Transform dialogueList;

    [SerializeField] bool overrideSave =false;

    bool initialized = false;
    IBossStarter currentBoss;

    private void Start()
    {
        InitialDoorSetup();
        SetBossFightStartMethod();
    }

    private void SetBossFightStartMethod()
    {
        if (!transform.GetChild(4).GetComponent<DialoguesList>().WillBeDialogueBeforeBossFight())
        {
            return;
        }
        GetComponent<Collider2D>().enabled = false;
    }

    private void InitialDoorSetup()
    {
        otherDoor.gameObject.SetActive(false);
        backtrackingDoor.gameObject.SetActive(false);
    }

    public void SetCurrentBoss(IBossStarter boss)
    {
        currentBoss = boss;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (initialized) return;
        if (collision.gameObject.GetComponent<PlayerIdentifer>())
        {
            StartFight();
            Collider2D collider = GetComponent<BoxCollider2D>();
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
        currentBoss.IStartActions();
    }

    private void ActivateUI()
    {
        bossUI.GetComponent<PlayMakerFSM>().Fsm.Event("ActivateBossUI");
    }

    //Called from the Boss Script when Dies Events happens
    public void BossDead( int dialogueID)
    {
        StartCoroutine(BossDeadC(dialogueID));
    }

    IEnumerator BossDeadC(int dialogueID)
    {
        SetDoorsAfterBossDefeated();
        //GameObject gameManager = GameObject.FindObjectOfType<SetActiveSceneAction>().gameObject;

        yield return new WaitForSeconds(.5f);
        DisableUI();
        yield return new WaitForSeconds(.5f);

        AfterDefeatingBossDialogue(dialogueID);
    }

    private void AfterDefeatingBossDialogue(int dialogueID)
    {
        transform.GetChild(4).GetComponent<DialoguesList>().RunDialogue(dialogueID);
        print("Still before");
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

    private void SetDoorsAfterBossDefeated()
    {
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

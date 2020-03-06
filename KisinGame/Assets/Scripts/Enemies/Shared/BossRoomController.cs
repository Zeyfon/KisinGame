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
    [SerializeField] Transform backtrackingLimitDoor = null;
    [SerializeField] Transform continueDoor = null;
    [SerializeField] Transform bossUI = null;
    [SerializeField] float timeToActivateBoss = 1;
    [SerializeField] Transform dialogueList;

    [SerializeField] bool bossDefeated = false;

    [Header("Doors Sounds")]
    [SerializeField] AudioClip closeDoor;
    [SerializeField] float closeDoorsVolume = 1;
    [SerializeField] AudioClip openDoor;
    [SerializeField] float openDoorsVolume = 1;

    IBossStarter currentBoss;

    void Start()
    {
        if (!bossDefeated)
        {
            SetDoorsBeforeBossFight();
        }
        else
        {
            SetDoorsAfterBossDefeated();
        }
        if (WillBeADialogueBeforeTheFight())
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private bool WillBeADialogueBeforeTheFight()
    {
        if (dialogueList.GetComponent<DialoguesList>().dialogues[0] == null) return false;
        return true;
    }

    public void SetCurrentBoss(IBossStarter boss)
    {
        currentBoss = boss;
    }

    //Called from the Dialogue Event in the Dialogue System Trigger
    public void StartFight()
    {
        print(currentBoss + "  StartFight");
        CloseDoors();
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
        print("REaching 1");
        OpenContinueDoor();
        yield return new WaitForSeconds(.5f);
        print("REaching 2");
        DisableUI();
        yield return new WaitForSeconds(.5f);
        print("REaching 3");

        AfterDefeatingBossDialogue(dialogueID);
        print("Reached the dialogues");
    }

    private void AfterDefeatingBossDialogue(int dialogueID)
    {
        dialogueList.GetComponent<DialoguesList>().RunDialogue(dialogueID);
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

    public void GameFinished()
    {
        PlayMakerFSM gameManagerFSM = FindObjectOfType<SetActiveSceneAction>().GetComponent<PlayMakerFSM>();
        gameManagerFSM.Fsm.Event("GameHasFinished");
        DisableUI();
    }

    #region BossRoomDoors
    void SetDoorsBeforeBossFight()
    {
        continueDoor.GetComponent<BossRoomDoor>().OpenedState();
        backtrackingLimitDoor.GetComponent<BossRoomDoor>().OpenedState();
    }

    void CloseDoors()
    {
        backtrackingLimitDoor.GetComponent<BossRoomDoor>().CloseDoor();
        continueDoor.GetComponent<BossRoomDoor>().CloseDoor();
        GetComponent<AudioSource>().PlayOneShot(closeDoor, closeDoorsVolume);
    }

    void OpenContinueDoor()
    {
        continueDoor.GetComponent<BossRoomDoor>().OpenDoor();
        GetComponent<AudioSource>().PlayOneShot(openDoor, openDoorsVolume);
    }

    private void SetDoorsAfterBossDefeated()
    {
        backtrackingLimitDoor.GetComponent<BossRoomDoor>().ClosedState();
        continueDoor.GetComponent<BossRoomDoor>().OpenedState();
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerIdentifer>())
        {
            Collider2D collider = GetComponent<BoxCollider2D>();
            collider.enabled = false;
            StartFight();
        }
    }
}

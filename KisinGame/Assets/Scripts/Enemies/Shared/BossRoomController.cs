using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using PixelCrushers.DialogueSystem;
using System;
using UnityEngine.SceneManagement;

public class BossRoomController : MonoBehaviour
{
    enum Bosses {levelBoss, izelBoss,mitlaBoss}
    [SerializeField] string bossName = "  ";
    [SerializeField] Transform backtrackingLimitDoor = null;
    [SerializeField] Transform continueDoor = null;
    [SerializeField] Transform bossUI = null;
    [SerializeField] float timeToActivateBoss = 1;
    [SerializeField] Transform dialogueList;

    bool bossDefeated = false;

    [Header("Doors Sounds")]
    [SerializeField] AudioClip closeDoor;
    [SerializeField] float closeDoorsVolume = 1;
    [SerializeField] AudioClip openDoor;
    [SerializeField] float openDoorsVolume = 1;

    public bool currentBossIsDead = false;
    bool initialClosingDoor = false;

    IBossStarter currentBoss;


    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        if(ES3.FileExists()) bossDefeated = (bool)ES3.Load<object>(bossName, bossDefeated);

        if (!bossDefeated)
        {
            SetDoorsBeforeBossFight();
        }
        else
        {
            SetDoorsAfterBossDefeated();
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void SetCurrentBoss(IBossStarter boss)
    {
        currentBoss = boss;
        currentBossIsDead = false;
    }

    public void InitialAction()
    {
        if (CheckForInitialDialogue())
        {
            dialogueList.GetComponent<DialoguesList>().RunDialogue(0);
            return;
        }
        StartFight();
    }

    //Called from the Dialogue Event in the Dialogue System Trigger
    public void StartFight()
    {
        print(currentBoss + "  StartFight");

        ActivateUI();
        StartCoroutine(ActivateBoss());
        if (SceneManager.GetActiveScene().buildIndex == 31 && initialClosingDoor) return;
        CloseDoors();
        initialClosingDoor = true;
    }

    bool CheckForInitialDialogue()
    {
        if(dialogueList.GetComponent<DialoguesList>().dialogues[0]!= null)
        {
            return true;
        }
        return false;
    }

    public void AfterConversationEndedActions()
    {
        if (!currentBossIsDead)
        {
            StartFight();
        }
    }
    public void StartFightDelayed(GameObject mitlanCover)
    {
        Destroy(mitlanCover);
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
        currentBossIsDead = true;
    }

    IEnumerator BossDeadC(int dialogueID)
    {
        DisableUI();
        yield return new WaitForSeconds(1f);
        AfterDefeatingBossDialogue(dialogueID);
        if (SceneManager.GetActiveScene().buildIndex == 31) yield break;
        OpenContinueDoor();
    }

    private void AfterDefeatingBossDialogue(int dialogueID)
    {
        dialogueList.GetComponent<DialoguesList>().RunDialogue(dialogueID);
    }

    private void DisableUI()
    {
        print("Disabling UI");
        bossUI.GetComponent<PlayMakerFSM>().Fsm.Event("DisableBossUI");
    }

    public void GameFinished()
    {
        print("GameFinishes");
        PlayMakerFSM gameManagerFSM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayMakerFSM>();
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
            InitialAction();
        }
    }
}

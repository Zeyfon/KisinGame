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

    [SerializeField] Transform dialogue1;
    [SerializeField] Transform dialogue2;
    [SerializeField] Transform bossCamera;

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
        if (initialized) return;
        if (collision.gameObject.GetComponent<PlayerIdentifer>())
        {
            StartFight();
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.enabled = false;
            initialized = true;
        }
    }

    public void StartFight()
    {
        print(gameObject.name + "  StartFight");
        ActivateDoors();
        ActivateUI();
        StartCoroutine(ActivateBoss());
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
    public void BossDead(int i)
    {

        StartCoroutine(BossDeadC(i));
    }

    IEnumerator BossDeadC(int bossType)
    {
        print("Wants to start dialogue");
        DialogueManager.DisplaySettings.cameraSettings.sequencerCamera = bossCamera.GetComponent<Camera>();
        if (bossType == (int)Bosses.levelBoss)
        {
            print("LevelBoss defeated");
            PlayerPrefs.SetInt(bossName, 1);
            BossDefeated();
            GameObject gameManager = GameObject.FindObjectOfType<SetActiveSceneAction>().gameObject;
            gameManager.GetComponent<PlayMakerFSM>().Fsm.Event("BossDefeated");
            EnableDialogue1();
        }

        yield return new WaitForSeconds(.5f);
        DisableUI();
        yield return new WaitForSeconds(.5f);

        if (bossType == (int)Bosses.izelBoss)
        {
            print("Izel Boss defeated");
            //dialogue1.GetComponent<ConversationStarter>().StartConversation();
            EnableDialogue1();

        }

        if (bossType == (int)Bosses.mitlaBoss)
        {
            print("Mitlan Boss defetead");
            dialogue2.GetComponent<DialogueSystemTrigger>().OnUse();
        }
    }

    private void EnableDialogue1()
    {
        dialogue1.GetComponent<DialogueSystemTrigger>().OnUse();
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

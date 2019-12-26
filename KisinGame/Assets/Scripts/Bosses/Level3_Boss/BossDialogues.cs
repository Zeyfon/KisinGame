using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using HutongGames.PlayMaker;

public class BossDialogues : MonoBehaviour
{
    [SerializeField] bool isIzel = false;
    [SerializeField] Transform transitionToMitlan;
    [SerializeField] Transform currentDialogue;
    [SerializeField] Transform bossCamera;
    [SerializeField] int dialogueEntryNum = 5;
    [SerializeField] int conversationNum1 = 4;
    [SerializeField] int conversationNum2 = 5;
    [SerializeField] int conversationNum3 = 6;

    BossRoomPlayerSpotter playerSpotter;

    private void Start()
    {
        DialogueManager.DisplaySettings.cameraSettings.sequencerCamera = bossCamera.GetComponent<Camera>();
    }
    public void SetPlayerSpotter(BossRoomPlayerSpotter spotter)
    {
        playerSpotter = spotter;
    }

    public void AfterBossDiesActions()
    {
        DialogueManager.DisplaySettings.cameraSettings.sequencerCamera = bossCamera.GetComponent<Camera>();
        currentDialogue.GetComponent<ConversationStarter>().StartConversation(transform);
    }

    //Dialoogue System Event Call. OnConversationLineEnd()
    public void TransitionToMitlan()
    {
        DialogueEntry dialogueEntry = DialogueManager.CurrentConversationState.subtitle.dialogueEntry;
        int conversationID = dialogueEntry.conversationID; //<-- This is the conversation ID.
        if (conversationID != conversationNum2) return;
        int dialogueEntryID = dialogueEntry.id; //<-- This is the dialogue entry ID.
        if (dialogueEntryID != dialogueEntryNum) return;
        transitionToMitlan.transform.position = transform.position;
        transitionToMitlan.GetComponent<TransitionToMitlan>().StartMitlanPhase();
        print("Mitlan was started");
    }
    //Dialoogue System Event Call. OnConversationEnd()
    public void ConversationEnded()
    {
        DialogueEntry dialogueEntry = DialogueManager.CurrentConversationState.subtitle.dialogueEntry;
        int conversationID = dialogueEntry.conversationID; //<-- This is the conversation ID.
        if (isIzel)
        {
            if (conversationID == conversationNum1)
            {
                playerSpotter.StartFight();
                return;
            }
            else
            {
                print("IsIzel");
                transitionToMitlan.GetComponent<TransitionToMitlan>().ConversationEnded();
                Destroy(gameObject);
                return;
            }

        }
        else
        {
            if (conversationID != conversationNum3) return;
            print("isNotIzel");
            FsmEventData myfsmEventData = new FsmEventData();
            HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
            GetComponent<PlayMakerFSM>().Fsm.Event("GameHasFinished");
        }

    }
}

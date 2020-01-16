using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using PixelCrushers.DialogueSystem;

public class TransitionToMitlan : MonoBehaviour
{
    [SerializeField] GameObject mitlanBoss;
    [SerializeField] int dialogueEntryNum = 5;
    [SerializeField] int conversationNum1 = 4;
    [SerializeField] int conversationNum2 = 5;
    [SerializeField] int conversationNum3 = 6;

    public void StartMitlanPhase()
    {

        StartCoroutine(ChangeToMitlan());
    }

    IEnumerator ChangeToMitlan()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(1);
        mitlanBoss.transform.position = transform.position;
        mitlanBoss.SetActive(true);
    }

    public void ConversationEnded()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        FsmEventData myfsmEventData = new FsmEventData();
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        mitlanBoss.GetComponent<PlayMakerFSM>().Fsm.Event("ResetUI");
        mitlanBoss.GetComponent<Level3Boss>().StartActions();
        return;
    }

    public void TransitionToMitlan2()
    {
        print("Entered");
        Debug.Break();
        DialogueEntry dialogueEntry = DialogueManager.CurrentConversationState.subtitle.dialogueEntry;
        int conversationID = dialogueEntry.conversationID; //<-- This is the conversation ID.
        if (conversationID != conversationNum2) return;
        int dialogueEntryID = dialogueEntry.id; //<-- This is the dialogue entry ID.
        if (dialogueEntryID != dialogueEntryNum) return;
        //transitionToMitlan.transform.position = transform.position;
        //transitionToMitlan.GetComponent<TransitionToMitlan>().StartMitlanPhase();
        print("Mitlan was started");
    }


}

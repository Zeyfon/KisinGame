using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using HutongGames.PlayMaker;

public class DialogueActions : MonoBehaviour
{
    [SerializeField] GameObject mitlanCover;
    [SerializeField] GameObject izelBoss;
    [SerializeField] GameObject mitlanBoss;
    [SerializeField] int conversationNum = 5;
    [SerializeField] int dialogueEntryNum = 6;


    GameObject mitlanCoverClone;


    public void ConversationEnded()
    {
        StartCoroutine(ConversationEndedActions());
    }

    IEnumerator ConversationEndedActions()
    {
        print(gameObject.name + "  Conversation Ended");
        FindObjectOfType<BossRoomController>().StartFightDelayed(mitlanCoverClone);
        izelBoss.GetComponent<Level3Boss>().DestroyBoss();
        yield return null;
    }

    public void ActivatePhaseThree()
    {
        DialogueEntry dialogueEntry = DialogueManager.CurrentConversationState.subtitle.dialogueEntry;
        int conversationID = dialogueEntry.conversationID; //<-- This is the conversation ID.
        if (conversationID != conversationNum) return;
        int dialogueEntryID = dialogueEntry.id; //<-- This is the dialogue entry ID.
        if (dialogueEntryID != dialogueEntryNum) return;

        StartCoroutine(ChangeToMitlan());
        print("Mitlan was activated");
    }

    IEnumerator ChangeToMitlan()
    {
        mitlanCoverClone = Instantiate(mitlanCover, izelBoss.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        yield return new WaitForSeconds(1);
        mitlanBoss.transform.position = izelBoss.transform.position;
        mitlanBoss.SetActive(true);
    }
}

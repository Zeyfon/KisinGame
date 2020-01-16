using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using HutongGames.PlayMaker;

public class BossDialogues : MonoBehaviour
{
    [SerializeField] GameObject mitlanCover;
    [SerializeField] GameObject izelBoss;
    [SerializeField] GameObject mitlanBoss;
    [SerializeField] int dialogueEntryNum = 5;
    [SerializeField] int conversationNum1 = 4;
    [SerializeField] int conversationNum2 = 5;
    [SerializeField] int conversationNum3 = 6;

    GameObject mitlanCoverClone;


    public void ConversationEnded()
    {
        StartCoroutine(ConversationEndedActions());
    }

    IEnumerator ConversationEndedActions()
    {
        print(gameObject.name + "  Conversation Ended");
        izelBoss.GetComponent<Level3Boss>().DestroyBoss();
        yield return new WaitForSeconds(.5f);
        Destroy(mitlanCoverClone);
        FindObjectOfType<BossRoomController>().StartFight();
    }

    public void ActivatePhaseThree()
    {  

        DialogueEntry dialogueEntry = DialogueManager.CurrentConversationState.subtitle.dialogueEntry;

        int conversationID = dialogueEntry.conversationID; //<-- This is the conversation ID.
        if (conversationID != conversationNum2) return;
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

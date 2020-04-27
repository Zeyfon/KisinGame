using PixelCrushers.DialogueSystem;
using System.Collections;
using UnityEngine;

public class DialogueActions : MonoBehaviour
{
    [SerializeField] GameObject mitlanCover;
    //[SerializeField] GameObject izelBoss;
    //[SerializeField] GameObject mitlanBoss;
    [SerializeField] int conversationNum = 5;
    [SerializeField] int dialogueEntryNum = 6;

    BossRoomController bossRoomController;
    GameObject mitlanCoverClone;
    Transform targetTransform;

    public void ConversationStart()
    {
        //DialogueEntry dialogueEntry = DialogueManager.CurrentConversationState.subtitle.dialogueEntry;
        int conversationID = DialogueManager.LastConversationID;
        if (conversationID == 7)
        {
            targetTransform = GameObject.FindObjectOfType<DoubleJumpEvent>().transform;
        }
    }

    public void ConversationEnded()
    {
        print(gameObject.name + "  Conversation Ended");
        if (bossRoomController == null)
        {
            bossRoomController = FindObjectOfType<BossRoomController>();
        }
        //DialogueEntry dialogueEntry = DialogueManager.CurrentConversationState.subtitle.dialogueEntry;
        int conversationID = DialogueManager.CurrentConversationState.subtitle.dialogueEntry.conversationID; //<-- This is the conversation ID.
        if (conversationID == 4)
        {
            print("Starting Fight from Dialogue Actions in  " + gameObject.name);
            bossRoomController.StartFight();
        }
        if (conversationID == 5)
        {
            bossRoomController.StartFightDelayed(mitlanCoverClone);
        }
        if (conversationID == 6)
        {
            EndGame();
        }
        if (conversationID == 7)
        {
            targetTransform = GameObject.FindObjectOfType<DoubleJumpEvent>().transform;
            targetTransform.GetComponent<DoubleJumpEvent>().DoubleJumpSkillAcquiringEvent();

        }
    }

    public void OnConversationLineEnded()
    {
        print("ConversationLine  " + gameObject.name);
        DialogueEntry dialogueEntry = DialogueManager.CurrentConversationState.subtitle.dialogueEntry;
        int conversationID = dialogueEntry.conversationID; //<-- This is the conversation ID.
        if (conversationID != conversationNum) return;
        int dialogueEntryID = dialogueEntry.id; //<-- This is the dialogue entry ID.
        if (dialogueEntryID != dialogueEntryNum) return;
        print("Activating Phase3");
        StartCoroutine(ChangeToMitlan());
        print("Mitlan was activated");
    }

    IEnumerator ChangeToMitlan()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Izel").transform.GetChild(1).transform;

        mitlanCoverClone = Instantiate(mitlanCover, targetTransform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        //Debug.Break();
        if (transform.gameObject.CompareTag("Mitlan"))
        {
            transform.GetChild(1).transform.position = targetTransform.position;
            print(gameObject.name + " destroying Izel Boss");
            Destroy(targetTransform.parent.transform.gameObject);
            transform.GetChild(1).transform.gameObject.SetActive(true);
        }
    }

    void EndGame()
    {
        print("Ending Game");
        FindObjectOfType<BossRoomController>().GameFinished();
        Destroy(gameObject);
    }
}

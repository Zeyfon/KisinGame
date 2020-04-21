using PixelCrushers.DialogueSystem;
using System.Collections;
using UnityEngine;

public class DialogueActions : MonoBehaviour
{
    [SerializeField] GameObject mitlanCover;
    [SerializeField] GameObject izelBoss;
    [SerializeField] GameObject mitlanBoss;
    [SerializeField] int conversationNum = 5;
    [SerializeField] int dialogueEntryNum = 6;

    BossRoomController bossRoomController;
    GameObject mitlanCoverClone;

    public void ConversationEnded()
    {
        print(gameObject.name + "  Conversation Ended");
        if (bossRoomController == null)
        {
            bossRoomController = FindObjectOfType<BossRoomController>();
            print(bossRoomController.gameObject.name);
        }
        DialogueEntry dialogueEntry = DialogueManager.CurrentConversationState.subtitle.dialogueEntry;
        int conversationID = dialogueEntry.conversationID; //<-- This is the conversation ID.
        if (conversationID == 4)
        {
            print("Starting Fight from Dialogue Actions in  " + gameObject.name);
            bossRoomController.StartFight();
        }
        if (conversationID == 5)
        {
            bossRoomController.StartFightDelayed(mitlanCoverClone);
            izelBoss.GetComponent<Level3Boss>().DestroyBoss();
        }
        if (conversationID == 6)
        {
            EndGame();
        }
    }

    public void OnConversationLineEnded()
    {

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
        mitlanCoverClone = Instantiate(mitlanCover, izelBoss.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        yield return new WaitForSeconds(1);
        mitlanBoss.transform.position = izelBoss.transform.position;
        mitlanBoss.transform.parent.gameObject.SetActive(true);
    }

    void EndGame()
    {
        print("Ending Game");
        FindObjectOfType<BossRoomController>().GameFinished();
        mitlanBoss.GetComponent<Level3Boss>().DestroyBoss();
    }
}

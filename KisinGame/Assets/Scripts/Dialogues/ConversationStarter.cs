using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] BossRoomController bossRoomController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //The collider is disabled to stop the dialogue to be activated again
            GetComponent<Collider2D>().enabled = false;
            //collision.GetComponent<PlayerIdentifer>().GetDialogueReference(transform);
        }

    }

    public void StartConversation()
    {
        GetComponent<DialogueSystemTrigger>().OnUse();
    }

    public void StartBossFight()
    {
        bossRoomController.StartFight();
    }
}

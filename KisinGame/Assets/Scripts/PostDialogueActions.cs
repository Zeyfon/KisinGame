using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostDialogueActions : MonoBehaviour
{
    void ConversationEnded()
    {
        GameObject.FindObjectOfType<BossRoomController>().AfterConversationEndedActions();
    }
}

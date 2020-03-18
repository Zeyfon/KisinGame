using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class ConversationStarter : MonoBehaviour
{

    public void StartConversation()
    {
        GetComponent<DialogueSystemTrigger>().OnUse();
    }

}

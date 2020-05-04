using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class GetDialogueInformation : MonoBehaviour
{
    public void GetDialogueInfo()
    {
        int conversationID = DialogueManager.LastConversationID;
        if(conversationID == 2)
        {
            GetComponent<PlayMakerFSM>().SendEvent("DoNotSave");
        }
        else
        {
            GetComponent<PlayMakerFSM>().SendEvent("DoSave");

        }
    }
}

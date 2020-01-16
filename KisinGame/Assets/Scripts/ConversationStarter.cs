using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class ConversationStarter : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void StartConversation()
    {
        GetComponent<DialogueSystemTrigger>().OnUse();
        print("Dialogue should appear");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class ConversationStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<DialogueSystemTrigger>().conversationActor = FindObjectOfType<PlayerIdentifer>().gameObject.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
    }
    public void StartConversation(Transform transform)
    {

        GetComponent<DialogueSystemTrigger>().conversationConversant = transform;
        GetComponent<DialogueSystemTrigger>().OnUse();
        print("Dialogue should appear");
    }
}

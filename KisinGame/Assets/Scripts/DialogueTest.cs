using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DialogueTest : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<DialogueSystemTrigger>().conversationActor= transform.parent.GetChild(0);
        GetComponent<DialogueSystemTrigger>().conversationConversant = transform.parent.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

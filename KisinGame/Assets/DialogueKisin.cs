using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DialogueKisin : MonoBehaviour
{
    Transform targetTransform = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConversationEndedChecker()
    {
        int id = DialogueManager.LastConversationID;
        print(id);
        if (id == 7)
        {
            targetTransform = GameObject.FindObjectOfType<DoubleJumpEvent>().transform;
            targetTransform.GetComponent<DoubleJumpEvent>().DoubleJumpSkillAcquiringEvent();

        }
    }

    public void ConversationStartChecker()
    {
        int id = DialogueManager.LastConversationID;
        print(id);
        if(id == 7)
        {
            targetTransform = GameObject.FindObjectOfType<DoubleJumpEvent>().transform;
        }
    }
}

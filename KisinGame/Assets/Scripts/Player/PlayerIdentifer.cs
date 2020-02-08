using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class PlayerIdentifer : MonoBehaviour
{
    [SerializeField] VirtualCamera activeVCamera;
    Transform dialogueToStart;

    public VirtualCamera GetVCamera()
    {
        return activeVCamera;
    }

    public void SetVCamera(VirtualCamera cam)
    {
        activeVCamera = cam;
    }

    public void StartBossFight()
    {
        if(!dialogueToStart) return;
        dialogueToStart.GetComponent<ConversationStarter>().StartBossFight();
        dialogueToStart = null;
    }

    public void GetDialogueReference(Transform transform)
    {
        dialogueToStart = transform;
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class DialoguesList : MonoBehaviour
{
    public  List<GameObject> dialogues;

    public void RunDialogue(int index)
    {
        //Get the conversation name
        string conversation = dialogues[index].GetComponent<DialogueSystemTrigger>().conversation;

        SetTheConversant(index, conversation);
        CheckIfSavingMustBeStopped(conversation);

        //Run Dialogue
        dialogues[index].GetComponent<DialogueSystemTrigger>().OnUse();

    }
    


    void CheckIfSavingMustBeStopped(string conversation)
    {
        if (conversation == "Level3/Dialogue1")
        {
            print("Stop Saving");
            FindObjectOfType<DialogueSystemController>().GetComponent<PlayMakerFSM>().SendEvent("StopSaving");
        }
    }

    void SetTheConversant(int index, string conversation)
    {
        if (conversation == "Level3/Dialogue1")
        {
            print("Setting Dialogue 1 conversant");
            dialogues[index].GetComponent<DialogueSystemTrigger>().conversationConversant = GameObject.FindGameObjectWithTag("Izel").transform;
            return;
        }
        else if (conversation == "Level3/Dialogue2" || conversation == "Level3/Dialogue3")
        {
            print("Setting Dialogue 2 Conversant");
            dialogues[index].GetComponent<DialogueSystemTrigger>().conversationConversant = GameObject.FindGameObjectWithTag("Mitlan").transform;
            return;
        }
        //else if (conversation == "Level3/Dialogue3")
        //{
        //    return;
        //}
        else
        {
            dialogues[index].GetComponent<DialogueSystemTrigger>().conversationConversant = GameObject.FindGameObjectWithTag("Kisin").transform;
            return;
        }
    }
}

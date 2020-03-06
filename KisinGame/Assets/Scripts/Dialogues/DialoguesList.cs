using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class DialoguesList : MonoBehaviour
{
    public  List<GameObject> dialogues;

    public bool WillBeDialogueBeforeBossFight()
    {
        if (dialogues[0] == null)
        {
            print("Will be no dialogue before figts");
            return false;
        }
        else
        {
            print("Will be dialogue before boss fight");
            return true;
        }
    }

    public Transform GetDialogue(int i)
    {
        print("Starting Dialogue  " + dialogues[i].name);
        return dialogues[i].transform;
    }

    public void RunDialogue(int index)
    {
        print("Wants To Run Dialogue");
        dialogues[index].GetComponent<DialogueSystemTrigger>().OnUse();
    }
}

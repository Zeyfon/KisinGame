using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class DialoguesList : MonoBehaviour
{
    [SerializeField] List<Transform> dialogues;

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
        return dialogues[i];
    }

    public void RunDialogue(int index)
    {
        print("Still before");
        dialogues[index].GetComponent<DialogueSystemTrigger>().OnUse();
    }
}

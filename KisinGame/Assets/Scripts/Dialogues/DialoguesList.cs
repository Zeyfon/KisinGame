using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class DialoguesList : MonoBehaviour
{
    public  List<GameObject> dialogues;

    private void Start()
    {
        if (dialogues[0] == null)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }

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

    public void RunDialogue(int index)
    {
        print("DialogueList running dialogue");
        dialogues[index].GetComponent<DialogueSystemTrigger>().OnUse();
        string conversation = dialogues[index].GetComponent<DialogueSystemTrigger>().conversation;
        print(conversation);
        if (conversation == "Level3/Dialogue2" || conversation == "Level3/Dialogue1")
        {
            FindObjectOfType<DialogueSystemController>().GetComponent<PlayMakerFSM>().SendEvent("StartBossFightAfterDialogue");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerIdentifer>())
        {
            RunDialogue(0);
            GetComponent<Collider2D>().enabled = false;
        }

    }
}

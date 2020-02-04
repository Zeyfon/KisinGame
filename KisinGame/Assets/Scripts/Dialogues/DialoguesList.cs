using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DialoguesList : MonoBehaviour
{
    [SerializeField] List<Transform> dialogues;

    private void Start()
    {
        dialogues = new List<Transform>(transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            dialogues.Add(transform.GetChild(i));
        }
    }

    public Transform GetDialogue(int i)
    {
        print("Starting Dialogue  " + dialogues[i].name);
        return dialogues[i];
    }
}

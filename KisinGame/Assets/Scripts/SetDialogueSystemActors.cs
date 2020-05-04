using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using HutongGames.PlayMaker;

public class SetDialogueSystemActors : MonoBehaviour
{
    [SerializeField] string actorTag = null;
    [SerializeField] string conversantTag = null;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Transform actor = null;
        Transform conversant = null;
        while(actor == null)
        {
            actor = GameObject.FindGameObjectWithTag(actorTag).transform;
            yield return new WaitForEndOfFrame();
        }
        while (conversant == null)
        {
            conversant = GameObject.FindGameObjectWithTag(conversantTag).transform;
            yield return new WaitForEndOfFrame();
        }
        GetComponent<DialogueSystemTrigger>().conversationActor = actor;
        GetComponent<DialogueSystemTrigger>().conversationConversant = conversant;
    }
}

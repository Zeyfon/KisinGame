using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using HutongGames.PlayMaker;
using PixelCrushers.DialogueSystem;

public class CinematicControlRemover : MonoBehaviour
{
    [SerializeField] Transform dialogueTutorial;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += StartDialogue;
        player = GameObject.FindObjectOfType<PlayerIdentifer>().gameObject;
    }
    
    void DisableControl(PlayableDirector director)
    {
        player.GetComponent<PlayMakerFSM>().SendEvent("ControllerDisabled");
        player.transform.GetChild(0).gameObject.SetActive(false);
    }

    void StartDialogue(PlayableDirector director)
    {
        print("Started Dialogue");
        dialogueTutorial.GetComponent<DialogueSystemTrigger>().OnUse();
    }

}

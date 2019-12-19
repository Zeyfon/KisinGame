using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;


public class AnimationEvents_FD_Vines : MonoBehaviour
{
    PlayMakerFSM pFSM;
    // Start is called before the first frame update
    void Start()
    {
        pFSM = GetComponent<PlayMakerFSM>();
        Debug.Log("AnimEvents: " + pFSM.name);
    }

    void Attack_Ended()
    {
        pFSM.Fsm.Event("Attack_Ended");
    }
}

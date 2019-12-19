using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class AnimationEvents_GJ : MonoBehaviour
{
    PlayMakerFSM[] pFSMs;
    // Start is called before the first frame update
    void Start()
    {
        pFSMs = GetComponents<PlayMakerFSM>();
    }

    void Missile_Spawn()
    {
        pFSMs[1].Fsm.Event("Missile_Spawn");
    }

    void PixanDrops_Create()
    {
        pFSMs[1].Fsm.Event("PixanDrops_Create");
    }

    void GameObject_Destroy()
    {
        pFSMs[1].Fsm.Event("GameObject_Destroy");
    }

    void MissilLaunchPreparation_Sound()
    {
        pFSMs[1].Fsm.Event("MissileLaunch_Preparation");
    }

}

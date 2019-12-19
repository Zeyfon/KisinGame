using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class AnimationEvents_BD : MonoBehaviour
{
    PlayMakerFSM[] pFSMs;
    // Start is called before the first frame update
    void Start()
    {
        pFSMs = GetComponents<PlayMakerFSM>();
        Debug.Log("AnimEvents: " + pFSMs[1].name);
    }

    void AttackPreparation_Sound()
    {
        pFSMs[1].Fsm.Event("AttackPreparation_Sound");
    }
    void LaunchBomb_Sound()
    {
        pFSMs[1].Fsm.Event("LaunchBomb_Sound");
    }

    void Bomb_Create()
    {
        pFSMs[1].Fsm.Event("Bomb_Create");
    }

    void BackJump_Sound()
    {
        pFSMs[1].Fsm.Event("BackJump_Sound");
    }

    void PlayerPosition_Get()
    {
        pFSMs[1].Fsm.Event("PlayerPosition_Get");
    }

    void PixanDrops_Create()
    {
        pFSMs[1].Fsm.Event("PixanDrops_Create");
    }

    void GameObject_Destroy()
    {
        pFSMs[1].Fsm.Event("GameObject_Destroy");
    }

}

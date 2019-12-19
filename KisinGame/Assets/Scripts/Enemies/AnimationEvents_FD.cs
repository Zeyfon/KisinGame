using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class AnimationEvents_FD : MonoBehaviour
{
    PlayMakerFSM[] pFSMs;
    // Start is called before the first frame update
    void Start()
    {
        pFSMs = GetComponents<PlayMakerFSM>();
        Debug.Log("AnimEvents: " + pFSMs[1].name);
    }

    void Attack_Sound()
    {
        pFSMs[1].Fsm.Event("Attack_Sound");
    }

    void Attack_Trigger()
    {
        pFSMs[1].Fsm.Event("Attack_Trigger");
    }

    void AttackPreparation_Sound()
    {
        pFSMs[1].Fsm.Event("AttackPreparation_Sound");
    }

    void BackJump_Sound()
    {
        pFSMs[1].Fsm.Event("Backjump_Sound");
    }

    void PixanDrops_Create()
    {
        pFSMs[1].Fsm.Event("PixanDrops_Create");
    }

    void GameObject_Destroy()
    {
        pFSMs[1].Fsm.Event("GameObject_Destroy");
    }

    void RangeAttack_Sound()
    {
        pFSMs[1].Fsm.Event("RangeAttack_Sound");
    }

    void Vines_Create()
    {
        pFSMs[1].Fsm.Event("Vines_Create");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class AnimationEvents_AD : MonoBehaviour
{
    PlayMakerFSM[] pFSMs;
    // Start is called before the first frame update
    void Start()
    {
        pFSMs = GetComponents<PlayMakerFSM>();
        //Debug.Log("AnimEvents: " + pFSMs[1].name);
    }

    void Flip_Check()
    {
        pFSMs[1].Fsm.Event("Flip_Check");
    }
    void Attack_Impulse()
    {
        pFSMs[1].Fsm.Event("Attack_Impulse");
    }

    void PixanDrops_Create()
    {
        pFSMs[1].Fsm.Event("PixanDrops_Create");
    }

    void GameObject_Destroy()
    {
        pFSMs[1].Fsm.Event("GameObject_Destroy");
    }
    void Attack1_Sound()
    {
        pFSMs[1].Fsm.Event("Attack1_Sound");
    }
    void Attack2_Sound()
    {
        pFSMs[1].Fsm.Event("Attack2_Sound");
    }
    void AttackPreparation_Sound()
    {
        pFSMs[1].Fsm.Event("AttackPreparation_Sound");
    }
    void BackJump_Sound()
    {
        pFSMs[1].Fsm.Event("Backjump_Sound");
    }
}

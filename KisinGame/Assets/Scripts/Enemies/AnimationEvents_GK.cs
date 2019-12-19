using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class AnimationEvents_GK : MonoBehaviour
{
    PlayMakerFSM[] pFSMs;
    // Start is called before the first frame update
    void Start()
    {
        pFSMs = GetComponents<PlayMakerFSM>();
    }

    void Attack_Impulse()
    {
        pFSMs[1].Fsm.Event("Attack_Impulse");
    }

    void Attack_Sound()
    {
        pFSMs[1].Fsm.Event("Attack_Sound");
    }

    void Flip_Check()
    {
        pFSMs[1].Fsm.Event("Flip_Check");
    }

    void AttackPreparation_Sound()
    {
        pFSMs[1].Fsm.Event("AttackPreparation_Sound");
    }

    void Patroling_Impulse()
    {
        pFSMs[1].Fsm.Event("Patroling_Impulse");
    }

    void Patroling_StopImpulse()
    {
        pFSMs[1].Fsm.Event("Patroling_StopImpulse");
    }

    void Patroling_Count()
    {
        pFSMs[1].Fsm.Event("Patroling_Count");
    }

    void Chasing_Impulse()
    {
        pFSMs[1].Fsm.Event("Chasing_Impulse");
    }

    void Chasing_StopImpulse()
    {
        pFSMs[1].Fsm.Event("Chasing_StopImpulse");
    }

    void Chasing_Count()
    {
        pFSMs[1].Fsm.Event("Chasing_Count");
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

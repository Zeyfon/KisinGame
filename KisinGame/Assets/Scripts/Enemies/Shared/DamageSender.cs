using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class DamageSender : MonoBehaviour
{
    [SerializeField] PlayMakerFSM[] pFSMs;

    public void SendDamageToPlayer(int damage, Transform target)
    {
        print(target.gameObject);
        if (pFSMs.Length==0)
        {
            pFSMs = target.GetComponents<PlayMakerFSM>();
            //print("Found Player's Health");
        }
        FsmEventData myfsmEventData = new FsmEventData();
        myfsmEventData.IntData = damage;
        myfsmEventData.GameObjectData = gameObject;
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        pFSMs[1].Fsm.Event("_PlayerDamaged");
        //print("PlayerDamaged");
    }
}

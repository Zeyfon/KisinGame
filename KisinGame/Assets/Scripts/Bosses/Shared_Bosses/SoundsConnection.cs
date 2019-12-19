using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class SoundsConnection : MonoBehaviour
{
    [SerializeField] PlayMakerFSM pFSM;

    public void SendSoundEventToFSM(string action)
    {
        FsmEventData myfsmEventData = new FsmEventData();
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        pFSM.Fsm.Event(action);
    }
}

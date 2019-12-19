using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class EnableBossUI : MonoBehaviour
{
    public void ActivateBossUI()
    {
        PlayMakerFSM playerHealthListenerFSM = GetComponent<PlayMakerFSM>();
        FsmEventData myfsmEventData = new FsmEventData();
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        playerHealthListenerFSM.Fsm.Event("EnableBossUI");
    }
}

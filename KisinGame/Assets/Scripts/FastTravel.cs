using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class FastTravel : MonoBehaviour
{
    Transform gameManager;
    PlayMakerFSM[] pFSMs;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f);
        while (!gameManager)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public void FasTravel(int sceneBuildIndex)
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").transform;
        pFSMs = gameManager.GetComponents<PlayMakerFSM>();
        foreach (PlayMakerFSM fsm in pFSMs)
        {
            if (fsm.FsmName == "GameManager")
            {
                FsmEventData myfsmEventData = new FsmEventData();
                myfsmEventData.IntData = sceneBuildIndex;
                HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
                fsm.SendEvent("FastTravel");
            }
        }
        ES3.DeleteFile("SaveData");
    }
}

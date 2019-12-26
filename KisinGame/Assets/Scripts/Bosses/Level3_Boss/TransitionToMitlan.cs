using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class TransitionToMitlan : MonoBehaviour
{
    [SerializeField] GameObject mitlanBoss;

    public void StartMitlanPhase()
    {

        StartCoroutine(ChangeToMitlan());
    }

    IEnumerator ChangeToMitlan()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(1);
        mitlanBoss.transform.position = transform.position;
        mitlanBoss.SetActive(true);
    }

    public void ConversationEnded()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        FsmEventData myfsmEventData = new FsmEventData();
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        mitlanBoss.GetComponent<PlayMakerFSM>().Fsm.Event("ResetUI");
        mitlanBoss.GetComponent<Level3Boss>().StartActions();
        return;
    }


}

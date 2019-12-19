using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class ThirdPhaseTransition : MonoBehaviour
{
    [SerializeField] GameObject mitlanBoss;

    private void Start()
    {
        StartCoroutine(PhaseThreeBossTimer());
    }

    IEnumerator PhaseThreeBossTimer()
    {
        yield return new WaitForSeconds(3);
        mitlanBoss.transform.position = transform.position;
        mitlanBoss.SetActive(true);
        //GameObject clone = Instantiate(mitlanBoss, transform.position, Quaternion.identity, transform.parent.transform);
        yield return new WaitForSeconds(3);
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        FsmEventData myfsmEventData = new FsmEventData();
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        mitlanBoss.GetComponent<PlayMakerFSM>().Fsm.Event("ResetUI");
        yield return new WaitForSeconds(2);

        mitlanBoss.GetComponent<Level3Boss>().StartActions();

    }
}

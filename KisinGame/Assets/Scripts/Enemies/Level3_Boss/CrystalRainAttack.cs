using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRainAttack : MonoBehaviour
{
    [UnityEngine.Tooltip("Set at runtinme")]
     List<Transform> rainSlots = new List<Transform>();
    [SerializeField] float timeBetweenRainSlotsAttack = 1f;
    [SerializeField] float timetoStartRainAttack = 1f;

    public int disabledRainSlotQuantity = 0;

    CrystalRainAttack thisAction;
    Vector3 initialPosition;
    int activeRainSlotsQuantity = 0;
    bool mitlanActive = false;


    private void Start()
    {
        BossRoomController bossController= GetComponent<Level3Boss>().bossRoomController;
        rainSlots = bossController.transform.parent.GetChild(5).GetComponent<CrystalRainAttackList>().crystalRainList;
        initialPosition = rainSlots[0].transform.position;
        mitlanActive = GetComponent<Level3Boss>().MitlanIsActive();
    }

    //Animation Event. Produce Sound
    void CrystalRain_Starts()
    {
        SetRainSlotsToInitialPosition();
        GetComponent<SoundsConnection>().SendSoundEventToFSM("CrystalRainAttackStart");
        StartCoroutine(RainSlotsActions()); 
    }

    void SetRainSlotsToInitialPosition()
    {
        foreach(Transform rainSlot2 in rainSlots)
        {
            rainSlot2.GetComponent<CrystalRainSlot>().SetInitialPosition();
        }
        Debug.Break();
    }

    IEnumerator RainSlotsActions()
    {

        List<Transform> currentActiveRainSlots = new List<Transform>();
        int phase = GetComponent<Level3Boss>().phase;
        //print("CrystalRain phase " + phase);
        ActivateRainSlots(currentActiveRainSlots, phase);
        yield return new WaitForSeconds(timetoStartRainAttack);
        StartCoroutine(StartRainSlotAttack(currentActiveRainSlots));
        disabledRainSlotQuantity = 0;
        while (disabledRainSlotQuantity < activeRainSlotsQuantity-phase)
        {
            yield return null;
        }

        GetComponent<Animator>().SetInteger("Attack", 90);
        if (mitlanActive) transform.GetChild(4).GetComponent<Animator>().SetInteger("Attack", 90);

    }


    private void ActivateRainSlots(List<Transform> currentActiveRainSlots, int phase)
    {
        activeRainSlotsQuantity = 0;
        List<Transform> temporalRainSlots = new List<Transform>(rainSlots);
        Transform currentRainSlot;
        int limit = rainSlots.Count - (3 - phase);
        for (int i = 0; i < limit; i++)
        {
            currentRainSlot = temporalRainSlots[Random.Range(0, temporalRainSlots.Count)];
            temporalRainSlots.Remove(currentRainSlot);
            currentRainSlot.gameObject.SetActive(true);
            currentActiveRainSlots.Add(currentRainSlot);
            activeRainSlotsQuantity++;
        }
    }
    IEnumerator StartRainSlotAttack(List<Transform> currentActiveRainSlots)
    {
        Transform currentRainSlot;
        int limit = currentActiveRainSlots.Count;
        for (int i = 0; i < limit; i++)
        {
            yield return new WaitForSeconds(timeBetweenRainSlotsAttack);
            currentRainSlot = currentActiveRainSlots[Random.Range(0, currentActiveRainSlots.Count)];
            currentRainSlot.GetComponent<CrystalRainSlot>().StartAttack(this);
            currentActiveRainSlots.Remove(currentRainSlot);
        }
    }

    public void Cancel()
    {
        foreach(Transform rainSlot in rainSlots)
        {
            rainSlot.gameObject.SetActive(false);
            StopAllCoroutines();
        }
    }
}

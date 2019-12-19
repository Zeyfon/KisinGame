using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRainAttack : MonoBehaviour
{
    [SerializeField] List<Transform> rainSlots = new List<Transform>();
    [SerializeField] float timeBetweenRainSlotsAttack = 1f;
    [SerializeField] float timetoStartRainAttack = 1f;
    [Tooltip("To get the initial y position")]
    [SerializeField] Transform rainSlotInitialPosition;

    public int disabledRainSlotQuantity = 0;

    CrystalRainAttack thisAction;
    Vector3 initialPosition;
    int activeRainSlotsQuantity = 0;

    private void Start()
    {
        initialPosition = rainSlotInitialPosition.position;
    }

    //Animation Event. Produce Sound
    void CrystalRain_Starts()
    {
        GetComponent<SoundsConnection>().SendSoundEventToFSM("CrystalRainAttackStart");
        ResetPositions();
        StartCoroutine(RainSlotsActions()); 
    }

    void ResetPositions()
    {
        foreach(Transform rainSlot2 in rainSlots)
        {
            rainSlot2.position = new Vector3(rainSlot2.position.x,initialPosition.y,0);
        }
    }

    IEnumerator RainSlotsActions()
    {

        List<Transform> currentActiveRainSlots = new List<Transform>();
        int phase = GetComponent<Level3Boss>().phase;

        ActivateRainSlots(currentActiveRainSlots, phase);
        yield return new WaitForSeconds(timetoStartRainAttack);
        StartCoroutine(StartRainSlotAttack(currentActiveRainSlots));
        disabledRainSlotQuantity = 0;
        while (disabledRainSlotQuantity < activeRainSlotsQuantity-phase)
        {
            yield return null;
        }

        GetComponent<Animator>().SetInteger("Attack", 90);
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

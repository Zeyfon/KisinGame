using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRainAttackList : MonoBehaviour
{
    public List<Transform> crystalRainList = new List<Transform>();

    private void Start()
    {
        if (crystalRainList[0].gameObject.activeInHierarchy)
        {
            foreach(Transform crystalSlot in crystalRainList)
            {
                crystalSlot.gameObject.SetActive(false);
            }
        }
    }
}

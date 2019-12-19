using System.Collections;
using UnityEngine;

public class HazardsManager : MonoBehaviour
{
    [Tooltip("Time to start the activation of all hazards")]
    [SerializeField] float timetoStart = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateChildrenTimer());
    }

    IEnumerator ActivateChildrenTimer()
    {
        ElectrileTile[] electricTiles;
        electricTiles = transform.GetComponentsInChildren<ElectrileTile>();
        LaserHazard[] lasers;
        lasers = transform.GetComponentsInChildren<LaserHazard>();
        yield return new WaitForSeconds(timetoStart);
        foreach(ElectrileTile tile in electricTiles)
        {
            tile.StartActions();
        }
        foreach(LaserHazard laser in lasers)
        {
            laser.StartActions();
        }
    }
}

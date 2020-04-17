using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{

    [Header("Internal Variables")]
    [SerializeField] float intervalSlowTime = 1;
    [SerializeField] float intervalFastTime = 1;
    [SerializeField] GameObject[] tilesArray = new GameObject[0];

    [Header("Sounds")]
    [SerializeField] AudioClip electricSound;
    [Range(0,1)]
    [SerializeField] float volume=1;

    ElectrileTile_Boss[] electricTiles;
    AudioSource audioSource;
    bool performingSound = false;

    float audioLength = 0;
    float intervalTime;
    bool startedLooping = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioLength = electricSound.length;
        electricTiles = new ElectrileTile_Boss[tilesArray.Length];
        for (int i = 0; i < tilesArray.Length; i++)
        {
            electricTiles[i] = tilesArray[i].transform.GetChild(0).GetComponent<ElectrileTile_Boss>();
        }
        intervalTime = intervalSlowTime;
        print("Tiling Done");
    }

    public void StartTileLoop()
    {
        if (!startedLooping)
        {
            StartCoroutine(TileLoop());
            startedLooping = true;
            return;
        }
        else
        {
            intervalTime = intervalFastTime;
            Debug.LogWarning("Tile Timing Changed");
        }

    }

    IEnumerator TileLoop()
    {
        int counter = 0;
        while(counter < 3)
        {

            StartCoroutine(FirstIteration(counter));
            counter++;
            yield return new WaitForSeconds(intervalTime);
        }
        yield return new WaitForSeconds(2);
        counter = 0;
        while (counter < 6)
        {

            StartCoroutine(SecondIteration(counter));
            counter++;
            yield return new WaitForSeconds(intervalTime);
        }
        yield return new WaitForSeconds(2);
        counter = 0;
        while (counter < 6)
        {

            StartCoroutine(ThirdIteration(counter));
            counter++;
            yield return new WaitForSeconds(intervalTime);
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(TileLoop());
    }

    IEnumerator FirstIteration(int counter)
    {
        switch (counter)
        {
            case 0:
                ActivateThirdPart();
                ActivateFourthPart();
                yield break;
            case 1:
                ActivateFifthPart();
                ActivateSecondPart();
                yield break;
            case 2:
                ActivateSixthPart();
                ActivateFirstPart();
                yield break;
            default:
                yield break;
        }
    }
    IEnumerator SecondIteration(int counter)
    {
        switch (counter)
        {
            case 0:
                ActivateFirstPart();
                yield break;
            case 1:
                ActivateSecondPart();
                yield break;
            case 2:
                ActivateThirdPart();
                yield break;
            case 3:
                ActivateFourthPart();
                yield break;
            case 4:
                ActivateFifthPart();
                yield break;
            case 5:
                ActivateSixthPart();
                yield break;
            default:
                yield break;
        }
    }

    IEnumerator ThirdIteration (int counter)
    {
        switch (counter)
        {
            case 0:
                ActivateSixthPart();
                yield break;
            case 1:
                ActivateFifthPart();
                yield break;
            case 2:
                ActivateFourthPart();
                yield break;
            case 3:
                ActivateThirdPart();

                yield break;
            case 4:
                ActivateSecondPart();
                yield break;
            case 5:
                ActivateFirstPart();
                yield break;
            default:
                yield break;
        }
    }

    void ActivateFirstPart()
    {
        electricTiles[0].StartAttack_Signal();
        electricTiles[1].StartAttack_Signal();
        electricTiles[2].StartAttack_Signal();
    }
    void ActivateSecondPart()
    {
        electricTiles[3].StartAttack_Signal();
        electricTiles[4].StartAttack_Signal();
        electricTiles[5].StartAttack_Signal();
    }
    void ActivateThirdPart()
    {
        electricTiles[6].StartAttack_Signal();
        electricTiles[7].StartAttack_Signal();
        electricTiles[8].StartAttack_Signal();
    }
    void ActivateFourthPart()
    {
        electricTiles[9].StartAttack_Signal();
        electricTiles[10].StartAttack_Signal();
        electricTiles[11].StartAttack_Signal();
    }
    void ActivateFifthPart()
    {
        electricTiles[12].StartAttack_Signal();
        electricTiles[13].StartAttack_Signal();
        electricTiles[14].StartAttack_Signal();
    }
    void ActivateSixthPart()
    {
        electricTiles[15].StartAttack_Signal();
        electricTiles[16].StartAttack_Signal();
        electricTiles[17].StartAttack_Signal();
    }

    public void StartElectric_Sound()
    {
        if (!performingSound)
        {
            StartCoroutine(Electric_Sound());
            performingSound = true;
        }

    }
   
    IEnumerator Electric_Sound()
    {
        audioSource.PlayOneShot(electricSound, volume);
        yield return new WaitForSeconds(audioLength - 0.1f);
        performingSound = false;
    }

    //Called From Health FSM. Dead State
    public void StartDisableElectricTiles()
    {
        StartCoroutine(DisableElectricTiles());
    }
    IEnumerator DisableElectricTiles()
    {
        yield return new WaitForSeconds(0.5f);
        print("Stopping all Coroutines");
        StopAllCoroutines();
    }
}

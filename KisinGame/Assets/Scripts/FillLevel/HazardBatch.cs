using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBatch : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] AudioClip electricSound;
    [SerializeField] float electricVolume=1;

    [SerializeField] AudioClip laserSound;
    [SerializeField] float laserVolume = 1;

    float electricTime;
    bool tilePerforming = false;
    float laserTime;
    bool laserPerforming = false;

    AudioSource audioSource;

    int counter_Test;

    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        electricTime = electricSound.length;
        laserTime = laserSound.length;
    }

    #region Electric Sound
    public void StartElectricTSound()
    {
        if (!tilePerforming)
        {
            tilePerforming = true;
            StartCoroutine(ElectricSound());
        }
    }

    IEnumerator ElectricSound()
    {
        counter_Test += 1;
        audioSource.PlayOneShot(electricSound, electricVolume);
        yield return new WaitForSeconds(electricTime - 0.1f);
        tilePerforming = false;
        counter_Test = 0;
    }
    #endregion

    #region LaserSound
    public void StartLaserTSound()
    {
        if (!laserPerforming)
        {
            laserPerforming = true;
            StartCoroutine(LaserSound());
        }
    }

    IEnumerator LaserSound()
    {
        counter_Test += 1;
        audioSource.PlayOneShot(laserSound, laserVolume);
        yield return new WaitForSeconds(laserTime - 0.1f);
        laserPerforming = false;
        counter_Test = 0;
    }
    #endregion
}


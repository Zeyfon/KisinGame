using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;
using HutongGames.PlayMaker;

public class KisinShard : MonoBehaviour, ISaveable
{
    bool status = false;
    // Start is called before the first frame update

    void Start()
    {
        FindObjectOfType<SavingWrapper>().Load();
    }
    
    void CheckifTaken(bool taken)
    {
        if (taken)
        {
            DisableObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerIdentifer>())
        {
            LevelUpPlayerStats();
            SaveState();
            DisableObject();
        }
    }

    void DisableObject()
    {
        gameObject.SetActive(false);
    }

    void SaveState()
    {
        FindObjectOfType<SavingWrapper>().Save();
    }

    void LevelUpPlayerStats()
    {
        PlayMakerFSM[] pFSMs = GameObject.FindGameObjectWithTag("GameManager").GetComponents<PlayMakerFSM>();
        foreach(PlayMakerFSM fsm in pFSMs)
        {
            fsm.SendEvent("UpgradePlayerStats");
        }
        //Open the menus to let the player choose which stat level up
    }

#region Interfaces
    public object CaptureState()
    {
        return status;
    }

    public void RestoreState(object state)
    {
        status = (bool)state;
        CheckifTaken(status);
    }
#endregion
}

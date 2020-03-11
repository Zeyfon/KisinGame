using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class SavingAreaTrigger : MonoBehaviour
{
    GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        print(gameManager);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.GetComponent<PlayerIdentifer>())
    //    {
    //        print("Player opening door");
    //        savingAreaDoor.OpenDoor();
    //        gameManager.GetComponent<PlayMakerFSM>().SendEvent("LoadScene")
    //    }
    //}
}

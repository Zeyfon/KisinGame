using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class EnemyActivater : MonoBehaviour
{
    GameObject[] childrenGO;
    PlayMakerFSM[] pFSMs;
    Rigidbody2D rb;
    bool canInteract = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetInformation());
    }

    IEnumerator GetInformation()
    {
        yield return new WaitForSeconds(1);
        rb = transform.parent.GetComponent<Rigidbody2D>();
        pFSMs = transform.parent.GetComponents<PlayMakerFSM>();
        int childQuantity = transform.parent.childCount;
        childrenGO = new GameObject[childQuantity];
        for (int i = 0; i < childQuantity; i++)
        {
            childrenGO[i] = transform.parent.GetChild(i).gameObject;
        }
        yield return new WaitForSeconds(1);
        DisableActions();
    }

    void DisableActions()
    {

        foreach (GameObject gameObject1 in childrenGO)
        {
            if (gameObject != gameObject1)
                gameObject1.SetActive(false);
        }
        foreach (PlayMakerFSM pFSM in pFSMs)
        {
            pFSM.enabled = false;
        }
        rb.Sleep();
        canInteract = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canInteract) return;
        //print("Something is inside  " + collision.gameObject.name);
        if (collision.CompareTag("EnemyActivator"))
        {
            
            print(gameObject.name + " is in player area");

            foreach(GameObject gameObject1 in childrenGO)
            {
                gameObject1.SetActive(true);
            }
            foreach(PlayMakerFSM pFSM in pFSMs)
            {
                pFSM.enabled = true;
            }
            rb.WakeUp();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyActivator"))
        {
            //print(gameObject.name + " is out of player area");
            DisableActions();
        }
    }

}

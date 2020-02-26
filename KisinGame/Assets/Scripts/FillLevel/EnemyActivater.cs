using System.Collections;
using UnityEngine;

public class EnemyActivater : MonoBehaviour
{
    GameObject[] childrenGO;
    bool canInteract = false;
    bool isInsidePlayerDetection = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        int childQuantity = transform.parent.childCount;
        childrenGO = new GameObject[childQuantity];
        for (int i = 0; i < childQuantity; i++)
        {
            childrenGO[i] = transform.parent.GetChild(i).gameObject;
        }
        yield return new WaitForSeconds(1);
        if (!isInsidePlayerDetection) DisableEnemy();
        canInteract = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInsidePlayerDetection = true;
        if (!canInteract) return;
        if (collision.CompareTag("EnemyActivator"))
        {
            //print("Player in Range");
            ActivateEnemy();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInsidePlayerDetection = false;
        if (!canInteract) return;
        if (collision.CompareTag("EnemyActivator"))
        {
            //print("Player out of range");
            DisableEnemy();
        }
    }

    void DisableEnemy()
    {
        foreach (GameObject tempGameObject in childrenGO)
        {
            if (this.gameObject != tempGameObject)
                tempGameObject.SetActive(false);
        }
    }

    private void ActivateEnemy()
    {
        foreach (GameObject tempGameObject in childrenGO)
        {
            tempGameObject.SetActive(true);
        }
    }

}

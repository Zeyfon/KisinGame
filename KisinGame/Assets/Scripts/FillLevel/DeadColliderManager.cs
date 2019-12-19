using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;

public class DeadColliderManager : MonoBehaviour
{
    Transform newCheckpointTransform;
    PlayMakerFSM[] pmFSMs;
    // Start is called before the first frame update

    public void ReturnToCheckpoint(Transform playerTransform, int damage)
    {
        StartCoroutine(TakePlayerToCheckpoint(playerTransform));

        pmFSMs = playerTransform.GetComponents<PlayMakerFSM>();
        FsmEventData myfsmEventData = new FsmEventData();
        myfsmEventData.IntData = damage;
        myfsmEventData.GameObjectData = gameObject;
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        pmFSMs[1].Fsm.Event("_PlayerDamaged");

        print(playerTransform.GetChild(0));
    }
    public void LoadNewCheckpoint(Transform targetTransform)
    {
        newCheckpointTransform = targetTransform;
        //print("New Checkpoint Loaded");
    }
    
    IEnumerator TakePlayerToCheckpoint(Transform playerTransform)
    {

        Rigidbody2D rb;
        yield return new WaitForSeconds(1);
        rb = playerTransform.GetComponent<Rigidbody2D>();
        rb.position = new Vector2(newCheckpointTransform.position.x, newCheckpointTransform.position.y);
    }
}

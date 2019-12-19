using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class AnimationEvents_FruitBomb : MonoBehaviour
{
    PlayMakerFSM pFSM;
    // Start is called before the first frame update
    void Start()
    {
        pFSM = GetComponent<PlayMakerFSM>();
        Debug.Log("AnimController: " + pFSM.name);
    }

    void BombCreated_Inform()
    {
        pFSM.Fsm.Event("BombCreated_Inform");
    }
    void Explosion_Sound()
    {
        pFSM.Fsm.Event("Explosion_Sound");
    }

    void GameObject_Destroy()
    {
        pFSM.Fsm.Event("GameObject_Destroy");
    }

}

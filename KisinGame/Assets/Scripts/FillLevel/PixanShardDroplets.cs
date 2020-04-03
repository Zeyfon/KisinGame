using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class PixanShardDroplets : MonoBehaviour
{
    PlayMakerFSM playMakerFSM;
    ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    GameObject player;
    static int test = 0;

    private void Start()
    {
        playMakerFSM = GetComponent<PlayMakerFSM>();
        ps = GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag("Player");
        ps.trigger.SetCollider(10, player.GetComponent<Collider2D>());
    }
   void Update()
    {
        if (!ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }
    private void OnParticleTrigger()
    {
        // The particle count up must be slower than the counting cycle in the UI to prevent data losses.
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        for (int i = 0; i < numEnter; i++)
        {
            test += 1;
            print(test);
            ParticleSystem.Particle p = enter[i];
            p.remainingLifetime = -1;
            playMakerFSM.SendEvent("PixanCollected");
            FsmVariables.GlobalVariables.GetFsmInt("shards").Value +=1;
            enter[i] = p;
        }
    }
}

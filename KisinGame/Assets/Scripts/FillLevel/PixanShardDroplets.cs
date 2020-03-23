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
        int pixanShards;
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        for (int i = 0; i < numEnter; i++)
        {
            float timer = 0;
            while (timer < .1f)
            {
                timer += Time.deltaTime;
            }
            pixanShards = FsmVariables.GlobalVariables.GetFsmInt("shards").Value;
            ParticleSystem.Particle p = enter[i];
            pixanShards += 1;
            playMakerFSM.SendEvent("PixanCollected");
            FsmVariables.GlobalVariables.GetFsmInt("shards").Value = pixanShards;
        }
    }
}

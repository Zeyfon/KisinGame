using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class PixanShardDroplets : MonoBehaviour
{
    PlayMakerFSM playMakerFSM;
    ParticleSystem ps;
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
    static int test = 0;

    private void Start()
    {
        playMakerFSM = GetComponent<PlayMakerFSM>();
        ps = GetComponent<ParticleSystem>();

        StartCoroutine(PlayerCollider());
    }

    IEnumerator PlayerCollider()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
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
        int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        for (int i = 0; i < numInside; i++)
        {
            ParticleSystem.Particle p = inside[i];
            p.remainingLifetime = 0;
            playMakerFSM.SendEvent("PixanCollected");
            FsmVariables.GlobalVariables.GetFsmInt("shards").Value += 1;
            inside[i] = p;
        }
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);  
    }
}

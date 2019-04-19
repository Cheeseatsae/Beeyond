using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Harry;

public class RewardTriggerScript : MonoBehaviour
{
    public List<ParticleSystem> particles = new List<ParticleSystem>();

    public GameObject LightningObject;
    public GameObject RainObject;



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBeeController>() == null) return;
        AudioManagerScript.gameProgression = 20f;
        Roo.WindScript.WindStates = Roo.WindScript.Winds.REWARD;
        LightningObject.gameObject.SetActive(false);
        RainObject.gameObject.SetActive(false);

        StopParticles();
        
    }

    public void StopParticles()
    {
        foreach (ParticleSystem p in particles)
        {
            p.Stop();
        }
    }

        private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);
        Gizmos.color = new Color(1, 1, 0, .3f);
        Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.size);
    }

}

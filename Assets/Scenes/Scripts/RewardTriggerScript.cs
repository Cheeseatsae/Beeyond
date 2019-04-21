using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Harry;

public class RewardTriggerScript : MonoBehaviour
{
   // public GameObject LightningObject;
   // public List<ParticleSystem> rain = new List<ParticleSystem>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBeeController>() == null) return;
        AudioManagerScript.gameProgression = 20f;
        Roo.WindScript.WindStates = Roo.WindScript.Winds.REWARD;
    }

        private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);
        Gizmos.color = new Color(1, 1, 0, .3f);
        Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.size);
    }

}

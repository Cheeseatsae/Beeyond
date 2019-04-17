using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Harry;

public class RewardTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBeeController>() == null) return;
        AudioManagerScript.gameProgression = 10f;
    }
}

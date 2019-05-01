using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Harry;

public class RewardTriggerScript : MonoBehaviour
{
    public GameObject FinalWayPoint;
    public GameObject ObjectToTeleport;
    public float newBeeSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBeeController>() != null || other.gameObject.layer == 15) 
		{
			AudioManagerScript.gameProgression = 20f;
			Roo.WindScript.WindStates = Roo.WindScript.Winds.REWARD;
			//AnimationBee_FlyidleToFlying.disableBeeAnimator = true;
			ObjectToTeleport.GetComponent<PlayerBeeController>().maxSpeed = newBeeSpeed;
			ObjectToTeleport.GetComponent<PlayerBeeController>().target = FinalWayPoint;
			GameManagerScript._isGameRunning = false; // disables pause menu
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Harry;

public class BranchAnimTrigger : MonoBehaviour
{

    public GameObject branch;
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BeeController>() == null) return;

        if (hasPlayed) return;
        hasPlayed = true;
        branch.GetComponent<Animator>().SetTrigger("Trigger");

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);
        Gizmos.color = new Color(0, 1, 1, .3f);
        Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.size);
    }

}

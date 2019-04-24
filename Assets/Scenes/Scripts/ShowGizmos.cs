using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGizmos : MonoBehaviour
{
    [Range(0, 1)] public float redChannel;
    [Range(0, 1)] public float greenChannel;
    [Range(0, 1)] public float blueChannel;
    [Range(0, 1)] public float transparency;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(redChannel,greenChannel,blueChannel,1);
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);
        Gizmos.color = new Color(redChannel, greenChannel, blueChannel, transparency);
        Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.size);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{
    
    public class ButterflyWaypoint : MonoBehaviour
    {

        [Range(0,10)] public float radius = 3;
        [Range(0,1)] public float transparency = 0.5f;

        public static List<GameObject> waypoints = new List<GameObject>();
        
        private void Awake()
        {
            waypoints.Add(this.gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
            Gizmos.color = new Color(1,0.92f,0.016f, transparency);
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
    
}



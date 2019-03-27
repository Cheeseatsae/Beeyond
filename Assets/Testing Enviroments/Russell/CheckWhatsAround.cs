using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Harry
{
    public class CheckWhatsAround : MonoBehaviour
    {
        [HideInInspector]
        public List<GameObject> WhosAround = new List<GameObject>();
    
        public SphereCollider collider;
        public float raduisOfSphere;
    

    
        // Start is called before the first frame update
        void Start()
        {
            collider.radius = raduisOfSphere;
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<AIBeeController>())
            {
                WhosAround.Add(other.gameObject);
            }
        }
    
        private void OnTriggerExit(Collider other)
        {
            WhosAround.Remove(other.gameObject);
        }
    }


}

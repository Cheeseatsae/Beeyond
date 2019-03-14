using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{
    public class ObjAvoidance : MonoBehaviour
    {
    
        public Vector3 myPos;
    
        public Vector3 contactPoint;
        // Start is called before the first frame update
        void Start()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }
        
        public void CheckObjAvoidance()
        {
                
        }
    
        private void OnTriggerEnter(Collider other)
        {
            contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(myPos);
            Debug.DrawRay(myPos, contactPoint, Color.red);
        }

        private void OnTriggerExit(Collider other)
        {
            contactPoint = Vector3.zero;
        }
    }


}

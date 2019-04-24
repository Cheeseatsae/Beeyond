using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{
    public class HiveWaypoint : MonoBehaviour
    {

        public GameObject waypoint1;
        public GameObject waypoint2;
        public GameObject waypoint3;
        // Start is called before the first frame update
        void Start()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<AIBeeController>())
            {
                AIBeeController ai = other.GetComponent<AIBeeController>();
                if (ai.currentState == ai.returnToHive)
                {
                    ai.ChangeState(ai.enterHive);
                    
                }
            }
        }
    }

}


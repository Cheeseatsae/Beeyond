using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{
    public class MoveWaypoints : MonoBehaviour
    {
    
        public GameObject waypoints;

        public GameObject hive;

        public GameObject hive2;
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
            if (other.GetComponent<PlayerBeeController>())
            {
                waypoints.transform.position = new Vector3(waypoints.transform.position.x, waypoints.transform.position.y, 4.2f);
                hive.tag = null;
                hive2.tag = "Hive";
            }
        }
    }


}

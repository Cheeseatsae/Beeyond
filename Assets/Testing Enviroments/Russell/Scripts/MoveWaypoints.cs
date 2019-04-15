using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{
    public class MoveWaypoints : MonoBehaviour
    {
    


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
                hive.tag = "Untagged";
                hive2.tag = "Hive";
            }
        }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{
    public class EndingEvent : MonoBehaviour
    {

        public event Action SpawnTheBees; 
        // Start is called before the first frame update
        void Start()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }
    
        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerBeeController>())
            {
                SpawnTheBees();
            }
        }
    }


}

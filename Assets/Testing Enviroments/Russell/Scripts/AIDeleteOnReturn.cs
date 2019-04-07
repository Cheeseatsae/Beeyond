using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{
    public class AIDeleteOnReturn : MonoBehaviour
    {
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
                if (other.GetComponent<AIBeeController>().currentState ==
                    other.GetComponent<AIBeeController>().returnToHive)
                {
                    Destroy(other.gameObject);
                }
            }
            
        }
    }


}

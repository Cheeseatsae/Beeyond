using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{
    public class FollowTheLeader : MonoBehaviour
    {
        public GameObject leader;
        public float movementSpeed;
        
    
        // Start is called before the first frame update
        void Start()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
            transform.LookAt(leader.transform);
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }
    }

}


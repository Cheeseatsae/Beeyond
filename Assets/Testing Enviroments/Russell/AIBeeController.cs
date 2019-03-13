using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{
    
    public class AIBeeController : BeeController
    {
        
        public float minDist;
        public float maxDist;
        public Color rayColor = Color.green;

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            RayCastDistanceCheck();
        }

        public void RayCastDistanceCheck()
        {
            foreach (GameObject bees in _whatsAround.WhosAround)
            {
                float distance;
                Debug.Log(bees + "now raycast");
                //rayColor = Color.red;
                
                if (Physics.Raycast(_myBody.transform.position, (_myBody.transform.position - bees.transform.position), _whatsAround.raduisOfSphere))
                {
                    Debug.DrawRay(transform.position, (bees.transform.position - _myBody.transform.position), rayColor);
                    distance = Vector3.Distance(transform.position, bees.transform.position);
                    if(distance <= minDist )
                    {
                        Debug.Log(bees + "is to close");
                        Debug.DrawRay(transform.position, (bees.transform.position - _myBody.transform.position), Color.red);
                        //move character away from close object
                    }
                    else if (distance >= maxDist)
                    {
                        Debug.Log("I need to move closer to" + bees);
                        Debug.DrawRay(transform.position, (bees.transform.position - _myBody.transform.position), Color.blue);
                        //move closer to object
                    }
                }
            }
        }
        
    }
    
}



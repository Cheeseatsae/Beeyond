using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{
    
    public class AIBeeController : BeeController
    {
        
        public float minPlayerDistance;
        public float minAiDist;
        public float maxDist;
        public Color rayColor = Color.green;
        float distance;
        private Vector3 _moveForce;
        public float braking = 10000;
        private Vector3 brakeForce;
        public override void FixedUpdate()
        {
            
            base.FixedUpdate();
            RaycastPlayerDistanceCheck();
            RayCastAiDistanceCheck();
        }

        public void RaycastPlayerDistanceCheck()
        {
            
            if (Physics.Raycast(_myBody.transform.position, (target.transform.position - _myBody.transform.position), 30))
            {
                //Debug.Log("raycast plz");
                //Debug.DrawRay(transform.position, (target.transform.position - _myBody.transform.position), rayColor);
                distance = Vector3.Distance(transform.position, target.transform.position);
                if(distance <= minPlayerDistance )
                {
                    //Debug.Log(target + "is to close");
                    //.DrawRay(transform.position, (target.transform.position - _myBody.transform.position), Color.red);
                    _moveForce = ((transform.position - target.transform.position) * speedMult * 2);
                    _myBody.AddForce(_moveForce);


                }
            }
        }
        public void RayCastAiDistanceCheck()
        {
            foreach (GameObject bees in _whatsAround.WhosAround)
            {
                
                //Debug.Log(bees + "now raycast");
                //rayColor = Color.red;
                
                if (Physics.Raycast(_myBody.transform.position, (bees.transform.position - _myBody.transform.position), _whatsAround.raduisOfSphere))
                {
                    //Debug.DrawRay(transform.position, (bees.transform.position - _myBody.transform.position), rayColor);
                    distance = Vector3.Distance(transform.position, bees.transform.position);
                    if(distance <= minAiDist )
                    {
                        Debug.Log(bees + "is to close");
                        //Debug.DrawRay(transform.position, (bees.transform.position - _myBody.transform.position), Color.red);
                        _moveForce = ((transform.position - bees.transform.position) * speedMult);
                        _myBody.AddForce(_moveForce);
                    }
                    else if (distance >= maxDist)
                    {
                        Debug.Log("I need to move closer to" + bees);
                        //Debug.DrawRay(transform.position, (bees.transform.position - _myBody.transform.position), Color.blue);
                        _moveForce = ((bees.transform.position - transform.position) * speedMult);
                        _myBody.AddForce(_moveForce);
                    }
                }
            }
        }
        
    }
    
}



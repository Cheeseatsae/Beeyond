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
        public ObjAvoidance avoidColl;
        public Vector3 avoidanceForce;
        RaycastHit leftHit;
        RaycastHit rightHit;
        public float offset = 5;
        RaycastHit downHit;
        RaycastHit upHit;
        public GameObject upPoint;
        public GameObject downPoint;
        public GameObject leftPoint;
        public GameObject rightPoint;
        public override void FixedUpdate()
        {
            
            base.FixedUpdate();
            
            RaycastPlayerDistanceCheck();
            
            RayCastAiDistanceCheck();
            ObjAvoidance();
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
                    //Debug.DrawRay(transform.position, (target.transform.position - _myBody.transform.position), Color.red);
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
                        //Debug.Log(bees + "is to close");
                        //Debug.DrawRay(transform.position, (bees.transform.position - _myBody.transform.position), Color.red);
                        _moveForce = ((transform.position - bees.transform.position) * speedMult);
                        _myBody.AddForce(_moveForce);
                    }
                    else if (distance >= maxDist)
                    {
                        //Debug.Log("I need to move closer to" + bees);
                        //Debug.DrawRay(transform.position, (bees.transform.position - _myBody.transform.position), Color.blue);
                        _moveForce = ((bees.transform.position - transform.position) * speedMult);
                        _myBody.AddForce(_moveForce);
                    }
                }
            }
        }

        public void ObjAvoidance()
        {
            // velocity += force / Mathf.Abs(distance / 5);
            
            float distance;
            if (Physics.Raycast(rightPoint.transform.position, Quaternion.AngleAxis(45f, transform.up) * transform.forward, out rightHit, 10f))
            {
                distance = Vector3.Distance(_myBody.transform.position, rightHit.point);
                Debug.DrawLine(rightPoint.transform.position, rightHit.point, Color.cyan);
                avoidanceForce = (transform.position - rightHit.point) / (distance / offset) * 2;
                _myBody.AddForce(avoidanceForce);
            }
            if (Physics.Raycast(leftPoint.transform.position, Quaternion.AngleAxis(-45f, transform.up) * transform.forward, out leftHit, 10f))
            {
                distance = Vector3.Distance(_myBody.transform.position, leftHit.point);
                Debug.DrawLine(leftPoint.transform.position, leftHit.point, Color.cyan);
                avoidanceForce = (transform.position - leftHit.point) / (distance / offset)* 2;
                _myBody.AddForce(avoidanceForce);
            }
            if (Physics.Raycast(downPoint.transform.position, Quaternion.AngleAxis(45f, transform.right) * transform.forward, out downHit, 10f))
            {
                distance = Vector3.Distance(_myBody.transform.position, downHit.point);
                Debug.DrawLine(downPoint.transform.position, downHit.point, Color.cyan);
                avoidanceForce = (transform.position - downHit.point) / (distance / offset)* 2;
                _myBody.AddForce(avoidanceForce);
            }
            if (Physics.Raycast(upPoint.transform.position, Quaternion.AngleAxis(-45f, transform.right) * transform.forward, out upHit, 10f))
            {
                distance = Vector3.Distance(_myBody.transform.position, upHit.point);
                Debug.DrawLine(upPoint.transform.position, upHit.point, Color.cyan);
                avoidanceForce = (transform.position - upHit.point) / (distance / offset)* 2;
                _myBody.AddForce(avoidanceForce);
            }
        }


        
    }
    
}



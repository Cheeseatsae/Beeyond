using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{
    public class FollowThePlayer : AIStateBase
    {
        private float distance;
        public AIBeeController controller;
        public float minPlayerDistance;
        public float maxPlayerDistance;
        
        public float minAiDistance;
        public float maxAiDistance;
        
        public Rigidbody rb;
        public GameObject currentTarget;
        private Vector3 _moveForce;
        public float speed;
        public Vector3 avoidanceForce;
        
        private RaycastHit bottomHit;        
        private RaycastHit downHit;
        public GameObject downPoint;
        private RaycastHit leftHit;
        public GameObject leftPoint;
        public GameObject rightPoint;
        private RaycastHit rightHit;
        private RaycastHit topHit;
        private RaycastHit upHit;
        public GameObject upPoint;  
        
        public float offset = 5;
        
        private void Awake()
        {

        }

        public override void Enter()
        {
            base.Enter();
            rb = controller._myBody;
            currentTarget = controller.target;
            speed = controller.speedMult;
        }

        public override void Execute()
        {

            base.Execute();
            PlayerDistanceCheck();                     
            ObjAvoidance();
            AiDistanceCheck(); 
        }


        public void PlayerDistanceCheck()
        {           
            distance = Vector3.Distance(transform.position, controller.target.transform.position);
            
            if (distance <= minPlayerDistance)
            {
                _moveForce = (transform.position - controller.target.transform.position) * speed * 2;
                rb.AddForce(_moveForce);
            }
            else if (distance >= maxPlayerDistance)
            {
                _moveForce = (controller.target.transform.position - transform.position) * speed;
                rb.AddForce(_moveForce);
            }
        }
        
        public void AiDistanceCheck()
        {
            foreach (var bees in controller._whatsAround.WhosAround)
            {
                distance = Vector3.Distance(transform.position, bees.transform.position);
                if (distance <= minAiDistance)
                {
                    _moveForce = (transform.position - bees.transform.position) * speed;
                    rb.AddForce(_moveForce);
                }
                else if (distance >= maxAiDistance)
                {
                    _moveForce = (bees.transform.position - transform.position) * speed;
                    rb.AddForce(_moveForce);
                }
            }
        }
        public void ObjAvoidance()
        {
            float dist;
                if (Physics.Raycast(rightPoint.transform.position,
                    Quaternion.AngleAxis(15f, transform.up) * transform.forward, out rightHit, 1f))
                {
                    dist = Vector3.Distance(rb.transform.position, rightHit.point);
                    Debug.DrawLine(rightPoint.transform.position, rightHit.point, Color.cyan);
                    avoidanceForce = (transform.position - rightHit.point) / (dist / offset) * 2;
                    rb.AddForce(avoidanceForce);
                    
                }

                if (Physics.Raycast(leftPoint.transform.position,
                    Quaternion.AngleAxis(-15f, transform.up) * transform.forward, out leftHit, 1f))
                {
                    dist = Vector3.Distance(rb.transform.position, leftHit.point);
                    Debug.DrawLine(leftPoint.transform.position, leftHit.point, Color.cyan);
                    avoidanceForce = (transform.position - leftHit.point) / (dist / offset) * 2;
                    rb.AddForce(avoidanceForce);
                    
                }

                if (Physics.Raycast(downPoint.transform.position,
                    Quaternion.AngleAxis(15f, transform.right) * transform.forward, out downHit, 1f))
                {
                    dist = Vector3.Distance(rb.transform.position, downHit.point);
                    Debug.DrawLine(downPoint.transform.position, downHit.point, Color.cyan);
                    avoidanceForce = (transform.position - downHit.point) / (dist / offset) * 2;
                    rb.AddForce(avoidanceForce);                   
                }

                if (Physics.Raycast(upPoint.transform.position,
                    Quaternion.AngleAxis(-15f, transform.right) * transform.forward, out upHit, 1f))
                {
                    dist = Vector3.Distance(rb.transform.position, upHit.point);
                    Debug.DrawLine(upPoint.transform.position, upHit.point, Color.cyan);
                    avoidanceForce = (transform.position - upHit.point) / (dist / offset) * 2;
                    rb.AddForce(avoidanceForce);
                    
                }

        }
        
        
    }


}


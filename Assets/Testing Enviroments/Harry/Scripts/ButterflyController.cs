using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Harry
{
    
    public class ButterflyController : MonoBehaviour
    {
        public float rotateSpeed = 2;
        public float fleeDistance = 5;
        private GameObject _myModel;
        private Rigidbody _myBody;
        public GameObject player;
        public float speed;
        public float maxSpeed;
        public float varianceMult;

        private Vector3 movePos;
        private Vector3 lookTarget;
        private Vector3 _force;

        private bool atTarget = true;
        
        private void Awake()
        {
            _myModel = GetComponentInChildren<Renderer>().gameObject;
            _myBody = GetComponent<Rigidbody>();
            lookTarget = player.transform.position;
        }

        private void Start()
        {
            FindNextWaypoint(transform.position);
            transform.position = movePos;
        }

        void Update()
        {
            RotateTowards(lookTarget);
            PlayerCheck();
        }

        private void FixedUpdate()
        {
            TargetCheck();
            
            // setting desired velocity to be towards the target
            _force = ((movePos - transform.position) * speed);
            // applying sin variation and the wind effect
            _force = new Vector3(_force.x, _force.y + (Mathf.PerlinNoise(transform.position.x, transform.position.y) * varianceMult), _force.z);
            
            // adding the force normalized
            _myBody.AddForce(_force);

            // adding drag to slow us and clamping speed
            _myBody.velocity = Vector3.Lerp(_myBody.velocity, Vector3.zero, 0.05f);
            _myBody.velocity = Vector3.ClampMagnitude(_myBody.velocity, maxSpeed);

        }

        private void FindNextWaypoint(Vector3 pos)
        {
            float smallestDist = 9999999f;
            Vector3 toMoveTo = Vector3.zero;
            GameObject usedWaypoint = new GameObject();
            
            foreach (GameObject w in ButterflyWaypoint.waypoints)
            {
                float dist = Vector3.Distance(pos, w.transform.position);
                
                if (dist < smallestDist)
                {
                    smallestDist = dist;
                    toMoveTo = w.transform.position;
                    usedWaypoint = w;
                }
            }
            
            Debug.Log("Moving to " + toMoveTo, this);
            movePos = toMoveTo;
            ButterflyWaypoint.waypoints.Remove(usedWaypoint);
            Destroy(usedWaypoint);
        }
        
         private void TargetCheck()
        {
            if (atTarget)
                lookTarget = player.transform.position;
            else
                lookTarget = movePos;

            if (Vector3.Distance(transform.position, movePos) < 5)
                atTarget = true;
            
        }
         
        private void PlayerCheck()
        {
            if (Vector3.Distance(player.transform.position, transform.position) < fleeDistance && atTarget)
                GetNewTarget();
        }
        
        private void GetNewTarget()
        {
            FindNextWaypoint(transform.position);
            atTarget = false;
        }
        
        private void RotateTowards(Vector3 t)
        {
            // finding dir to turn towards
            Vector3 dir = t - _myModel.transform.position;
            // so we dont go upside down
            dir = new Vector3(dir.x, 0,dir.z);
            
            // so we dont go sideways/turn when we dont want to
            if (dir.x < 0.1f && dir.x > -0.1f) return;
            
            // setting turn speed
            float step = rotateSpeed * Time.deltaTime;

            // actual rotation
            Vector3 newDir = Vector3.RotateTowards(_myModel.transform.forward, dir, step, 0);
            _myModel.transform.rotation = Quaternion.LookRotation(newDir);

        }
       
    }

}



﻿using UnityEngine;

namespace Harry
{
    
    public class BeeController : MonoBehaviour
    {

        // rotate on turn to have been follow its target 
        // put in a state for disabling input
        
        public enum BeeState { Moving, Stopped, Pollenized }
        public BeeState myState = BeeState.Moving;
        
        public GameObject target;
        
        private Rigidbody _myBody;
        private BeeFlutter _flutter;
        private GameObject _myModel;
        private CheckWhatsAround _whatsAround;
        private Vector3 _velocity;
        public float minDist;
        public float maxDist;
        public Color rayColor = Color.green;

        [Range(0,2)]    public float speedMult = 1;
        [Range(0, 5)]   public float windSpeedClamp;
        [Range(0, 5)]   public float windSpeedMult = 2;
        [Range(0,10)]    public float rotateSpeed = 2;
        public float maxSpeed = 5;

        private void Awake()
        {
            // setup
            _myBody = GetComponent<Rigidbody>();
            _flutter = GetComponent<BeeFlutter>();
            _myModel = GetComponentInChildren<Renderer>().gameObject;
            _whatsAround = GetComponent<CheckWhatsAround>();
        }

        private void Update()
        {
            //RayCastDistanceCheck();
        }

        private void FixedUpdate()
        {
            // if we're stopped do nothing
            if (myState == BeeState.Stopped) return;
            
            RotateTowards(target.transform.position);
            Debug.DrawLine(_myModel.transform.position, _myModel.transform.position + _myModel.transform.forward * 4, Color.cyan);
            
            // setting desired velocity to be towards the target
            _velocity = ((target.transform.position - transform.position) * speedMult);
            // applying sin variation and the wind effect
            _velocity = new Vector3(_velocity.x - (Roo.WindScript.windSpeed * windSpeedMult), _velocity.y + _flutter.InputSin(), _velocity.z);
            
            // adding the force normalized
            _myBody.AddRelativeForce(_velocity);

            // adding drag to slow us and clamping speed
            _myBody.velocity = Vector3.Lerp(_myBody.velocity, Vector3.zero, 0.01f);
            _myBody.velocity = new Vector3(Mathf.Clamp(_myBody.velocity.x, -maxSpeed - (Roo.WindScript.windSpeed * windSpeedClamp), maxSpeed), Mathf.Clamp(_myBody.velocity.y, -maxSpeed, maxSpeed), Mathf.Clamp(_myBody.velocity.z, -maxSpeed, maxSpeed));
        }
        
        public void RotateTowards(Vector3 t)
        {
            // finding dir to turn towards
            Vector3 dir = t - _myModel.transform.position;
            // so we dont go upside down
            dir = new Vector3(dir.x, 0,0);
            
            // so we dont go sideways/turn when we dont want to
            if (dir.x < 0.1f && dir.x > -0.1f) return;
            
            // setting turn speed
            float step = rotateSpeed * Time.deltaTime;

            // actual rotation
            Vector3 newDir = Vector3.RotateTowards(_myModel.transform.forward, dir, step, 0);
            _myModel.transform.rotation = Quaternion.LookRotation(newDir);

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
                        rayColor = Color.red;
                        //move character away from close object
                    }
                    else if (distance >= maxDist)
                    {
                        Debug.Log("I need to move closer to" + bees);
                        rayColor = Color.blue;
                        //move closer to object
                    }
                }
            }
        }
    }

}

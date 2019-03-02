using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Harry
{
    
    public class BeeController : MonoBehaviour
    {

        // rotate on turn to have been follow its target 
        // put in a state for disabling input
        
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
        [Range(0, 10)]     public float windSpeedClamp;
        [Range(1, 10)]     public float windSpeedMult = 2;
        public float rotateThreshold = 0.1f;
        [Range(0,4)]    public float rotateSpeed = 2;
        public float maxSpeed = 5;

        private void Awake()
        {
            _myBody = GetComponent<Rigidbody>();
            _flutter = GetComponent<BeeFlutter>();
            _myModel = GetComponentInChildren<Renderer>().gameObject;
            _whatsAround = GetComponent<CheckWhatsAround>();
        }

        private void Update()
        {
            RayCastDistanceCheck();
        }

        private void FixedUpdate()
        {
            
            
            
            _velocity = ((target.transform.position - transform.position) * speedMult);
            Debug.Log(_velocity);
            RotateBee(_velocity);
            _velocity = new Vector3(_velocity.x - (Roo.WindScript.windSpeed / windSpeedMult), _velocity.y + _flutter.InputSin(), _velocity.z);
            
            _myBody.AddRelativeForce(_velocity);

            _myBody.velocity = Vector3.Lerp(_myBody.velocity, Vector3.zero, 0.01f);
            _myBody.velocity = new Vector3(Mathf.Clamp(_myBody.velocity.x, -maxSpeed - (Roo.WindScript.windSpeed / windSpeedClamp), maxSpeed), Mathf.Clamp(_myBody.velocity.y, -maxSpeed, maxSpeed), Mathf.Clamp(_myBody.velocity.z, -maxSpeed, maxSpeed));
        }

        
        // STILL BROKEN, NEEDS WORK
        public void RotateBee(Vector3 v)
        {
            //Debug.Log(transform.forward);
            if (v.x < -rotateThreshold && transform.forward.z < 0)
            {
                _myModel.transform.Rotate(0,rotateSpeed,0);
            } 
            else if (v.x > rotateThreshold && transform.forward.z > 0)
            {
                _myModel.transform.Rotate(0,-rotateSpeed,0);
            }
            
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

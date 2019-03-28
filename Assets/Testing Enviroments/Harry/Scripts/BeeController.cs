using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Harry
{
    
    public class BeeController : MonoBehaviour
    {

        // rotate on turn to have been follow its target 
        // put in a state for disabling input
        
        public GameObject target;
        
        
        [HideInInspector] public Rigidbody _myBody;
        [HideInInspector] public BeeFlutter _flutter;
        [HideInInspector] public GameObject _myModel;
        [HideInInspector] public CheckWhatsAround _whatsAround;
        [HideInInspector] public Vector3 _force;

        public Interactable currentInteractable;

        [Range(0,5)] public float speedMult = 1;
        [Range(0, 5)] public float windSpeedClamp;
        [Range(0, 5)] public float windSpeedMult = 2;
        public float rotateThreshold = 0.1f;
        [Range(0,4)] public float rotateSpeed = 2;
        public float maxSpeed = 5;
        [Range(0, 1)] public float windYAxisDivider;
        public Text finalWindSpeed;
        public Text playerState;

        public enum BeeState { Moving, Stopped, Pollenated }
        public BeeState myState = BeeState.Moving;
        
        
        public virtual void Awake()
        {
            _myBody = GetComponent<Rigidbody>();
            _flutter = GetComponent<BeeFlutter>();
            _myModel = GetComponentInChildren<Renderer>().gameObject;
            _whatsAround = GetComponent<CheckWhatsAround>();
        }
        
        

        public virtual void FixedUpdate()
        {
            RotateTowards(target.transform.position);
            Debug.DrawLine(_myModel.transform.position, _myModel.transform.position + _myModel.transform.forward * 4, Color.cyan);
            
            // setting desired velocity to be towards the target
            _force = ((target.transform.position - transform.position) * speedMult);
            // applying sin variation and the wind effect
            _force = new Vector3(_force.x - (Roo.WindScript.windSpeed * windSpeedMult) - (Roo.WindScript.windSpeed * (_myModel.transform.position.y * windYAxisDivider)), _force.y + _flutter.InputSin(), _force.z);
            
            // adding the force normalized
            _myBody.AddForce(_force);

            // adding drag to slow us and clamping speed
            _myBody.velocity = Vector3.Lerp(_myBody.velocity, Vector3.zero, 0.01f);
            _myBody.velocity = new Vector3(Mathf.Clamp(_myBody.velocity.x, -maxSpeed - (Roo.WindScript.windSpeed * windSpeedClamp), maxSpeed), Mathf.Clamp(_myBody.velocity.y, -maxSpeed, maxSpeed), Mathf.Clamp(_myBody.velocity.z, -maxSpeed, maxSpeed));
 
            if (finalWindSpeed != null)
                finalWindSpeed.text = System.Math.Round((Roo.WindScript.windSpeed * windSpeedMult) + (Roo.WindScript.windSpeed * (_myModel.transform.position.y / windYAxisDivider)),2).ToString();
        }
        
        public virtual void RotateTowards(Vector3 t)
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

    }

}

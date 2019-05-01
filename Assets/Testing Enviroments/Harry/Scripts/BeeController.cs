using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
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
        
        // Modifiers for Y wind application
        [Range(0, 1)] public float windYAxisDivider;
        private float _yAxisWindMod = 0;
        [Range(0, 10)] public float yAxisMinWindEffect = 0;
        [Range(-3, 3)] public float yAxisWindCutOff = 0;
        
        public Text finalWindSpeed;
        public Text playerState;
        
        
        
        

        public enum BeeState { Moving, Stopped, Pollenated }
        public BeeState myState = BeeState.Moving;

        public bool interacting = false;
        
        public virtual void Awake()
        {
            _myBody = GetComponent<Rigidbody>();
            _flutter = GetComponent<BeeFlutter>();
            _myModel = GetComponentInChildren<Renderer>().gameObject;
            _whatsAround = GetComponent<CheckWhatsAround>();
        }

        public virtual void FixedUpdate()
        {
            float _windspeed = Roo.WindScript.windSpeed;
            if (!GameManagerScript._isGameRunning) _windspeed = 0f;
            RotateTowards(target.transform.position);
            Debug.DrawLine(_myModel.transform.position, _myModel.transform.position + _myModel.transform.forward * 4, Color.cyan);
            
            // setting desired velocity to be towards the target
            _force = ((target.transform.position - transform.position) * speedMult);
            // applying sin variation and the wind effect
            
            // wind based on y axis
            if (_myModel.transform.position.y > yAxisWindCutOff)
                _yAxisWindMod = _windspeed * (_myModel.transform.position.y * windYAxisDivider);
            else
                _yAxisWindMod = yAxisMinWindEffect * windYAxisDivider;
            
            _force = new Vector3(_force.x - (_windspeed * windSpeedMult) - _yAxisWindMod, _force.y + _flutter.InputSin(), _force.z);
            
            // adding the force normalized
            _myBody.AddForce(_force);

            // adding drag to slow us and clamping speed
            _myBody.velocity = Vector3.Lerp(_myBody.velocity, Vector3.zero, 0.01f);
            _myBody.velocity = new Vector3(Mathf.Clamp(_myBody.velocity.x, -maxSpeed - (_windspeed * windSpeedClamp), maxSpeed), Mathf.Clamp(_myBody.velocity.y, -maxSpeed, maxSpeed), Mathf.Clamp(_myBody.velocity.z, -maxSpeed, maxSpeed));
 
            if (finalWindSpeed != null)
                finalWindSpeed.text = System.Math.Round((_windspeed * windSpeedMult) + (_windspeed * (_myModel.transform.position.y / windYAxisDivider)),2).ToString();
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

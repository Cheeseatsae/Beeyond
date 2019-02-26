using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Harry;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Vector3 = UnityEngine.Vector3;

namespace Harry
{
    
    public class PlayerController : MonoBehaviour
    {
        private GameObject _beeChild;
        private Rigidbody _beeBody;

        public string inputHorizontal, inputVertical;
        public float speedMult = 0.05f;
        public float maxSpeed;

        private int _inputHorizontalValue 
        { 
            get
            {
                if (Input.GetAxisRaw(inputHorizontal) < -0.1f) return -1;
                else if (Input.GetAxisRaw(inputHorizontal) > -0.1f && Input.GetAxisRaw(inputHorizontal) < 0.1f) return 0;
                else return 1;
            } 
        }
        
        private int _inputVerticalValue 
        { 
            get
            {
                if (Input.GetAxisRaw(inputVertical) < -0.1f) return -1;
                else if (Input.GetAxisRaw(inputVertical) > -0.1f && Input.GetAxisRaw(inputVertical) < 0.1f) return 0;
                else return 1;
            } 
        }
        
        private void Awake()
        {
            _beeChild = GetComponentInChildren<BeeFlutter>().gameObject;
            _beeBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update() 
        {
            
            transform.position = new Vector3(_beeChild.transform.position.x, transform.position.y, _beeChild.transform.position.z);
            
            
            //Debug.Log("speed is " + _beeBody.velocity);
            Debug.Log("X is " + _inputHorizontalValue + "          Y is " + _inputVerticalValue);
            
            _beeBody.velocity += new Vector3(_inputHorizontalValue * 2, _inputVerticalValue + _beeBody.velocity.y, 0) * speedMult;

            if (_inputHorizontalValue == 0 && _inputVerticalValue == 0)
            {
                _beeBody.velocity = Vector3.Lerp(_beeBody.velocity, Vector3.zero, 0.1f);
            }

        }

        private void FixedUpdate()
        {
            _beeBody.velocity = new Vector3(Mathf.Clamp(_beeBody.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(_beeBody.velocity.y, -maxSpeed, maxSpeed), 0);
        }
    }

}


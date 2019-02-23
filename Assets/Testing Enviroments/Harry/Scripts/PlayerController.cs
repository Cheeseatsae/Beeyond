using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Harry;
using UnityEngine;
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

        private float _inputHorizontalValue { get { return Input.GetAxis(inputHorizontal); } }
        private float _inputVerticalValue { get { return Input.GetAxis(inputVertical); } } 

        private void Awake()
        {
            _beeChild = GetComponentInChildren<BeeFlutter>().gameObject;
            _beeBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            
            transform.position = new Vector3(_beeChild.transform.position.x, transform.position.y, _beeChild.transform.position.z);

        
            Debug.Log("speed is " + _beeBody.velocity);
            _beeBody.velocity -= new Vector3(_inputHorizontalValue, _inputVerticalValue + _beeBody.velocity.y, 0) * speedMult;

            _beeBody.velocity = Vector3.ClampMagnitude(_beeBody.velocity, maxSpeed);

        }
    }

}


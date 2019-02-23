using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Harry
{
    public class BeeFlutter : MonoBehaviour
    {
        [Range(0,1)]
        public float speed;
        [Range(0,1)]
        public float amplitude;
        [Range(0,2)]
        public float distance;
        [Range(0,2)]
        public float amount;

        public GameObject parent;
        
        private float _sinTime;

        public float floatingRange;

        private Rigidbody _myBody;
        private float _yValue;
        private bool _zeroed = true;

        private void Awake()
        {
            _myBody = GetComponent<Rigidbody>();
            _sinTime = Random.Range(0, 1f);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // sin(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distance * _Amount;
    
            _sinTime += Time.deltaTime;
            
            _yValue = Mathf.Sin(_sinTime * speed + transform.position.y * amplitude) * distance * amount;
    
                if (Vector3.Distance(transform.position, parent.transform.position) > floatingRange)     
                    _zeroed = false;
                
                if (_zeroed) 
                    _myBody.velocity = new Vector3(0, _yValue, 0);
                else
                {
                    _myBody.velocity = new Vector3(0, parent.transform.position.y - transform.position.y, 0) ;
    
                    if (Vector3.Distance(transform.position, parent.transform.position) < 0.02f)
                    {
                        _sinTime = 0;
                        _zeroed = true;
                    } 
                        
                }
    
            }
        
    }

}


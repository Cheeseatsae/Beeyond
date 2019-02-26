using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Harry
{
    public class BeeFlutter : MonoBehaviour
    {
        [Range(0,1)]
        public float speed = 1;
        [Range(0,1)]
        public float amplitude = 0.6f;
        [Range(0,2)]
        public float distance = 0.5f;
        [Range(0,2)]
        public float amount = 0.5f;
        
        private float _sinTime;
        private float _yValue;

        private void Awake()
        {
            _sinTime = Random.Range(0f, 1f);
        }
        
        // Update is called once per frame
        void FixedUpdate()
        {
            // sin(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distance * _Amount;
            _sinTime += Time.deltaTime;
    
        }

        public float InputSin()
        {
            _yValue = Mathf.Sin(_sinTime * speed + transform.position.y * amplitude) * distance * amount;
            return _yValue;
        }
        
        public void ResetSin()
        {
            _sinTime = 0;
        }
        
    }
    
}


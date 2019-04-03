using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{
    
    public class ButterflyController : MonoBehaviour
    {
        public float rotateSpeed = 2;
        private GameObject _myModel;

        private Vector3 movePos;
        private Vector3 lookTarget;

        private void Awake()
        {
            _myModel = GetComponentInChildren<Renderer>().gameObject;
        }

        void Update()
        {
        
        }

        private void FindNextWaypoint()
        {
            
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



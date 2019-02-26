using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Harry
{
    public class BeeTargetController : MonoBehaviour
    {
        
        // 
        public string inputHorizontal, inputVertical;
        [Range(0,3)]
        public float inputMult = 1;

        public Transform parent;

        // horizontal input property
        private float InputHorizontalValue 
        { 
            get
            {
                return Input.GetAxis(inputHorizontal);
//                if (Input.GetAxisRaw(inputHorizontal) < -0.1f) return -1;
//                else if (Input.GetAxisRaw(inputHorizontal) > -0.1f && Input.GetAxisRaw(inputHorizontal) < 0.1f) return 0;
//                else return 1;
            } 
        }
        
        // vertical input property
        private float InputVerticalValue 
        { 
            get
            {
                return Input.GetAxis(inputVertical);
//                if (Input.GetAxisRaw(inputVertical) < -0.1f) return -1;
//                else if (Input.GetAxisRaw(inputVertical) > -0.1f && Input.GetAxisRaw(inputVertical) < 0.1f) return 0;
//                else return 1;
            } 
        }
        
        void Update() 
        {
            // sets position locally for bee to chase
            transform.position = new Vector3(InputHorizontalValue * inputMult, InputVerticalValue * inputMult, 0) + parent.position;
            // Debug.Log("X is " + InputHorizontalValue + "          Y is " + InputVerticalValue);
        }
        
    }

}



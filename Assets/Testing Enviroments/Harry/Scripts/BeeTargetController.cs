using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Harry
{
    public class BeeTargetController : MonoBehaviour
    {
        
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
            } 
        }
        
        // vertical input property
        private float InputVerticalValue 
        { 
            get
            {
                return Input.GetAxis(inputVertical);
                
            } 
        }
        
        void Update() 
        {
            // sets position locally for bee to chase
            transform.position = new Vector3(InputHorizontalValue * inputMult, InputVerticalValue * inputMult, 0) + parent.position;
            parent.position = new Vector3(parent.position.x, parent.position.y,  Mathf.Lerp(parent.position.z, 0, 0.05f));
        }
        
    }

}



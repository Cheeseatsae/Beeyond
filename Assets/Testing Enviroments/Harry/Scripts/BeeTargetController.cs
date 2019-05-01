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

        public GameObject parent;

        public static float BuzzingVolume;

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
            float _inputHorizontal = InputHorizontalValue;
            float _inputVertical = InputVerticalValue;

            if (!GameManagerScript._isGameRunning) { _inputHorizontal = 0f; _inputVertical = 0f; }

            // sets position locally for bee to chase
                transform.position = new Vector3(_inputHorizontal * inputMult, _inputVertical * inputMult, 0) + parent.transform.position;
                parent.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y, Mathf.Lerp(parent.transform.position.z, 0, 0.05f));
                

            // sets float for buzzing volume of bee
            BuzzingVolume = Mathf.Max(_inputHorizontal, _inputVertical);
            if (BuzzingVolume < 0.1f) BuzzingVolume = 0.1f;
        }
        
    }

}



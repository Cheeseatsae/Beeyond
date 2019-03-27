using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject ObjectToBreak;// object that will have joint broken
    [Range(0,1)] public int hingeJointToBreak = 0;
    
    private HingeJoint[] _hinges;
    
    // Start is called before the first frame update
    void Start()
    {
        _hinges = ObjectToBreak.GetComponents<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }



        if (Input.GetKey(KeyCode.Alpha1))
        {
            
            Destroy(_hinges[hingeJointToBreak]); // destroy the joint of that object
        }
    }

}

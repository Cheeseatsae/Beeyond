﻿using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject ObjectToBreak;// object that will have joint broken
    [Range(0,1)] public int hingeJointToBreak = 0;
    
    private HingeJoint[] _hinges;

    public GameObject ObjectToDestroy;
    public GameObject GameCam;

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
            Destroy(ObjectToDestroy); // destroy the wall
            GameCam.GetComponent<Roo.CameraMovementScript>().openGate += 1; // increase the clamp
        }
    }

}

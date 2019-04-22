﻿using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject ObjectToBreak;// object that will have joint broken
    [Range(0,1)] public int hingeJointToBreak = 0;
    
    private HingeJoint[] _hinges;

    public GameObject ObjectToDestroy;
    public GameObject GameCam;

    public GameObject GamePanel;
    public GameObject StartMenu;

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

        if (Input.GetKey(KeyCode.Space))
        {
            GamePanel.SetActive(false);
            Roo.CameraMovementScript.cameraClampMinY = 1.5f;
            AudioManagerScript.Playsound("music");
        }

    }

}

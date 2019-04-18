using System.Collections;
using System.Collections.Generic;
using Harry;
using Roo;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class SwingTrigger : MonoBehaviour
{
    private FlowerInteraction _myFlower;
    public HingeJoint hingeToBreak;
    public GameObject wallToDestroy;
    public CameraMovementScript cam;

    private void Awake()
    {
        _myFlower = GetComponent<FlowerInteraction>();
        _myFlower.InteractionEvent += TriggerSwingEvent;
    }

    public void TriggerSwingEvent()
    {
        Destroy(hingeToBreak); // destroy the joint of that object
        Destroy(wallToDestroy);
        // AudioManagerScript.gameProgression += 1; // progress the score of the game
        cam.IncreaseClamp(0);
    }

    private void OnDestroy()
    {
        _myFlower.InteractionEvent -= TriggerSwingEvent;
    }
}

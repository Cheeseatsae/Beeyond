using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject ObjectToBreak;// object that will have joint broken
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            
            Destroy(ObjectToBreak.GetComponent<CharacterJoint>()); // destroy the joint of that object
        }
    }

}

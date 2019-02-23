﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WindScript : MonoBehaviour
{

    public static float windSpeed = 1f; // eventual global wind speed


    float _pingpongRange = 2f; //size of pingpong
    float _pingpongSpeed = 1f; // speed / rate of pingpong

// following texts are for live debugging
    public Text windSpeedText;
    public Text pingpongRangeText;
    public Text pingpongSpeedText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate ()
    {

        windSpeed = Mathf.PingPong(Time.time*_pingpongSpeed, _pingpongRange); // get next pingpong value
      
        if(windSpeed< 0.1f)  // if at the start of the pingpong. set new random values
        {    
            _pingpongRange = GetRandomValue(0f); //get value for pingpong
            _pingpongSpeed = GetRandomValue(_pingpongRange); //use previous value to get weighted speed value
        }
        
       

        
        // set screen texts for debugging
        windSpeedText.text = "wind speed " + windSpeed.ToString();
        pingpongRangeText.text = "PP Range " + _pingpongRange.ToString();
        pingpongSpeedText.text = "pp Speed " + _pingpongSpeed.ToString();
        
    }
    
    // gets a random (weighted) float
    float GetRandomValue(float rand) {
        if(rand == 0f){ rand = Random.Range(0f,10f);}
        if (rand <= 6f)
            return Random.Range(1f, 3f);
        if (rand <= 8f)
            return Random.Range(3f, 6f);
 
        return Random.Range(7f, 10f);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WindScript : MonoBehaviour
{

    public static float windSpeed = 1f; // eventual global wind speed


    float _pingpongRange = 2f; //size of pingpong
    float _pingpongSpeed = 1f; // speed / rate of pingpong
    
    /*
     * the following ranges are used for weighted random variables
     * weight 1 can be in a group size up to 60%
     * weight 2 can be up to 95% - weight 1 size
     * weight 3 is what remains after 1 and 2 have been set
     *
     * upper and lower range is the pool from which the random number is generated
     *
     *  -= these values need to be set in order for the script to work =-
    */
    
    [Range(1f,6f)] public float weight1;

    [Range(1f, 10f)] public float weight1LowerRange;
    [Range(1f, 10f)] public float weight1UpperRange;
    
    [Range(6f,9.5f)] public float weight2;
    [Range(1f, 10f)] public float weight2LowerRange;
    [Range(1f, 10f)] public float weight2UpperRange;
    
    [Range(1f, 10f)] public float weight3LowerRange;
    [Range(1f, 10f)] public float weight3UpperRange;
    
    

// following texts are for live debugging
    public Text windSpeedText;
    public Text pingpongRangeText;
    public Text pingpongSpeedText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {

        windSpeed = Mathf.PingPong(Time.time*_pingpongSpeed, _pingpongRange); // get next pingpong value
      
        if(windSpeed< 0.03f)  // if at the start of the pingpong. set new random values
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
        if (rand <= weight1)
            return Random.Range(weight1LowerRange, weight1UpperRange);
        if (rand <= weight2)
            return Random.Range(weight2LowerRange, weight2UpperRange);
 
        return Random.Range(weight3LowerRange, weight3UpperRange);
    }
}

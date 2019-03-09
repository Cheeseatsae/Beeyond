using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindDebugPanel : MonoBehaviour
{
    
    public GameObject _debugPanel; // canvas that can be switched on and off
    
    
    // following texts are for live debugging
    public Slider windSpeedSlider;
    
    public Text pingpongRangeText;
    public Text pingpongSpeedText;
    public Text windSpeedText;
    
    
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        // used for toggling audio canvas on and off
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (_debugPanel.activeSelf)
            {
                _debugPanel.SetActive(false);
            }
           else
           {
               _debugPanel.SetActive(true);
           }
        }
        
        
        // set screen texts for debugging
        windSpeedSlider.value = Roo.WindScript.windSpeed;
        windSpeedText.text = System.Math.Round(Roo.WindScript.windSpeed,2).ToString();
        pingpongRangeText.text = System.Math.Round(Roo.WindScript.pingpongRange,2).ToString();
        pingpongSpeedText.text = System.Math.Round(Roo.WindScript.pingpongSpeed,2).ToString();
        
    }
}

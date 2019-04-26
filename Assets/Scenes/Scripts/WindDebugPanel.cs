using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindDebugPanel : MonoBehaviour
{
    
    public GameObject _debugPanel; // canvas that can be switched on and off
    
    
    // following texts are for live debugging
    public Slider windSpeedSlider;
    public Image fill;
    
    // public Text pingpongRangeText;
    public Text timerText;
    private float _startTime;

    public Text windSpeedText;
    public Text gameProgress;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // used for toggling audio canvas on and off
   /*     if (Input.GetKeyDown(KeyCode.T))
        {
            if (_debugPanel.activeSelf)
            
                _debugPanel.SetActive(false);
            
           else
           {
               _debugPanel.SetActive(true);
           }
        } */

        // get time values in mins and sec
        float t = Time.time - _startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f0");

   
        // set values in panel for debugging
        windSpeedSlider.value = Roo.WindScript.windSpeed;
        fill.color = Color.Lerp(Color.green, Color.red, Roo.WindScript.windSpeed / 10f); // slider chnges colour depending on value
        windSpeedText.text = System.Math.Round(Roo.WindScript.windSpeed,2).ToString(); // output windspeed
        timerText.text = minutes + ":" + seconds; // output time as string
        gameProgress.text = AudioManagerScript.gameProgression.ToString();

        //pingpongRangeText.text = System.Math.Round(Roo.WindScript.pingpongRange,2).ToString();
        //pingpongSpeedText.text = System.Math.Round(Roo.WindScript.pingpongSpeed,2).ToString();

    }
}

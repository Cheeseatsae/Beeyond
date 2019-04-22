﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    // List all possible game sounds
    public static FMOD.Studio.EventInstance wind; // Game sounds from SFX bank
    public static FMOD.Studio.EventInstance music;// game music
    public static FMOD.Studio.EventInstance atmosExploring;

    public static FMOD.Studio.EventInstance thunder01, thunder02, thunder03, thunder04, thunder05;

    public static float gameProgression = 0f;

    /*
     *  BELOW IS A SAMPLE, CHANGE FOR CORRECT NAMES
     *  
    public static FMOD.Studio.EventInstance score1, score2, score3; // Game sounds from Score bank
    public static FMOD.Studio.EventInstance atmos1, atmos2, atmos3; // Game sounds from Atmos bank
    */


    // name the parameters for wind variables
    public static FMOD.Studio.ParameterInstance WindIntensity; // name the parameter for wind intensity
    public static FMOD.Studio.ParameterInstance WindVol; // name the parameter for wind Volume

    // name the parameters for music variables
    public static FMOD.Studio.ParameterInstance gameLevel;
    public static FMOD.Studio.ParameterInstance musicVol;




    // Keep music rolling between scenes. Will not destroy onload unless there is a double.
    static AudioManagerScript instance = null;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // below we connect to all sounds in FMOD for the game

        // sfx bank
        wind = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Wind");
        thunder01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Thunder01");
        thunder02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Thunder02");
        thunder03 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Thunder03");
        thunder04 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Thunder04");
        thunder05 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Thunder05");

        // music
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Score/Music");

        // atmos
        atmosExploring = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Exploring");


        // connect to sound parameters

        wind.getParameter("WindIntensity", out WindIntensity); // connect to WindIntensity Parameter in Wind Sound
        wind.getParameter("Strength", out WindVol); // connect to Strength Parameter in Wind Sound and output WindVol
        music.getParameter("GameLevel", out gameLevel);

       wind.start(); // start the wind
       atmosExploring.start(); // start exploring atmos
        // WindVol.setValue(1); // set volume to max - no longer required. volume is automated
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WindIntensity.setValue(Roo.WindScript.windSpeed);
        gameLevel.setValue(gameProgression);
    }

    // Below is the switch statement for all the possible sounds used in the game
    public static void Playsound(string clip)
    {
        switch (clip) { case ("thunder01"): thunder01.start(); break; }
        switch (clip) { case ("thunder02"): thunder02.start(); break; }
        switch (clip) { case ("thunder03"): thunder03.start(); break; }
        switch (clip) { case ("thunder04"): thunder04.start(); break; }
        switch (clip) { case ("thunder05"): thunder05.start(); break; }

        switch (clip) { case ("music"): music.start(); break; }

        switch (clip) { case ("atmosExploringStop"): atmosExploring.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); break; }

        //switch (clip) { case ("noWind"): WindIntensity.setValue(0); break; }
    }

}

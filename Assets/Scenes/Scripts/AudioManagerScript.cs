using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{    /***************************************************************************************************
     *                          -= !!! ATTENTION CODERS OF BEEYOND !!!=-                                *     
     *                                                                                                  *
     *       with this script loaded, all you need to do to play a sound is to use the code below...    *
     *   you can call this from any script on any object without requiring to reference anything else   *
     *                                                                                                  *
     *                      AudioMasterScript.Playsound("NAME OF SOUND HERE");                          *
     *                                                                                                  *
     *                      just follow the code below to add the sounds from hte bank                  *
     *                      first list the sounds,                                                      *
     *                      then connect to the sounds in the bank                                      *
     *                      then add to the PlaySound() function                                        *
     *                                                                                                  *
     *                                                                                                  *
     *                                                                                                  *
     *                                       Enjoy, Roo :)                                              *
     *                                                                                                  *
     ***************************************************************************************************/


    // List all possible game sounds
    public static FMOD.Studio.EventInstance wind; // Game sounds from SFX bank
    public static FMOD.Studio.EventInstance music;// game music
    public static FMOD.Studio.EventInstance atmosExploring, atmosStruggle; //atmos sounds
    public static FMOD.Studio.EventInstance BeeButtDance01, BeeButtDance02, BeeButtDance03; // all bee dance sounds
    public static FMOD.Studio.EventInstance thunder01, thunder02, thunder03, thunder04, thunder05; // all thundersounds
    public static FMOD.Studio.EventInstance BeeBuzzIdleMedium, BeeBuzzIdleSoft, BeeBuzzIntense; // all bee buzz sounds
    public static FMOD.Studio.EventInstance BeeCollisionAmended, BeeCollisionMetal01, BeeCollisionMetal02, BeeCollisionMetal03, BeePlasticCollision, BeeWoodCollision01, BeeWoodCollision02; // all bee collision sounds
    public static FMOD.Studio.EventInstance BeeDigQuickLong, BeeDigQuickShort01, BeeDigQuickShort02, BeeDigSlowLong, BeeDigSlowShort; // all bee dig sounds
    public static FMOD.Studio.EventInstance BeeLanding01; // bee landing sound
    public static FMOD.Studio.EventInstance FarmAnimalsChickens01, FarmAnimalsCow01, FarmAnimalsSheep01, FarmAnimalsSheep02; // all farm animal sounds
    public static FMOD.Studio.EventInstance FlowerToneHigh01, FlowerToneHigh02, FlowerToneLow01, FlowerToneLow02, FlowerToneMid01, FlowerToneMid02; // all flower tone sounds
    public static FMOD.Studio.EventInstance GateCreak01, GateCreak02, GateCreak03, GateCreak04, GateLatchHigh, GateLatchOpening01, GateLatchOpening02, GateLatchOpening03, GateLatchOpening04, GateSlam01, GateSlam02; // all gate sounds
    public static FMOD.Studio.EventInstance HeavyObjectFallingGrass01, HeavyObjectFallingGrass02, HeavyObjectFallingGrass03; //all heavy object falling sounds
    public static FMOD.Studio.EventInstance InsectCallsCicadas, InsectCallsCrickets; // all insect sounds
    public static FMOD.Studio.EventInstance Kite, KiteFlap01, KiteFlap02, KiteTailCollision; // all kite sounds
    public static FMOD.Studio.EventInstance LightWindLoopFinal, WindMediumLongConstant, WindMediumLongPulsing, WindMediumMedium, WindMediumMediumConstant01, WindMediumMediumConstant02, WindMediumMediumPulsing, WindMediumShort, WindMediumShortBurst, WindMediumShortConstant, WindMediumShortPulsing, WindSoftLongPulsing, WindSoftMediumConstant01, WindSoftMediumConstant02, WindSoftMediumPulsing, WindSoftShortBurst, WindStrongLongConstant01, WindStrongLongConstant02, WindStrongLongConstant03, WindStrongLongConstant04, WindStrongLoop2, WindStrongMediumConstant, WindStrongMediumPulsing01, WindStrongMediumPulsing02, WindStrongShort, WindStrongShortBurst01, WindStrongShortBurst02, WindStrongShortConstant01, WindStrongShortConstant02, WindStrongShortPulsing; // all the extra wind sound loops
    public static FMOD.Studio.EventInstance LongGrassRustle, TwigsSnapping; // all environmental occurence sounds
    public static FMOD.Studio.EventInstance MetalChainClunky, MetalChainMediumThickness, MetalChainMediumThickness02, MetalChainSnapping01, MetalChainSnapping02, MetalChainThin; // all chain sounds


    public static float gameProgression = 0f;

    /*
     *  BELOW IS A SAMPLE, CHANGE FOR CORRECT NAMES
     *  
    public static FMOD.Studio.EventInstance score1, score2, score3; // Game sounds from Score bank
    public static FMOD.Studio.EventInstance atmos1, atmos2, atmos3; // Game sounds from Atmos bank
    */

    public FMOD.Studio.EventInstance Exploring, Struggle; // Game sounds from atmos bank


    // name the parameters for wind variables
    public static FMOD.Studio.ParameterInstance WindIntensity; // name the parameter for wind intensity
    public static FMOD.Studio.ParameterInstance WindVol; // name the parameter for wind Volume

    // name the parameters for music variables
    public static FMOD.Studio.ParameterInstance gameLevel;
    public static FMOD.Studio.ParameterInstance musicVol;

    // name the parameters for bee movement variables
    public static FMOD.Studio.ParameterInstance BeeMovement; // name the parameter for bee movement




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
        BeeButtDance01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeButtDance01");
        BeeButtDance02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeButtDance02");
        BeeButtDance03 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeButtDance03");
        BeeBuzzIdleMedium = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeBuzzIdleMedium");
        BeeBuzzIdleSoft = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeBuzzIdleSoft");
        BeeBuzzIntense = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeBuzzIntense");
        BeeCollisionAmended = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeCollisionAmended");
        BeeCollisionMetal01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeCollisionMetal01");
        BeeCollisionMetal02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeCollisionMetal02");
        BeeCollisionMetal03 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeCollisionMetal03");
        BeeDigQuickLong = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeDigQuickLong");
        BeeDigQuickShort01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeDigQuickShort01");
        BeeDigQuickShort02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeDigQuickShort02");
        BeeDigSlowLong = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeDigSlowLong");
        BeeDigSlowShort = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeDigSlowShort");
        BeeLanding01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeLanding01");
        BeePlasticCollision = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeePlasticCollision");
        BeeWoodCollision01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeWoodCollision01");
        BeeWoodCollision02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BeeWoodCollision02");
        FarmAnimalsChickens01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/FarmAnimalsChickens01");
        FarmAnimalsCow01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/FarmAnimalsCow01");
        FarmAnimalsSheep01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/FarmAnimalsSheep01");
        FarmAnimalsSheep02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/FarmAnimalsSheep02");
        FlowerToneHigh01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/FlowerToneHigh01");
        FlowerToneHigh02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/FlowerToneHigh02");
        FlowerToneLow01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/FlowerToneLow01");
        FlowerToneLow02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/FlowerToneLow02");
        FlowerToneMid01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/FlowerToneMid01");
        FlowerToneMid02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/FlowerToneMid02");
        GateCreak01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/GateCreak01");
        GateCreak02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/GateCreak02");
        GateCreak03 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/GateCreak03");
        GateCreak04 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/GateCreak04");
        GateLatchHigh = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/GateLatchHigh");
        GateLatchOpening01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/GateLatchOpening01");
        GateLatchOpening02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/GateLatchOpening02");
        GateLatchOpening03 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/GateLatchOpening03");
        GateLatchOpening04 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/GateLatchOpening04");
        GateSlam01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/GateSlam01");
        GateSlam02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/GateSlam02");
        HeavyObjectFallingGrass01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/HeavyObjectFallingGrass01");
        HeavyObjectFallingGrass02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/HeavyObjectFallingGrass02");
        HeavyObjectFallingGrass03 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/HeavyObjectFallingGrass03");
        InsectCallsCicadas = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/InsectCallsCicadas");
        InsectCallsCrickets = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/InsectCallsCrickets");
        Kite = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Kite");
        KiteFlap01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/KiteFlap01");
        KiteFlap02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/KiteFlap02");
        KiteTailCollision = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/KiteTailCollision");
        LightWindLoopFinal = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/LightWindLoopFinal");
        LongGrassRustle = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/LongGrassRustle");
        MetalChainClunky = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/MetalChainClunky");
        MetalChainMediumThickness = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/MetalChainMediumThickness");
        MetalChainMediumThickness02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/MetalChainMediumThickness02");
        MetalChainSnapping01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/MetalChainSnapping01");
        MetalChainSnapping02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/MetalChainSnapping02");
        MetalChainThin = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/MetalChainThin");
        TwigsSnapping = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/TwigsSnapping");
        WindMediumLongConstant = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindMediumLongConstant");
        WindMediumLongPulsing = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindMediumLongPulsing");
        WindMediumMedium = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindMediumMedium");
        WindMediumMediumConstant01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindMediumMediumConstant01");
        WindMediumMediumConstant02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindMediumMediumConstant02");
        WindMediumMediumPulsing = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindMediumMediumPulsing");
        WindMediumShort = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindMediumShort");
        WindMediumShortBurst = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindMediumShortBurst");
        WindMediumShortConstant = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindMediumShortConstant");
        WindMediumShortPulsing = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindMediumShortPulsing");
        WindSoftLongPulsing = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindSoftLongPulsing");
        WindSoftMediumConstant01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindSoftMediumConstant01");
        WindSoftMediumConstant02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindSoftMediumConstant02");
        WindSoftMediumPulsing = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindSoftMediumPulsing");
        WindSoftShortBurst = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindSoftShortBurst");
        WindStrongLongConstant01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindStrongLongConstant01");
        WindStrongLongConstant02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindStrongLongConstant02");
        WindStrongLongConstant03 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindStrongLongConstant03");
        WindStrongLongConstant04 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindStrongLongConstant04");
        WindStrongLoop2 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindStrongLoop2");
        WindStrongMediumConstant = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindStrongMediumConstant");
        WindStrongMediumPulsing01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindStrongMediumPulsing01");
        WindStrongMediumPulsing02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindStrongMediumPulsing02");
        WindStrongShort = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindStrongShort");
        WindStrongShortBurst01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindStrongShortBurst01");
        WindStrongShortBurst02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindStrongShortBurst02");
        WindStrongShortConstant01 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindStrongShortConstant01");
        WindStrongShortConstant02 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindStrongShortConstant02");
        WindStrongShortPulsing = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WindStrongShortPulsing");
        // music
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Score/Music");

        // atmos
        atmosExploring = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Exploring");
        atmosStruggle = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Struggle");


        // connect to sound parameters

        wind.getParameter("WindIntensity", out WindIntensity); // connect to WindIntensity Parameter in Wind Sound
        wind.getParameter("Strength", out WindVol); // connect to Strength Parameter in Wind Sound and output WindVol
        music.getParameter("GameLevel", out gameLevel);
        BeeBuzzIdleMedium.getParameter("BeeMovement", out BeeMovement);

       wind.start(); // start the wind
       atmosExploring.start(); // start exploring atmos
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WindIntensity.setValue(Roo.WindScript.windSpeed);
        gameLevel.setValue(gameProgression);
        BeeMovement.setValue(Harry.BeeTargetController.BuzzingVolume);
    }

    // Below is the switch statement for all the possible sounds used in the game
    public static void Playsound(string clip)
    {
        switch (clip) { case ("thunder01"): thunder01.start(); break; }
        switch (clip) { case ("thunder02"): thunder02.start(); break; }
        switch (clip) { case ("thunder03"): thunder03.start(); break; }
        switch (clip) { case ("thunder04"): thunder04.start(); break; }
        switch (clip) { case ("thunder05"): thunder05.start(); break; }
        switch (clip) { case ("BeeButtDance01"): BeeButtDance01.start(); break; }
        switch (clip) { case ("BeeButtDance02"): BeeButtDance02.start(); break; }
        switch (clip) { case ("BeeButtDance03"): BeeButtDance03.start(); break; }
        switch (clip) { case ("BeeBuzzIdleMedium"): BeeBuzzIdleMedium.start(); break; }
        switch (clip) { case ("BeeBuzzIdleSoft"): BeeBuzzIdleSoft.start(); break; }
        switch (clip) { case ("BeeBuzzIntense"): BeeBuzzIntense.start(); break; }
        switch (clip) { case ("BeeCollisionAmended"): BeeCollisionAmended.start(); break; }
        switch (clip) { case ("BeeCollisionMetal01"): BeeCollisionMetal01.start(); break; }
        switch (clip) { case ("BeeCollisionMetal02"): BeeCollisionMetal02.start(); break; }
        switch (clip) { case ("BeeCollisionMetal03"): BeeCollisionMetal03.start(); break; }
        switch (clip) { case ("BeeDigQuickLong"): BeeDigQuickLong.start(); break; }
        switch (clip) { case ("BeeDigQuickShort01"): BeeDigQuickShort01.start(); break; }
        switch (clip) { case ("BeeDigQuickShort02"): BeeDigQuickShort02.start(); break; }
        switch (clip) { case ("BeeDigSlowLong"): BeeDigSlowLong.start(); break; }
        switch (clip) { case ("BeeDigSlowShort"): BeeDigSlowShort.start(); break; }
        switch (clip) { case ("BeeLanding01"): BeeLanding01.start(); break; }
        switch (clip) { case ("BeePlasticCollision"): BeePlasticCollision.start(); break; }
        switch (clip) { case ("BeeWoodCollision01"): BeeWoodCollision01.start(); break; }
        switch (clip) { case ("BeeWoodCollision02"): BeeWoodCollision02.start(); break; }
        switch (clip) { case ("FarmAnimalsChickens01"): FarmAnimalsChickens01.start(); break; }
        switch (clip) { case ("FarmAnimalsCow01"): FarmAnimalsCow01.start(); break; }
        switch (clip) { case ("FarmAnimalsSheep01"): FarmAnimalsSheep01.start(); break; }
        switch (clip) { case ("FarmAnimalsSheep02"): FarmAnimalsSheep02.start(); break; }
        switch (clip) { case ("FlowerToneHigh01"): FlowerToneHigh01.start(); break; }
        switch (clip) { case ("FlowerToneHigh02"): FlowerToneHigh02.start(); break; }
        switch (clip) { case ("FlowerToneLow01"): FlowerToneLow01.start(); break; }
        switch (clip) { case ("FlowerToneLow02"): FlowerToneLow02.start(); break; }
        switch (clip) { case ("FlowerToneMid01"): FlowerToneMid01.start(); break; }
        switch (clip) { case ("FlowerToneMid02"): FlowerToneMid02.start(); break; }
        switch (clip) { case ("GateCreak01"): GateCreak01.start(); break; }
        switch (clip) { case ("GateCreak02"): GateCreak02.start(); break; }
        switch (clip) { case ("GateCreak03"): GateCreak03.start(); break; }
        switch (clip) { case ("GateCreak04"): GateCreak04.start(); break; }
        switch (clip) { case ("GateLatchHigh"): GateLatchHigh.start(); break; }
        switch (clip) { case ("GateLatchOpening01"): GateLatchOpening01.start(); break; }
        switch (clip) { case ("GateLatchOpening02"): GateLatchOpening02.start(); break; }
        switch (clip) { case ("GateLatchOpening03"): GateLatchOpening03.start(); break; }
        switch (clip) { case ("GateLatchOpening04"): GateLatchOpening04.start(); break; }
        switch (clip) { case ("GateSlam01"): GateSlam01.start(); break; }
        switch (clip) { case ("GateSlam02"): GateSlam02.start(); break; }
        switch (clip) { case ("HeavyObjectFallyingGrass01"): HeavyObjectFallingGrass01.start(); break; }
        switch (clip) { case ("HeavyObjectFallyingGrass02"): HeavyObjectFallingGrass02.start(); break; }
        switch (clip) { case ("HeavyObjectFallyingGrass03"): HeavyObjectFallingGrass03.start(); break; }
        switch (clip) { case ("InsectCallsCicadas"): InsectCallsCicadas.start(); break; }
        switch (clip) { case ("InsectCallsCrickets"): InsectCallsCrickets.start(); break; }
        switch (clip) { case ("Kite"): Kite.start(); break; }
        switch (clip) { case ("KiteFlap01"): KiteFlap01.start(); break; }
        switch (clip) { case ("KiteFlap02"): KiteFlap02.start(); break; }
        switch (clip) { case ("KiteTailCollision"): KiteTailCollision.start(); break; }
        switch (clip) { case ("LightWindLoopFinal"): LightWindLoopFinal.start(); break; }
        switch (clip) { case ("LongGrassRustle"): LongGrassRustle.start(); break; }
        switch (clip) { case ("MetalChainClunky"): MetalChainClunky.start(); break; }
        switch (clip) { case ("MetalChainMediumThickness"): MetalChainMediumThickness.start(); break; }
        switch (clip) { case ("MetalChainMediumThickness02"): MetalChainMediumThickness02.start(); break; }
        switch (clip) { case ("MetalChainSnapping01"): MetalChainSnapping01.start(); break; }
        switch (clip) { case ("MetalChainSnapping02"): MetalChainSnapping02.start(); break; }
        switch (clip) { case ("MetalChainThin"): MetalChainThin.start(); break; }
        switch (clip) { case ("TwigsSnapping"): TwigsSnapping.start(); break; }
        switch (clip) { case ("Wind"): wind.start(); break; }
        switch (clip) { case ("WindMediumLongConstant"): WindMediumLongConstant.start(); break; }
        switch (clip) { case ("WindMediumLongPulsing"): WindMediumLongPulsing.start(); break; }
        switch (clip) { case ("WindMediumMedium"): WindMediumMedium.start(); break; }
        switch (clip) { case ("WindMediumMediumConstant01"): WindMediumMediumConstant01.start(); break; }
        switch (clip) { case ("WindMediumMediumConstant02"): WindMediumMediumConstant02.start(); break; }
        switch (clip) { case ("WindMediumMediumPulsing"): WindMediumMediumPulsing.start(); break; }
        switch (clip) { case ("WindMediumShort"): WindMediumShort.start(); break; }
        switch (clip) { case ("WindMediumShortBurst"): WindMediumShortBurst.start(); break; }
        switch (clip) { case ("WindMediumShortConstant"): WindMediumShortConstant.start(); break; }
        switch (clip) { case ("WindMediumShortPulsing"): WindMediumShortPulsing.start(); break; }
        switch (clip) { case ("WindSoftLongPulsing"): WindSoftLongPulsing.start(); break; }
        switch (clip) { case ("WindSoftMediumConstant01"): WindSoftMediumConstant01.start(); break; }
        switch (clip) { case ("WindSoftMediumConstant02"): WindSoftMediumConstant02.start(); break; }
        switch (clip) { case ("WindSoftMediumPulsing"): WindSoftMediumPulsing.start(); break; }
        switch (clip) { case ("WindSoftShortBurst"): WindSoftShortBurst.start(); break; }
        switch (clip) { case ("WindStrongLongConstant01"): WindStrongLongConstant01.start(); break; }
        switch (clip) { case ("WindStrongLongConstant02"): WindStrongLongConstant02.start(); break; }
        switch (clip) { case ("WindStrongLongConstant03"): WindStrongLongConstant03.start(); break; }
        switch (clip) { case ("WindStrongLongConstant04"): WindStrongLongConstant04.start(); break; }
        switch (clip) { case ("WindStrongLoop2"): WindStrongLoop2.start(); break; }
        switch (clip) { case ("WindStrongMediumConstant"): WindStrongMediumConstant.start(); break; }
        switch (clip) { case ("WindStrongMediumPulsing01"): WindStrongMediumPulsing01.start(); break; }
        switch (clip) { case ("WindStrongMediumPulsing02"): WindStrongMediumPulsing02.start(); break; }
        switch (clip) { case ("WindStrongShort"): WindStrongShort.start(); break; }
        switch (clip) { case ("WindStrongShortBurst01"): WindStrongShortBurst01.start(); break; }
        switch (clip) { case ("WindStrongShortBurst02"): WindStrongShortBurst02.start(); break; }
        switch (clip) { case ("WindStrongShortConstant01"): WindStrongShortConstant01.start(); break; }
        switch (clip) { case ("WindStrongShortConstant02"): WindStrongShortConstant02.start(); break; }
        switch (clip) { case ("WindStrongShortPulsing"): WindStrongShortPulsing.start(); break; }

        switch (clip) { case ("music"): music.start(); break; }
        switch (clip) { case ("musicStop"): music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); break; }

        switch (clip) { case ("windStop"): wind.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); break; }

        switch (clip) { case ("atmosExploring"): atmosExploring.start(); break; }
        switch (clip) { case ("atmosStruggle"): atmosStruggle.start(); break; }
        switch (clip) { case ("atmosExploringStop"): atmosExploring.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); break; }
        switch (clip) { case ("atmosStruggleStop"): atmosStruggle.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); break; }

        //switch (clip) { case ("noWind"): WindIntensity.setValue(0); break; }
    }

}

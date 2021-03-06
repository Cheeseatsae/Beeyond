﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Roo
{

    public class WindScript : MonoBehaviour
    {
        public enum Winds { EXPLORING, STRUGGLE, REWARD } // windstates for each part of tha game
        public static Winds WindStates;

        [Range(0.5f, 5f)] public float struggleLoopSpeed;
        // public static variables for game
        public static float windSpeed = 0f; // eventual global wind speed --> use Roo.WindScript.windSpeed
        public static float pingpongRange = 2f; //size of pingpong
        public static float pingpongSpeed = 1f; // speed / rate of pingpong

        // values used to lerp to smooth out windSpeed jitters
        private float _oldWindSpeed = 0f;
        private float _newWindSpeed = 1f;

        // lower the lerpAmount to smooth out wind jitters
        [Range(0.0f, 0.1f)] public float lerpAmount;

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

        [Range(1f, 6f)] public float weight1;

        [Range(1f, 10f)] public float weight1LowerRange;
        [Range(1f, 10f)] public float weight1UpperRange;

        [Range(6f, 9.5f)] public float weight2;
        [Range(1f, 10f)] public float weight2LowerRange;
        [Range(1f, 10f)] public float weight2UpperRange;

        [Range(1f, 10f)] public float weight3LowerRange;
        [Range(1f, 10f)] public float weight3UpperRange;

        private bool _hasTimeBeenReset = false;
        private float _resetTime;



        // Start is called before the first frame update
        void Start()
        {
            _resetTime = Time.time;
            WindStates = Winds.EXPLORING;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            switch (WindStates)
            {
                case (Winds.EXPLORING):

                    _newWindSpeed = Mathf.PingPong(Time.time * pingpongSpeed, pingpongRange); // get next pingpong value

                    windSpeed = noZeroValue(Mathf.Lerp(_oldWindSpeed, _newWindSpeed, lerpAmount));

                    if (_newWindSpeed < 0.5f) // if at the start of the pingpong. set new random values
                    {
                        pingpongRange = GetRandomValue(0f); //get value for pingpong 
                        pingpongSpeed = GetRandomValue(pingpongRange); //use previous value to get weighted speed value
                    }

                    _oldWindSpeed = windSpeed; // set _oldWindSpeed for origin of lerp before next update

                    break;

                case (Winds.STRUGGLE):
                    if (!_hasTimeBeenReset) { _hasTimeBeenReset = true; _resetTime = Time.time - _resetTime; }
                    _newWindSpeed = Mathf.PingPong(Time.time - _resetTime * struggleLoopSpeed, 15f); // 

                    windSpeed = noZeroValue(Mathf.Lerp(_oldWindSpeed, _newWindSpeed, lerpAmount));

                    _oldWindSpeed = windSpeed;
                    break;



                case (Winds.REWARD):
                    windSpeed = .01f;
                    break;
            }

        }

        // gets a random (weighted) float
        float GetRandomValue(float rand)
        {
            if (rand == 0f)
            {
                rand = Random.Range(0f, 10f);
            }

            if (rand <= weight1)
                return Random.Range(weight1LowerRange, weight1UpperRange);
            if (rand <= weight2)
                return Random.Range(weight2LowerRange, weight2UpperRange);

            return Random.Range(weight3LowerRange, weight3UpperRange);
        }

        float noZeroValue(float checkSpeed)
        {
            if (checkSpeed > .01)
            {
                return checkSpeed;
            }
            else
            {
                return 0.1f;
            }
        }
    }
}
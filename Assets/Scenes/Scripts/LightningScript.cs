using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roo
{
    public class LightningScript : MonoBehaviour
    {

        // public static FMOD.Studio.EventInstance thunder1, thunder2, thunder3; // thunder sounds


        public static bool lightningActive = false;
        private bool _lightningInProgress = false;

        private Light _lightning;

        [Range(0.01f, 0.5f)] public float minLightningFlux;
        [Range(0.01f, 0.5f)] public float maxLightningFlux;

        [Range(1f, 30f)] public float minPauseTime;
        [Range(5f, 60f)] public float maxPauseTime;

        [Range(10, 50)] public int minDuration;
        [Range(20, 500)] public int maxDuration;

        [Range(1f, 15f)] public float minBrightness;
        [Range(1f, 15f)] public float maxBrightness;

        [Range(-30f, 15f)] public float minVariation;
        [Range(0f, 150f)] public float maxVariation;

        [Range(.1f, 3f)] public float minPauseForThunder;
        [Range(.5f, 10f)] public float maxPauseForThunder;
        // Start is called before the first frame update
        void Start()
        {
            // thunder1 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/lightning1"); //  connect to thunder sounds
            _lightning = GetComponent<Light>();
        }

        // Update is called once per frame
        void Update()
        {
            if (lightningActive && !_lightningInProgress)
            {
                _lightningInProgress = true;
                StartCoroutine(LightningTime(Random.Range(minPauseTime, maxPauseTime), Random.Range(minDuration, maxDuration), Random.Range(minBrightness, maxBrightness), new Vector2(minVariation, maxVariation), Random.Range(minPauseForThunder, maxPauseForThunder)));
            }
        }

        IEnumerator LightningTime(float _lightningPause, int _lightningDuration, float _flashIntensity, Vector2 _intensityRange, float _pauseForThunder)
        {
            yield return new WaitForSeconds(_lightningPause);

            _lightning.intensity = _flashIntensity;

            for (int i = 0; i < _lightningDuration; i++)
            {
                yield return new WaitForSeconds(Random.Range(minLightningFlux,maxLightningFlux));

                float v = Random.Range(_flashIntensity + _intensityRange.x, _flashIntensity + _intensityRange.y);

                _lightning.intensity = v;

            }


            _lightning.intensity = 0;

            yield return new WaitForSeconds(_pauseForThunder);
            int t = Random.Range(1, 5);
            AudioManagerScript.Playsound("thunder0"+t);
            // thunder1.start() // play thunder sounds based on light intensity
            _lightningInProgress = false;
        }
    }
}
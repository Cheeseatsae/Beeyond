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

        [Range(1f, 30f)] public float minPauseTime;
        [Range(5f, 60f)] public float maxPauseTime;

        [Range(.2f, 1f)] public float minDuration;
        [Range(1f, 2f)] public float maxDuration;

        [Range(1f, 7f)] public float minBrightness;
        [Range(7f, 15f)] public float maxBrightness;

        [Range(1f, 3f)] public float minPauseForThunder;
        [Range(3f, 10f)] public float maxPauseForThunder;
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
                StartCoroutine(LightningTime(Random.Range(minPauseTime, maxPauseTime), Random.Range(minDuration, maxDuration), Random.Range(minBrightness, maxBrightness), Random.Range(minPauseForThunder, maxPauseForThunder)));
            }
        }

        IEnumerator LightningTime(float _lightningPause, float _lightningDuration, float _flashIntensity, float _pauseForThunder)
        {
            yield return new WaitForSeconds(_lightningPause);

            _lightning.intensity = _flashIntensity;
            yield return new WaitForSeconds(_lightningDuration);

            _lightning.intensity = 0;

            yield return new WaitForSeconds(_pauseForThunder);
            // thunder1.start() // play thunder sounds based on light intensity
            _lightningInProgress = false;
        }
    }
}

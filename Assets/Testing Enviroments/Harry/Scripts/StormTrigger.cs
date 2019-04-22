﻿using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class StormTrigger : MonoBehaviour
{
    public GameObject LightningObject;

    public float transparency = 0.5f;

    public float desiredExposure = 0.15f;
    public float decreaseRate = 0.02f;
    public float delay = 0.05f;
    private float exposure;
    private float oldExposure;

    // original post processing values
    // private FloatParameter _oldTemperature;
    // private FloatParameter _oldContrast;
    // private Vector4Parameter _oldGain;

    public GameObject postProcess;
    public GameObject BlockerToEnable;
    public GameObject Dust;
    private ColorGrading _colourGrading;

    public List<ParticleSystem> particles = new List<ParticleSystem>();

    public float rainIntensity;
    public float rainTransitionSpeed;
    
    private void Awake()
    {
        oldExposure = RenderSettings.skybox.GetFloat("_Exposure");
        exposure = RenderSettings.skybox.GetFloat("_Exposure");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBeeController>() == null) return;

        AudioManagerScript.gameProgression = 10f;
        AudioManagerScript.Playsound("atmosExploringStop");

        StartCoroutine(TheStormApproaches());

        PlayParticles();
        Roo.WindScript.WindStates = Roo.WindScript.Winds.STRUGGLE;
        Roo.LightningScript.lightningActive = true;
        Dust.SetActive(false);
        BlockerToEnable.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerBeeController>() == null) return;

        StartCoroutine(TheStormRecedes());

        StopParticles();
        Roo.WindScript.WindStates = Roo.WindScript.Winds.EXPLORING;
        Dust.SetActive(true);
        LightningObject.SetActive(false);
    }

    private void PlayParticles()
    {
        foreach (ParticleSystem p in particles)
        {
            //p.Play();
            StartCoroutine(StartRain(p));
        }
        Debug.Log(particles.Count);
    }
    
    public void StopParticles()
    {
        foreach (ParticleSystem p in particles)
        {
            // p.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            StartCoroutine(StopRain(p));
        }
    }

    private IEnumerator StartRain(ParticleSystem _particles)
    {
        var _emission = _particles.emission;
        while(_emission.rateOverTime.constant < rainIntensity)
        {
            yield return new WaitForSeconds(.05f);
            _emission.rateOverTime = _emission.rateOverTime.constant + rainTransitionSpeed;
        }
    }

    private IEnumerator StopRain(ParticleSystem _particles)
    {
        var _emission = _particles.emission;
        while (_emission.rateOverTime.constant > 0f)
        {
            yield return new WaitForSeconds(.05f);
            _emission.rateOverTime = _emission.rateOverTime.constant - rainTransitionSpeed;
        }
    }

    private IEnumerator TheStormApproaches()
    {
        bool running = exposure > desiredExposure;

        while (running)
        {
            exposure -= decreaseRate;
            RenderSettings.skybox.SetFloat("_Exposure", exposure);
            
            yield return new WaitForSeconds(delay);
            
            if (exposure <= desiredExposure)
            {
                running = false;
                StopCoroutine(TheStormApproaches());
            }
        }

    }

    private IEnumerator TheStormRecedes()
    {
        bool running = exposure < oldExposure;

        while (running)
        {
            exposure += decreaseRate;
            RenderSettings.skybox.SetFloat("_Exposure", exposure);

            yield return new WaitForSeconds(delay);

            if (exposure >= oldExposure)
            {
                running = false;
                StopCoroutine(TheStormRecedes());
            }
        }

    }

    private void OnApplicationQuit()
    {
        RenderSettings.skybox.SetFloat("_Exposure", oldExposure);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);
        Gizmos.color = new Color(0, 0, 1, transparency);
        Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.size);
    }
}

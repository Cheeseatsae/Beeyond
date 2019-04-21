using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Harry;

public class BranchAnimTrigger : MonoBehaviour
{

    public GameObject branch;
    public Light tempLightning;
    private bool hasPlayed = false;

    [Range(0.01f, 0.5f)] public float minLightningFlux;
    [Range(0.01f, 0.5f)] public float maxLightningFlux;

    [Range(1f, 15f)] public float minBrightness;
    [Range(1f, 15f)] public float maxBrightness;

    [Range(-30f, 15f)] public float minVariation;
    [Range(0f, 150f)] public float maxVariation;

    public ParticleSystem embers;

    [Range(0f, 15f)] public float windRequired;
    [Range(0.1f, 5f)] public float sparkMultiplyer = 1f;

    public ParticleSystem oneoffSparks;

    private bool _isPlaying = false;
    private bool _isStopped = true;
    // Start is called before the first frame update
    void Start()
    {
        var _emission = embers.emission;
        _emission.enabled = false;
    }

    void FixedUpdate()
    {
        var _emission = embers.emission;
        float ws = Roo.WindScript.windSpeed;

        _emission.rateOverTime = (ws - windRequired) * sparkMultiplyer;

        if (ws > windRequired && hasPlayed)
        {
            if (_isPlaying) return;

            _emission.enabled = true;
            _isPlaying = true;
            _isStopped = false;
            Debug.Log("Embers Playing");
        }
        else
        {
            if (_isStopped) return;

            _emission.enabled = false;
            _isStopped = true;
            _isPlaying = false;
            Debug.Log("Embers Stopped");
        }
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BeeController>() == null) return;

        if (hasPlayed) return;
        hasPlayed = true;
        branch.SetActive(true);
       // branch.GetComponent<Animator>().SetTrigger("Trigger");

        StartCoroutine(CloseLightning());

    }

    IEnumerator CloseLightning()
    {
        AudioManagerScript.Playsound("thunder05");
        oneoffSparks.Play();


        for (int i = 0; i < 15; i++)
        {
            yield return new WaitForSeconds(Random.Range(minLightningFlux, maxLightningFlux));

            float v = Random.Range(minBrightness + minVariation, maxBrightness + maxVariation);

            tempLightning.intensity = v;

        }

        branch.GetComponent<Animator>().SetTrigger("Trigger");

        tempLightning.intensity = 0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);
        Gizmos.color = new Color(0, 1, 1, .3f);
        Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.size);
    }

}

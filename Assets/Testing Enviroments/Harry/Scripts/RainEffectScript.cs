using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainEffectScript : MonoBehaviour
{
    private float _wind;
    private ParticleSystem.VelocityOverLifetimeModule velocity;
    private ParticleSystem.MinMaxCurve rate;

    // Start is called before the first frame update
    void Start()
    {
        velocity = GetComponent<ParticleSystem>().velocityOverLifetime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _wind = -Roo.WindScript.windSpeed;
        rate.constant = _wind;
        velocity.x = rate;
    }
}

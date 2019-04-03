using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneController : MonoBehaviour
{
    private WindZone _windZone;
    // Start is called before the first frame update
    void Start()
    {
        _windZone = GetComponent<WindZone>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _windZone.windPulseMagnitude = Roo.WindScript.windSpeed*2f;
        _windZone.windTurbulence = 0.5f+(Roo.WindScript.windSpeed / 10f);
    }
}

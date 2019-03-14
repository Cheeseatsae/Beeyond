using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustMovement : MonoBehaviour
{

    private float _perlinNoise = 0f;
    private Vector3 _originalPos;
    private Vector3 _pos;
    
    
    [Range(1f,20f)] public float perlinSmoothness = 3;
    [Range(1f,10f)] public float perlinAplitude = 2;
    [Range(0f,10f)] public float dustMinimumSpeed = 1;
    [Range(0.1f, 10f)] public float yAxisSpeedMultiplyer = 1;
    [Range(0.1f, 20f)] public float secondsToDestroy = 10;
    
        
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,secondsToDestroy);
        _originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _pos = transform.position;
        _perlinNoise = Mathf.PerlinNoise(_pos.x/perlinSmoothness, _pos.y/perlinSmoothness);
    }

    void FixedUpdate()
    {
        float yMulti = transform.position.y * Time.deltaTime * yAxisSpeedMultiplyer;
        if (yMulti < 0)
        {
            yMulti = 0;
            
        }
        transform.position = new Vector3(transform.position.x - (Roo.WindScript.windSpeed * Time.deltaTime)-(dustMinimumSpeed * Time.deltaTime) - yMulti, _originalPos.y + (_perlinNoise * perlinAplitude), _originalPos.z);
    }

}

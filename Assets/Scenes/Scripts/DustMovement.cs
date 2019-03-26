using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustMovement : MonoBehaviour
{

    private float _perlinNoise = 0f;
    private Vector3 _originalPos;
    private Vector3 _pos;
    private float _progressiveY;
    private float _randPerlinFloat;
    
    
    [Range(1f,20f)] public float perlinSmoothness = 3;
    [Range(1f,10f)] public float perlinAplitude = 2;
    [Range(0f,10f)] public float dustMinimumSpeed = 1;
    [Range(0.1f, 10f)] public float yAxisSpeedMultiplyer = 1;
    [Range(0.1f, 20f)] public float secondsToDestroy = 10;
    [Range(0.1f, 40f)] public float xOffsetToDestroy = 10;
    [Range(0.1f, 40f)] public float yOffsetToDestroy = 10;
    [Range(0f, 10f)] public float fallRate = 2f;
    [Range(0f, 50f)] public float movementVariation = 25f;

    private Transform _camTransform;
    
        
    // Start is called before the first frame update
    void Start()
    {
        _randPerlinFloat = Random.Range(0f, movementVariation);
        _camTransform = Roo.CameraMovementScript.liveCamera.transform;
        StartCoroutine(ParticleCheck());
        _originalPos = transform.position;
        _progressiveY = _originalPos.y;
    }

    void Update()
    {
        _pos = transform.position;
        _perlinNoise = Mathf.PerlinNoise(_pos.x/perlinSmoothness, (Time.time * _randPerlinFloat) / perlinSmoothness);
    }

    void FixedUpdate()
    {
        float yMulti = transform.position.y * Time.deltaTime * yAxisSpeedMultiplyer;
        if (yMulti < 0)
        {
            yMulti = 0;
            
        }
        transform.position = new Vector3(transform.position.x - (Roo.WindScript.windSpeed * Time.deltaTime)-(dustMinimumSpeed * Time.deltaTime) - yMulti, _progressiveY + (_perlinNoise * perlinAplitude), _originalPos.z);
        _progressiveY -= fallRate*Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }

    public IEnumerator ParticleCheck()
    {
        yield return new WaitForSeconds(secondsToDestroy);

        bool check = false;
        
        if (transform.position.x < _camTransform.position.x - xOffsetToDestroy || transform.position.y < _camTransform.position.y - yOffsetToDestroy)
        {
            Destroy(this.gameObject);
        }
        else
        {
            check = true;
        }

        while (check)
        {
            yield return new WaitForSeconds(1);

            if (transform.position.x < _camTransform.position.x - xOffsetToDestroy|| transform.position.y < _camTransform.position.y - yOffsetToDestroy)
            {
                Destroy(this.gameObject);
            }
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnInterval = 15;
    private float _counter = 0f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCloud();
    }

    void SpawnCloud()
    {
        _counter -= Time.deltaTime;
        if (_counter <= 0)
        {
            Instantiate(objectToSpawn, new Vector3(transform.position.x, Random.Range(transform.position.y*1.1f, transform.position.y/1.1f), transform.position.z), Quaternion.identity);
            _counter = Random.Range( spawnInterval*1.1f, spawnInterval/1.1f);
        }
    }
}

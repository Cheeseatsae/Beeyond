using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDustScript : MonoBehaviour
{
    private Vector3 _center;
    private bool _isSpawning = false;
    
    public Vector3 spawnerSize;
    public GameObject DustPrefab;
    public float offsetAxisY;

    [Range(0.05f,1f)] public float timeBetweenSpawn = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _center = new Vector3(transform.position.x + 20f, (spawnerSize.y/2f) + offsetAxisY,0f);

        if (!_isSpawning)
        {
            _isSpawning = true;
            float timer = timeBetweenSpawn;
            Invoke("SpawnDust",timer);
        }
    }

    public void SpawnDust()
    {
        Vector3 _pos = _center + new Vector3(Random.Range(-spawnerSize.x/2,spawnerSize.x/2),Random.Range(-spawnerSize.y/2,spawnerSize.y/2),Random.Range(-spawnerSize.z/2,spawnerSize.z/2));

        Instantiate(DustPrefab, _pos, Quaternion.identity);

        _isSpawning = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawCube(_center, spawnerSize);
    }
}

using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Harry
{
    
    
public class DummyButterflyZone : MonoBehaviour
{
    [Range(0,1)] public float transparency;
    
    public List<Vector3> points = new List<Vector3>();

    public GameObject dummyButterfly;
    
    public int butterflyCount = 5;
    public float horizontalVariance = 2;
    public float verticalVariance = 2;
    

    private void Awake()
    {
       Setup();
       SpawnButterflyCluster();
    }

    public void SpawnButterflyCluster()
    {
        Vector3 p = transform.position;
        Vector3 s = transform.localScale;
        
        for (int i = 0; i < butterflyCount; i++)
        {
            Vector3 randSpawn = new Vector3(Random.Range(p.x - s.x, p.x + s.x), Random.Range(p.y - s.y, p.y + s.y), Random.Range(p.z - s.z, p.z + s.z));
            DummyButterflyController b = Instantiate(dummyButterfly, randSpawn, Quaternion.identity).GetComponent<DummyButterflyController>();

            // setting up variables 
            b.horizontalVariance = horizontalVariance;
            b.verticalVariance = verticalVariance;
            b.points = points;

            // if = 1, travel forwards, if = 2, travel backwards
            b.travelsBackwards = Random.Range(0, 2) != 1;
            
            b.FindFirstWaypoint();
            
        }
        
    }
    
    private void Setup()
    {
        Vector3 pos = transform.position;
        Vector3 extents = transform.localScale;
        
        // making points for the 4 corners of the bounds
        points.Add(pos + new Vector3(extents.x / 2, 0, extents.z / 2));
        points.Add(pos + new Vector3(extents.x / 2, 0, -extents.z / 2));
        points.Add(pos + new Vector3(-extents.x / 2, 0,-extents.z / 2));
        points.Add(pos + new Vector3(-extents.x / 2, 0, extents.z / 2));
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
        Gizmos.color = new Color(1,0,1, transparency);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}


}

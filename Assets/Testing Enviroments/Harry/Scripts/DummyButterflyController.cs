using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Harry
{
    
    public class DummyButterflyController : MonoBehaviour
    {
        public float rotateSpeed = 2;
        private GameObject _myModel;
        private Rigidbody _myBody;
        public float speed;
        public float maxSpeed;
        public float varianceMult;
        
        private int currentWaypoint = 0;
        [HideInInspector] public List<Vector3> points = new List<Vector3>();

        [HideInInspector] public bool travelsBackwards;

        private Vector3 target;
        private Vector3 _force;

        [HideInInspector] public float horizontalVariance;
        [HideInInspector] public float verticalVariance;

        private void Awake()
        {
            _myModel = GetComponentInChildren<Renderer>().gameObject;
            _myBody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            RotateTowards(target);
            TargetDistanceCheck();
            
            // setting desired velocity to be towards the target
            _force = ((target - transform.position) * speed);
            // applying sin variation and the wind effect
            _force = new Vector3(_force.x, _force.y + (Mathf.PerlinNoise(transform.position.x, transform.position.y) * varianceMult), _force.z);
            
            // adding the force normalized
            _myBody.AddForce(_force);

            // adding drag to slow us and clamping speed
            _myBody.velocity = Vector3.Lerp(_myBody.velocity, Vector3.zero, 0.05f);
            _myBody.velocity = Vector3.ClampMagnitude(_myBody.velocity, maxSpeed);

        }

        public void FindFirstWaypoint()
        {
            Vector3 toMoveTo = Vector3.zero;
            float smallestDist = 999999f;

            if (!travelsBackwards)
            {
                // cycle forwards through list
                for (int i = 0; i < points.Count; i++)
                {
                    float dist = Vector3.Distance(transform.position,points[i]);
                
                    if (dist < smallestDist)
                    {
                        smallestDist = dist;
                        toMoveTo = points[i];
                        currentWaypoint = i;
                    }
                }  
            }
            else
            {
                // cycle backwards through list
                for (int i = points.Count - 1; i > -1; i--)
                {
                    float dist = Vector3.Distance(transform.position,points[i]);
                
                    if (dist < smallestDist)
                    {
                        smallestDist = dist;
                        toMoveTo = points[i];
                        currentWaypoint = i;
                    }
                }  
            }      
            
            target = AddVariance(toMoveTo);
            Debug.Log("First waypoint = " + target);
        }
        
        private void FindNextWaypoint()
        {

            if (!travelsBackwards)
            {
                // cycle forwards through list
                if (currentWaypoint < points.Count - 1)
                {
                    currentWaypoint++;
                    target = AddVariance(points[currentWaypoint]);
                }
                else
                {
                    currentWaypoint = 0;
                    target = AddVariance(points[currentWaypoint]);
                }
            }
            else
            {
                // cycle backwards through list
                if (currentWaypoint <= 0)
                {
                    currentWaypoint = points.Count - 1;
                    target = AddVariance(points[currentWaypoint]);
                }
                else
                {
                    currentWaypoint--;
                    target = AddVariance(points[currentWaypoint]);
                }
            }

        }

        private Vector3 AddVariance(Vector3 p)
        {
            // adds variances to inputed vector3
            float x = p.x + Random.Range(-horizontalVariance, horizontalVariance);
            float y = p.y + Random.Range(-verticalVariance, verticalVariance);
            float z = p.z + Random.Range(-horizontalVariance, horizontalVariance);
            
            return new Vector3(x,y,z);
        }
        
         private void TargetDistanceCheck()
         {
             if (Vector3.Distance(transform.position, target) < 1)
                 FindNextWaypoint();

         }
        
        private void RotateTowards(Vector3 t)
        {
            // finding dir to turn towards
            Vector3 dir = t - _myModel.transform.position;
            // so we dont go upside down
            dir = new Vector3(dir.x, 0,dir.z);
            
            // so we dont go sideways/turn when we dont want to
            if (dir.x < 0.1f && dir.x > -0.1f) return;
            
            // setting turn speed
            float step = rotateSpeed * Time.deltaTime;

            // actual rotation
            Vector3 newDir = Vector3.RotateTowards(_myModel.transform.forward, dir, step, 0);
            _myModel.transform.rotation = Quaternion.LookRotation(newDir);

        }
       
    }

}



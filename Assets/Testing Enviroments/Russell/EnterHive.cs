using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{
    public class EnterHive : AIStateBase
    {
        public GameObject bee;
        private GameObject hive;    
        private AIBeeController ai;
        public HiveWaypoint waypoints;

        public override void Enter()
        {
            base.Enter();
            ai = bee.GetComponent<AIBeeController>();
            hive = GameObject.FindGameObjectWithTag("Hive");
            waypoints = hive.GetComponentInChildren<HiveWaypoint>();
            ai.windSpeedMult = 0;
            ai.maxSpeed = 2;
            ai.yAxisWindCutOff = 0;
            ai.windYAxisDivider = 0;
            ai.target = waypoints.waypoint1;
            StartCoroutine(SwitchTargets());
            
            

        }

        IEnumerator SwitchTargets()
        {
            yield return new WaitForSeconds(2);
            ai.target = waypoints.waypoint2;
            yield return  new WaitForSeconds(2);
            ai.target = waypoints.waypoint3;
            yield return  new WaitForSeconds(2);
            Destroy(bee);
        }
    }
    

}

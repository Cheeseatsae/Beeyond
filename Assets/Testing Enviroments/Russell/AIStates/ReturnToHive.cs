using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{
    public class ReturnToHive : AIStateBase
    {
        public GameObject bee;
        private AIBeeController controller;
        public FollowThePlayer ftp;
        public float distance;

        public GameObject hive;

        public GameObject returnPoint;
        // Start is called before the first frame update

        private void Awake()
        {
            ftp = GetComponent<FollowThePlayer>();
        }

        public override void Enter()
        {
            base.Enter();
            controller = bee.GetComponent<AIBeeController>();
            hive = GameObject.FindWithTag("Hive");
            controller.target = hive;
            

        }

        public override void Execute()
        {
            base.Execute();
            distance = Vector3.Distance(hive.transform.position, this.transform.position);
            if (distance > 10)
            {
                ftp.ObjAvoidance();
                ftp.AiDistanceCheck();
            }
            
        }
    }


}

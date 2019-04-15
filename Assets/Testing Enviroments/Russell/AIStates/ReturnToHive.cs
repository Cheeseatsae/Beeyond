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
        public HiveInteractable hiveInt;
        public GameObject hive2;
        public GameObject returnPoint;
        // Start is called before the first frame update

        private void Awake()
        {
            ftp = GetComponent<FollowThePlayer>();
            
        }

        public override void Enter()
        {
            base.Enter();
            hive = GameObject.FindWithTag("Hive");
            controller = bee.GetComponent<AIBeeController>();

            
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

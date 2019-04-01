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

        public GameObject hive;
        // Start is called before the first frame update
        public override void Enter()
        {
            base.Enter();
            controller = bee.GetComponent<AIBeeController>();
            hive = GameObject.FindWithTag("Hive");
            controller.target = hive;
            

        }
    }


}

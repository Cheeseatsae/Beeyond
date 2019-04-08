using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{
    public class LandOnFlower : AIStateBase
    {
        private AIBeeController beeController;
        public GameObject parent;
        public FlowerInteraction myFlower;
        private Rigidbody rb;


        private void Awake()
        {
            beeController = parent.GetComponent<AIBeeController>();
            
            rb = parent.GetComponent<Rigidbody>();
        }

        public override void Enter()
        {
            base.Enter();
            myFlower = beeController.target.GetComponentInParent<FlowerInteraction>();
            //beeController.target = myFlower.aiPickupPoint;
        }

        public override void Execute()
        {
            base.Execute();
            float speed = 1 * Time.deltaTime;            
            parent.transform.position =
                Vector3.MoveTowards(parent.transform.position, beeController.target.transform.position, speed);
            StartCoroutine(GoBackToHive());

        }
// Start is called before the first frame update

        IEnumerator GoBackToHive()
        {
            yield return new WaitForSeconds(6);
            //myFlower.harvested = true;
            //myFlower.active = false;
            beeController.ChangeState(beeController.returnToHive);
            
        }
    }


}

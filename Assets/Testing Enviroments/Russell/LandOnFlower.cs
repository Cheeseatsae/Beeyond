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
        private FlowerInteraction myFlower;
        private Rigidbody rb;


        private void Awake()
        {
            beeController = parent.GetComponent<AIBeeController>();
            
            rb = parent.GetComponent<Rigidbody>();
        }

        public override void Enter()
        {
            base.Enter();
            //myFlower = beeController.target.GetComponent<FlowerInteraction>();
            //beeController.target = myFlower.aiPickupPoint;
        }

        public override void Execute()
        {
            base.Execute();
            

        }
// Start is called before the first frame update

        IEnumerator GoBackToHive()
        {
            yield return new WaitForSeconds(5);
            beeController.ChangeState(beeController.returnToHive);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{
    public class FindAFlower : AIStateBase
    {
        public AIBeeController controller;
        public FlowerManager flowerManager;
        public FollowThePlayer follow;
        public GameObject newTarget;
        public int flowerIndex;
        
        // Start is called before the first frame update

        private void Awake()
        {
            flowerManager = FindObjectOfType<FlowerManager>();
        }

        void Start()
        {
            follow = GetComponent<FollowThePlayer>();
        }


        public override void Enter()
        {
            base.Enter();
            ChooseAFlower();
        }

        public override void Execute()
        {
            base.Execute();
            
            follow.ObjAvoidance();
            follow.AiDistanceCheck();
            
        }

        public void ChooseAFlower()
        {
            flowerIndex = Random.Range(0, flowerManager.FlowerList.Count);
            newTarget = flowerManager.FlowerList[flowerIndex];
            controller.target = newTarget;
            follow.minPlayerDistance = 0;
            follow.maxPlayerDistance = 0;
        }
        
    }


}

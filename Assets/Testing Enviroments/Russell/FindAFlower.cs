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
        public List<GameObject> flowersIveVisited = new List<GameObject>();
        
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
            CheckIfIveBeenHereBefore();
        }

        public override void Execute()
        {
            base.Execute();
            
            //follow.PlayerDistanceCheck();
            follow.ObjAvoidance();
            follow.AiDistanceCheck();
            
        }

        public void ChooseAFlower()
        {
            flowerIndex = Random.Range(0, flowerManager.FlowerList.Count);
            newTarget = flowerManager.FlowerList[flowerIndex];
            controller.target = newTarget;
            flowersIveVisited.Add(newTarget);
        }

        public void CheckIfIveBeenHereBefore()
        {
            foreach (GameObject target in flowersIveVisited)
            {
                if (newTarget == target)
                {
                    ChooseAFlower();
                }
            }
        }
    }


}

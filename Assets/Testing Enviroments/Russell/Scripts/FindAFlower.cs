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
        public List<GameObject> myflowers = new List<GameObject>();
        
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
            
            //follow.PlayerDistanceCheck();
            follow.ObjAvoidance();
            follow.AiDistanceCheck();
            
        }

        public void ChooseAFlower()
        {
            AddMyFlowers();            
            flowerIndex = Random.Range(0, myflowers.Count);
            newTarget = myflowers[flowerIndex];
            controller.target = newTarget;
            
        }

        public void AddMyFlowers()
        {
            foreach (GameObject flowers in flowerManager.FlowerList)
            {
                if (!myflowers.Contains(flowers))
                {
                    myflowers.Add(flowers);
                }
            }
        }
        
        
    }


}

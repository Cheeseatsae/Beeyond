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
        public int count;
        public bool hasflower = false;
        public List<GameObject> myflowers = new List<GameObject>();
        
        // Start is called before the first frame update

        private void Awake()
        {
            flowerManager = FindObjectOfType<FlowerManager>();
        }

        void Start()
        {
            follow = GetComponent<FollowThePlayer>();
            ForceOthersAway.ReList += ChooseNewFlower;
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
            if (myflowers.Count == 0 && controller.visitedAFlower == false)
            {
                controller.ChangeState(controller.returnToHive);
            }
            
        }

        public void ChooseAFlower()
        {
            if (!controller.visitedAFlower)
            {
                Debug.Log("rerun flowerchooser");
                AddMyFlowers();
                if (myflowers.Count != 0)
                {
                    flowerIndex = Random.Range(0, myflowers.Count);
                    newTarget = myflowers[flowerIndex];
                    controller.target = newTarget.GetComponent<FlowerInteraction>().aiPickupPoint;
                    hasflower = true;
                    Debug.Log("rerun flowerchooser");
                }
            }
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

        public void ChooseNewFlower(int count)
        {
            myflowers.Clear();
            ChooseAFlower();           
        }
        
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{
    public class ForceOthersAway : MonoBehaviour
    {
        public GameObject flower;
        private FlowerInteraction floInt;
        

        public GameObject thisFlowersBee;
        
        public delegate void RecalMyList(int i);

        public static RecalMyList ReList;
        // Start is called before the first frame update
        private void Awake()
        {
            floInt = flower.GetComponent<FlowerInteraction>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<AIBeeController>() && floInt.active && other.GetComponent<AIBeeController>().visitedAFlower == false && !floInt.visited)
            {
                thisFlowersBee = other.gameObject;
                AIBeeController beeState = thisFlowersBee.GetComponent<AIBeeController>();
                beeState.ChangeState(beeState.gettingPollen);
                beeState.target = floInt.aiPickupPoint;
                floInt.visited = true;
                beeState.visitedAFlower = true;                
                ReList.Invoke(floInt.flowerLevel);
                
                //floInt.active = false;
                
                //run event to remove this from list
            }

        }
    }


}

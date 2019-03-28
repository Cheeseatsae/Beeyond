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
            if (other.GetComponent<AIBeeController>())
            {
                thisFlowersBee = other.gameObject;
                //change the bees state to get the pollen
                floInt.state = Interactable.State.Occupied;
                //run event to remove this from list
            }

        }
    }


}

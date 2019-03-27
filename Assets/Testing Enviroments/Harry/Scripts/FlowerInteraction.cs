using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{
    
    public class FlowerInteraction : Interactable
    {
        
        public bool harvested;
        [Range(0, 5)]
        public int flowerLevel;
        public bool playerOnly;
        

        public bool active;

        private void Start()
        {
            HiveInteractable.ActivateNextFlowers += ActivateFlower;
        }

        public override void OnCollisionEnter(Collision other)
        {
            if (harvested) return;
            base.OnCollisionEnter(other);
        }

        public void ActivateFlower(int count)
        {
            if (flowerLevel == count)
            {
                active = true;
            }
        }
        public override void OnInteract()
        {
            harvested = true;
            active = false;
            GetComponent<Renderer>().material.color = Color.grey;
            myBeeController.myState = BeeController.BeeState.Pollenated;
            base.OnInteract();
        }
    }
}

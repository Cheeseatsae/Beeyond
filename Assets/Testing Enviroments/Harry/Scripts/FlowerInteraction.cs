using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{
    
    public class FlowerInteraction : MonoBehaviour
    {
        public bool harvested = false;

        private float previousWindMod;
        private GameObject myBee;
        private BeeController myBeeController;
    
        public enum FlowerState
        {
            Occupied,
            Unoccupied
        }

        public FlowerState state = FlowerState.Unoccupied;
    
        private void OnCollisionEnter(Collision other)
        {
            if (harvested) return;

            // on collision with a bee occupy the flower if we aren't already occupied
            if (other.gameObject.GetComponent<BeeController>() != null && state == FlowerState.Unoccupied)
            {
                // setup
                state = FlowerState.Occupied;
                myBee = other.gameObject;
                myBeeController = myBee.GetComponent<BeeController>();
                myBeeController.currentFlower = GetComponent<FlowerInteraction>();
            
                // removing wind effectiveness from bee
                previousWindMod = myBeeController.windSpeedMult;
                myBeeController.windSpeedMult = 0;
                // stopping bee
                myBeeController.myState = BeeController.BeeState.Stopped;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            // if our bee leaves 
            if (myBee == other.gameObject && state == FlowerState.Occupied)
            {
                // turn wind back on
                myBeeController.windSpeedMult = previousWindMod;
                myBeeController.currentFlower = null;
                // remove references 
                myBee = null;
                myBeeController = null;
            }
        }

        public void Harvest()
        {
            if (!harvested) harvested = true;
        }
    }


}

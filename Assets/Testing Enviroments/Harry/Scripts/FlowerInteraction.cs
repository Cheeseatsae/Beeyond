using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{
    
    public class FlowerInteraction : Interactable
    {

        public bool harvested;

        public override void OnCollisionEnter(Collision other)
        {
            if (harvested) return;
            base.OnCollisionEnter(other);
        }

        public override void OnInteract()
        {
            harvested = true;
            myBeeController.myState = BeeController.BeeState.Pollenated;
            base.OnInteract();
        }
    }


}

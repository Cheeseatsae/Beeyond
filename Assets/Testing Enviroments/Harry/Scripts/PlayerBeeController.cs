using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{
    
    public class PlayerBeeController : BeeController
    {
        private void Update()
        {
            // HACK will need to be redone later 

                if (myState == BeeState.Stopped)
                {
                    Debug.Log(currentInteractable);
                    currentInteractable.OnInteract();
                }
               
            
        }

        // Update is called once per frame
        public override void FixedUpdate()
        {
        // if we're stopped do nothing
            if (myState == BeeState.Stopped) return;
        
            base.FixedUpdate();
        
        }
    }
    
}


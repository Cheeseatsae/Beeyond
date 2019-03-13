using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{
    
    public class PlayerBeeController : BeeController
    {
   
        // Update is called once per frame
        public override void FixedUpdate()
        {
        
            // HACK will need to be redone later 
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (myState)
                {
                    case BeeState.Stopped:
                        myState = BeeState.Pollenated;
                        currentFlower.Harvest();

                        break;

                    case BeeState.Pollenated:
                        myState = BeeState.Moving;

                        break;  
                }
               
            }

            // if we're stopped do nothing
            if (myState == BeeState.Stopped) return;
        
            base.FixedUpdate();
        
        }
    }
    
}


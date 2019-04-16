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
            playerState.text = myState.ToString();

                if (myState == BeeState.Stopped && !interacting)
                {
                    currentInteractable.OnInteract();
                if (currentInteractable.GetComponent<FlowerInteraction>() != null) PlayAnimation("Standing");
                if (currentInteractable.GetComponent<HiveInteractable>() != null) PlayAnimation("ButtDance");
                }
            
        }

        public void PlayAnimation(string s)
        {
            GetComponentInChildren<Animator>().SetTrigger(s);
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


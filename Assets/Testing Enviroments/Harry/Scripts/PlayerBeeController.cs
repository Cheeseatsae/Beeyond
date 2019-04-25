using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{
    
    public class PlayerBeeController : BeeController
    {
        private bool _animationRunning = false;
        public ParticleSystem fireflies;
        public float numberOfFireflies;

        void Start()
        {
            var _emission = fireflies.emission;
            _emission.rateOverTime = 0f;
        }
        
        private void Update()
        {
            // HACK will need to be redone later 
            playerState.text = myState.ToString();

                if (myState == BeeState.Stopped && !interacting)
                {
                    currentInteractable.OnInteract();
                if (currentInteractable.GetComponent<FlowerInteraction>() != null) PlayAnimation("Standing");
                if (currentInteractable.GetComponent<HiveInteractable>() != null)
                {
                    var _emission = fireflies.emission;
                    _emission.rateOverTime = 0f;
                    _animationRunning = false;

                    PlayAnimation("ButtDance");
                }
                }
            if(myState == BeeState.Pollenated && !_animationRunning)
            {
                var _emission = fireflies.emission;
                _emission.rateOverTime = numberOfFireflies;
                    _animationRunning = true;
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


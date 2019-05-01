using System;
using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{

    

    public class FlowerInteraction : Interactable
    {
        public ParticleSystem fireflies;
        public float numberOfFireflies = 5f;
        private bool hasRemovedFireflies = false;
        
        public bool harvested;
        [Range(0, 5)]
        public int flowerLevel;
        public bool playerOnly;
        public GameObject aiPickupPoint;

        public bool visited = false;
        public bool active;

        public float flowerAnimationDelay = 2f;

        public delegate void OnFlowerInteract();

        public OnFlowerInteract InteractionEvent;


        private void Start()
        {
            HiveInteractable.ActivateNextFlowers += ActivateFlower;

            InteractionEvent += PlayAnimation;

            

            if (playerOnly)
            {
                aiPickupPoint.SetActive(false);
            }

            // fireflies.Play(); not needed, set in particle inspector
        }



        void FixedUpdate()
        {
            if (harvested && playerOnly && !hasRemovedFireflies)
            {
                
                fireflies.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                
                hasRemovedFireflies = true;
            }
        }
    

        public override void OnCollisionEnter(Collision other)
        {
            if (harvested) return;
            if (other.gameObject.GetComponent<BeeController>() == null) return;
            if (other.gameObject.GetComponent<BeeController>().myState == BeeController.BeeState.Pollenated) return;
            if (other.gameObject.GetComponent<PlayerBeeController>() != null) { AudioManagerScript.Playsound("BeeDigQuickShort02");}            
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
            if (myBeeController.myState == BeeController.BeeState.Pollenated) return;
            base.OnInteract();
        }

        public override IEnumerator Interaction()
        {
            myBeeController.interacting = true;                
            yield return new WaitForSeconds(delay);
            
            harvested = true;
            active = false;
            GetComponent<Renderer>().material.color = Color.grey;
            myBeeController.myState = BeeController.BeeState.Pollenated;
            InteractionEvent?.Invoke();
            
            Reset();
        }

        public void PlayAnimation()
        {
            StartCoroutine(OnPlay());
        }

        public IEnumerator OnPlay()
        {
            yield return new WaitForSeconds(flowerAnimationDelay);

            GetComponentInChildren<Animator>().SetTrigger("Play");
        }

        private void OnDestroy()
        {
            InteractionEvent -= PlayAnimation;
        }
    }
}

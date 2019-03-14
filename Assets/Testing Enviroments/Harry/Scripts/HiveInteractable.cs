using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace Harry
{
    
    public class HiveInteractable : Interactable
    { 
        
        public int[] amountOfBees;
        public int[] pollenRequirement;
        public GameObject aiBee;

        public int pollenCount = 0;
        private int triggerCount = 0;
    
        public delegate void OnPollenIncrease(int i);

        public static OnPollenIncrease PollenCollected;

        private void Start()
        {
            // spawn bees on event
            PollenCollected += SpawnBees;
        }

        public override void OnCollisionEnter(Collision other)
        {
            // if bee isnt pollenated dont let it do anything
            if (other.gameObject.GetComponent<BeeController>().myState != BeeController.BeeState.Pollenated) return;
            
            base.OnCollisionEnter(other);
        }

        public override void OnInteract()
        {
            // if interacted increase the pollen count
            pollenCount++;
            // allow bee to move
            myBeeController.myState = BeeController.BeeState.Moving;

            if (triggerCount > pollenRequirement.Length - 1) return;
            // with enough pollen run event
            if (pollenCount >= pollenRequirement[triggerCount])
            {
                triggerCount++;
                PollenCollected?.Invoke(triggerCount);
            }
            
            base.OnInteract();
        }

        public void SpawnBees(int count)
        {
            //if (amountOfBees.Length > count) return;
            
            for (int i = 0; i < amountOfBees[count - 1]; i++)
            {
                GameObject newBee = Instantiate(aiBee, transform.position + (Vector3.up * 4), Quaternion.Euler(0, 0, 0));
                newBee.GetComponent<BeeController>().target = myBee;
            }
        }
        
        private void OnDestroy()
        {
            PollenCollected -= SpawnBees;
        }
    }
    
}


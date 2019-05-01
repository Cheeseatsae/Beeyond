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
        public GameObject aiSpawnPoint;

        public int pollenCount = 0;
        
        private static int triggerCount = 0;
    
        public delegate void OnPollenIncrease(int i);

        public static OnPollenIncrease PollenCollected;
        
        public delegate void ActivateFlowers(int i);

        public static ActivateFlowers ActivateNextFlowers;

        private void Start()
        {
            // spawn bees on event
            PollenCollected += SpawnBees;
        }

        public override void OnCollisionEnter(Collision other)
        {
            // if bee isnt pollenated dont let it do anything
            if (other.gameObject.GetComponent<BeeController>().myState != BeeController.BeeState.Pollenated) return;
            if (other.gameObject.GetComponent<PlayerBeeController>().myState != BeeController.BeeState.Pollenated) return;


            // if (other.gameObject.GetComponent<PlayerBeeController>() != null) AudioManagerScript.gameProgression += 1;

            base.OnCollisionEnter(other);
        }

        public override IEnumerator Interaction()
        {
            
            myBeeController.interacting = true;                
            yield return new WaitForSeconds(delay);

            AudioManagerScript.gameProgression += 1; // increment the game audio progression

            // if interacted increase the pollen count
            pollenCount++;
            // allow bee to move
            myBeeController.myState = BeeController.BeeState.Moving;
            
            if (triggerCount > pollenRequirement.Length - 1) StopCoroutine(Interaction());
            // with enough pollen run event
            if (pollenCount >= pollenRequirement[triggerCount])
            {
                triggerCount++;
                PollenCollected?.Invoke(triggerCount);
                ActivateNextFlowers.Invoke(triggerCount);
            }
            Reset();

        }

        public void SpawnBees(int count)
        {
            //if (amountOfBees.Length > count) return;
            if (myBee == null) return;
            
            for (int i = 0; i < amountOfBees[count - 1]; i++)
            {
                GameObject newBee = Instantiate(aiBee, aiSpawnPoint.transform.position, Quaternion.Euler(0, 0, 0));
                newBee.GetComponent<BeeController>().target = myBee;
            }
        }
        
        private void OnDestroy()
        {
            PollenCollected -= SpawnBees;
        }
    }
    
}


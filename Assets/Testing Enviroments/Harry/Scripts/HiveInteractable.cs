using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;


namespace Harry
{
    
    public class HiveInteractable : Interactable
    { 
        
        int amountOfBees = 2;
        public GameObject aiBee;
        
        public override void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<BeeController>().myState != BeeController.BeeState.Pollenated) return;
            
            base.OnCollisionEnter(other);
        }

        public override void OnInteract()
        {
            //if (myBeeController.myState != BeeController.BeeState.Pollenated) return;

            Debug.Log("Hive triggered");
            myBeeController.myState = BeeController.BeeState.Moving;

            for (int i = 0; i < amountOfBees; i++)
            {
                GameObject newBee = Instantiate(aiBee, transform.position + (Vector3.up * 4), Quaternion.Euler(0, 0, 0));
                newBee.GetComponent<BeeController>().target = myBee;
            }
            
            IncreaseBees();
            base.OnInteract();
        }

        public void IncreaseBees()
        {
            switch (amountOfBees)
            {
                case 2:
                    amountOfBees = 5;
                    break;
                
                case 5:
                    amountOfBees = 8;
                    break;
            }
        }
    }
    
}


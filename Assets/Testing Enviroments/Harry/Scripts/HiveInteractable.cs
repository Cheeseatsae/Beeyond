using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Harry
{
    
    public class HiveInteractable : Interactable
    {

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
            GameObject newBee = Instantiate(aiBee, transform.position + (Vector3.up * 4), Quaternion.Euler(0, 0, 0));
            newBee.GetComponent<BeeController>().target = myBee;
            
            base.OnInteract();
        }
    }
    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{

    public class Interactable : MonoBehaviour
    {
        private bool withinTrigger = false;
        private float previousWindMod;
        [HideInInspector] public GameObject myBee;
        [HideInInspector] public BeeController myBeeController;

        public enum State
        {
            Occupied,
            Unoccupied
        }

        public State state = State.Unoccupied;

        public virtual void OnCollisionEnter(Collision other)
        {
            if (!withinTrigger) return;

            // on collision with a bee occupy the flower if we aren't already occupied
            if (other.gameObject.GetComponent<BeeController>() != null && state == State.Unoccupied)
            {
                Debug.Log("theres a bee on me");
                // setup
                state = State.Occupied;
                myBee = other.gameObject;
                myBee.GetComponent<Rigidbody>().velocity = Vector3.zero;
                myBeeController = myBee.GetComponent<BeeController>();
                myBeeController.currentInteractable = GetComponent<Interactable>();

                // removing wind effectiveness from bee
                previousWindMod = myBeeController.windSpeedMult;
                myBeeController.windSpeedMult = 0;
                // stopping bee
                myBeeController.myState = BeeController.BeeState.Stopped;
            }
        }

        public virtual void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<BeeController>() != null) withinTrigger = true;
            else withinTrigger = false;
        }

        public virtual void OnTriggerExit(Collider other)
        {
            withinTrigger = false;
        }

        public virtual void OnInteract()
        {
            // On interaction
            // turn wind back on
            myBeeController.windSpeedMult = previousWindMod;
            
            myBeeController.currentInteractable = null;
            myBee = null;
            myBeeController = null;
            state = State.Unoccupied;

        }
    }

}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{

    public class Interactable : MonoBehaviour
    {
        private bool withinTrigger = false;
        private float previousWindMod;
        private float previousYWindMod;
        [HideInInspector] public GameObject myBee;
        public BeeController myBeeController;

        public float delay = 2;

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
                // setup
                state = State.Occupied;
                myBee = other.gameObject;
                myBee.GetComponent<Rigidbody>().velocity = Vector3.zero;
                myBeeController = myBee.GetComponent<BeeController>();
                myBeeController.currentInteractable = GetComponent<Interactable>();

                // removing wind effectiveness from bee
                previousWindMod = myBeeController.windSpeedMult;
                previousYWindMod = myBeeController.windYAxisDivider;
                myBeeController.windSpeedMult = 0;
                myBeeController.windYAxisDivider = 0;
                // stopping bee
                myBeeController.myState = BeeController.BeeState.Stopped;
            }
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BeeController>() != null || other.gameObject.layer == 15)
            {
                withinTrigger = true;
                Debug.Log("Bee in trigger");
            }
            else withinTrigger = false;
        }

        public virtual void OnTriggerExit(Collider other)
        {
            withinTrigger = false;
        }

        public virtual void OnInteract()
        {
            StartCoroutine(Interaction());
        }

        public virtual IEnumerator Interaction()
        {
            myBeeController.interacting = true;                
            yield return new WaitForSeconds(delay);
            Reset();
        }
        
        public virtual void Reset()
        {
            // On interaction
            // turn wind back on
            myBeeController.windSpeedMult = previousWindMod;
            myBeeController.windYAxisDivider = previousYWindMod;
            myBeeController.interacting = false;
            
            myBeeController.currentInteractable = null;
            myBee = null;
            myBeeController = null;
            state = State.Unoccupied;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{
    public class FinalAreaState : AIStateBase
    {
        private FlowerEndList flowerlist;
        
        public AIBeeController ai;
        public GameObject parent;

        private void Awake()
        {
            flowerlist = FindObjectOfType<FlowerEndList>();
            
            ai = parent.GetComponent<AIBeeController>();

        }

        public override void Enter()
        {
            base.Enter();
            ai.target = flowerlist.finalflowers[Random.Range(0, flowerlist.finalflowers.Length)];
        }

        public override void Execute()
        {
            base.Execute();
            if (Vector3.Distance(parent.transform.position, ai.target.transform.position) < 1)
            {
                StartCoroutine(WaitASec());
            }
        }

        public void ReTarget()
        {
            
        }

        IEnumerator WaitASec()
        {
            yield return new WaitForSeconds(2);
            ai.target = flowerlist.finalflowers[Random.Range(0, flowerlist.finalflowers.Length)];
             
        }
    }


}

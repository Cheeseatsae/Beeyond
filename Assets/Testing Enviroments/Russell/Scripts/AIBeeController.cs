using System.Collections;
using UnityEngine;

namespace Harry
{
    public class AIBeeController : BeeController
    {            
        public AIStateBase followState;
        public AIStateBase findflower;
        public AIStateBase currentState;
        public AIStateBase gettingPollen;
        public AIStateBase returnToHive;
        public AIStateBase enterHive;
        public bool visitedAFlower = false;

        public void ChangeState(AIStateBase newState)
        {
            //Check state is not the same
            currentState.Exit();
            newState.Enter();
            currentState = newState;
            //Debug.Log("Ran Change State "+ newState);
        }

        public override void Awake()
        {
            base.Awake();
            ChangeState(followState);

        }
        private void Start()

        {
            ChangetoFollow.GoFindFlowers += ChangeToFind;
        }

        private void ChangeToFind()
        {
            ChangeState(findflower);
            
        }

        public override void FixedUpdate()
        {

            base.FixedUpdate();
            
            
            currentState.Execute();
        }

    }
}
﻿using System.Collections;
using UnityEngine;

namespace Harry
{
    public class AIBeeController : BeeController
    {            
        public AIStateBase followState;
        public AIStateBase findflower;
        public AIStateBase currentState;

        public void ChangeState(AIStateBase newState)
        {
            //Check state is not the same
            currentState.Exit();
            newState.Enter();
            currentState = newState;
            Debug.Log("Ran Change State "+ newState);
        }

        public override void Awake()
        {
            base.Awake();
            ChangeState(followState);

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            currentState.Execute();
        }

    }
}
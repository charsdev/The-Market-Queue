using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Chars.Tools
{
    [SerializeField]
    public class FiniteStateMachine
    {
        protected Dictionary<int, State> states = new Dictionary<int, State>();
        public State currentState;

        public FiniteStateMachine() { }

        public void Add(int key, State state) => states.Add(key, state);
        public State GetState(int key) => states[key];
        public void Start() => SetCurrentState(GetState(0));
        public void Update() => currentState?.Update();
        public void SetCurrentState(State state)
        {
            currentState?.Exit();
            currentState = state;
            currentState?.Enter();
        }
    }
}

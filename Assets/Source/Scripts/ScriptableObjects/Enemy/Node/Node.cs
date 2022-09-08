using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingame.Behaviour 
{
    [Serializable]
    public abstract class Node : ScriptableObject
    {
        public enum State
        {
           Running,
           Failure,
           Success,
           Stop,
           Abandon
        }

        [SerializeField] 
        [HideInInspector]
        private State state;
        
        [HideInInspector]
        public string Guid;
        
        [HideInInspector]
        public Vector2 Position = Vector2.zero;
        
        private bool _isRunning = false;
        public State Tick()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                ActOnStart();
            }

            state = ActOnTick();

            if (state == State.Success || state == State.Failure)
            {
                _isRunning = false;
                ActOnStop();
            }

            return state;
        }

        public virtual Node Clone()
        {
            return Instantiate(this);
        }
        protected abstract void ActOnStart();
        protected abstract void ActOnStop();
        protected abstract State ActOnTick();
    }
}
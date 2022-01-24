using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    public abstract class StateBase : ScriptableObject
    { 
        public abstract void OnEnter(ref EcsEntity entity);

        public StateBase Tick(ref EcsEntity entity)
        {
            foreach (var i in Transitions)
            {
                if (i.Decision.Decide())
                {
                    i.State.OnEnter(ref entity);
                    OnExit(ref entity);
                    return i.State;
                }
            }

            return null;
        }

        public abstract void OnExit(ref EcsEntity entity);
        public List<Transition> Transitions;
    }
}
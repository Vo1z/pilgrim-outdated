using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ingame.Enemy.Logic
{

    public abstract class StateBase : ScriptableObject
    {
        [FormerlySerializedAs("Decisions")] [SerializeField]
        private List<DecisionBase> decisions;
        
        public StateBase GetPresentState(ref EcsEntity entity)
        {
            if (IsNotBlocked(ref entity))
            {
                foreach (var i in decisions) {
                    if (i.Decide(ref entity))
                    {
                        DeleteCurrentStateTag(ref entity);
                        return i.NextState;
                    }
                } 
            }
            return this;          
        }

        protected abstract void DeleteCurrentStateTag(ref EcsEntity entity);

        protected virtual bool IsNotBlocked(ref EcsEntity entity)
        {
            return true;
        }
    }
}
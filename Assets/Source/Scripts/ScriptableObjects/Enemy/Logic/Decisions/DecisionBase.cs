using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    public abstract class DecisionBase : ScriptableObject
    {
        [SerializeField]
        private StateBase nextState;
        public StateBase NextState => nextState;
        
        public abstract bool Decide(ref EcsEntity entity);
    }
}
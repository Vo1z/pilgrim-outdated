using System.Linq;
using Ingame.Enemy.State;
using Ingame.Health;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;
namespace Ingame.Enemy.System
{
    public class EnemyBehaviourSystem : IEcsRunSystem
    {
        private EcsFilter<StateModel,EnemyBehaviourTag> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var state = ref _filter.Get1(i);
                state.State = state.State.GetPresentState(ref entity);
            }
        }
    }
}
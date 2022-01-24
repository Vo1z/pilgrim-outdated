using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.ECS{
    sealed class FollowSystem : IEcsRunSystem {
        // auto-injected fields.
        private readonly EcsFilter<EnemyMovementComponent,FollowStateTag> _enemyFilter;
        
        void IEcsRunSystem.Run () {
            foreach (var i in _enemyFilter)
            {
                Debug.Log("1");
                ref var enity = ref _enemyFilter.GetEntity(i);
                ref var movementComponent = ref _enemyFilter.Get1(i);
                movementComponent.NavMeshAgent.isStopped = true;
            }
        }
    }
}
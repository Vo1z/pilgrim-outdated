
using Ingame.Enemy.ECS;
using Ingame.Enemy.Logic;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.ECS {
    sealed class EnemyMovementSystem : IEcsRunSystem  {
        
        private readonly EcsFilter<EnemyMovementComponent> _enemyFilter;
        public void Run()
        {
            foreach (var i in _enemyFilter)
            {
                ref var enemyEntity = ref _enemyFilter.GetEntity(i);
                ref var enemyMovement = ref _enemyFilter.Get1(i);
                if (enemyMovement.Waypoint == null)
                {
                    enemyEntity.Get<WaypointGetTag>();
                    return;
                }

                enemyMovement.NavMeshAgent.destination = enemyMovement.Waypoint.position;
                enemyMovement.NavMeshAgent.speed = enemyMovement.EnemyMovementData.SpeedForward;
                enemyMovement.NavMeshAgent.isStopped = false;
                if ((enemyMovement.NavMeshAgent.remainingDistance <= enemyMovement.NavMeshAgent.stoppingDistance && !enemyMovement.NavMeshAgent.pathPending))
                {
                    enemyEntity.Get<WaypointNextTag>();
                }
            }
        }
    }
}
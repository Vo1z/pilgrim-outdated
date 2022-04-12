using Ingame.Enemy.State;
using Leopotam.Ecs;
 

namespace Ingame.Enemy.System{
    public sealed class FollowSystem : IEcsRunSystem {
        private readonly EcsFilter<EnemyMovementComponent,LocateTargetComponent,FollowStateTag> _enemyFilter;
        
        void IEcsRunSystem.Run () {
            foreach (var i in _enemyFilter)
            {
                ref var entity = ref _enemyFilter.GetEntity(i);
                ref var movement = ref _enemyFilter.Get1(i);
                ref var target = ref _enemyFilter.Get2(i);

                movement.Waypoint = target.Target;
                movement.NavMeshAgent.speed = movement.EnemyMovementData.SpeedForward;
                movement.NavMeshAgent.destination = movement.Waypoint.position;
                movement.NavMeshAgent.isStopped = false;
                if ((movement.NavMeshAgent.remainingDistance <= movement.NavMeshAgent.stoppingDistance &&
                     !movement.NavMeshAgent.pathPending))
                {
                    movement.NavMeshAgent.isStopped = true;
                }
            }
        }
    }
}
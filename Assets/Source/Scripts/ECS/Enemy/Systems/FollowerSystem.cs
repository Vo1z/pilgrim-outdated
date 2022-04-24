using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;


namespace Ingame.Enemy.System
{
    public sealed class FollowerSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyMovementComponent,FollowerModel,TransformModel,RoboDogTag> _filter;
        private readonly float _distanceTreshhold = 25f;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var movement = ref _filter.Get1(i);
                ref var follower = ref _filter.Get2(i);
                ref var transformModel = ref _filter.Get3(i);
                if (follower.Followed == null)
                {
                    continue;
                }

                movement.NavMeshAgent.isStopped = false;
                
                if ((movement.NavMeshAgent.remainingDistance <= movement.NavMeshAgent.stoppingDistance &&
                         !movement.NavMeshAgent.pathPending))
                {
                    movement.NavMeshAgent.stoppingDistance = movement.EnemyMovementData.MaxDistanceFromDestinationPoint;
                    movement.NavMeshAgent.speed = movement.EnemyMovementData.SpeedForward;
                    
                }

                if (_distanceTreshhold<Vector3.Distance(transformModel.transform.position,follower.Followed.position))
                {
                    movement.NavMeshAgent.destination = follower.Followed.position;
                }
            }
        }
    }
}
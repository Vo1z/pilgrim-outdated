 
using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.System
{
    public sealed class PatrolSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyMovementComponent,WaypointComponent,PatrolStateTag> _enemyFilter;
        public void Run()
        {

            foreach (var i in _enemyFilter)
            {
                ref var entity = ref _enemyFilter.GetEntity(i);
                if (entity.Has<WaitOnPointCallbackRequest>())
                {
                    continue;
                }
                
                ref var enemyMovement = ref _enemyFilter.Get1(i);
                ref var waypoint = ref _enemyFilter.Get2(i);
                if (waypoint.Waypoints ==null || waypoint.Waypoints.Count>=0)
                {
                    continue;
                }
                enemyMovement.Waypoint = waypoint.Waypoints[waypoint.Index];
                

                enemyMovement.NavMeshAgent.destination = enemyMovement.Waypoint.position;
                enemyMovement.NavMeshAgent.speed = enemyMovement.EnemyMovementData.SpeedForward;
                enemyMovement.NavMeshAgent.isStopped = false;
                if ((enemyMovement.NavMeshAgent.remainingDistance <= enemyMovement.NavMeshAgent.stoppingDistance &&
                     !enemyMovement.NavMeshAgent.pathPending))
                {
                    entity.Get<WaitOnPointCallbackRequest>();
                    enemyMovement.Waypoint = GetNextWaypoint(ref entity,ref waypoint);
                }
            }
        }
        
        private Transform GetNextWaypoint(ref EcsEntity e,ref WaypointComponent waypoints)
        {
            //Reverse
            if (e.Has<WaypointReverseComponent>())
            {
                ref var direction = ref e.Get<WaypointReverseComponent>();
                if (waypoints.Index + 1 >=waypoints.Waypoints.Count)
                {
                    direction.Direction = true;
                }
                else if(waypoints.Index<=0)
                {
                    direction.Direction = false;
                }
                waypoints.Index += (direction.Direction ? -1 : 1);
                   
            }
            //Straight
            else
            {
                waypoints.Index = (++waypoints.Index)%waypoints.Waypoints.Count;
            }

            return waypoints.Waypoints[waypoints.Index];
        }
    }
}
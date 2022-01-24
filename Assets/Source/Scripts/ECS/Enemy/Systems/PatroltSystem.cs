using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.ECS
{
    public sealed class PatrolSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyMovementComponent,WaypointComponent,PatrolStateTag> _enemyFilter;
        public void Run()
        {

            foreach (var i in _enemyFilter)
            {
                ref var entity = ref _enemyFilter.GetEntity(i);
                ref var enemyMovement = ref _enemyFilter.Get1(i);
                ref var waypoint = ref _enemyFilter.Get2(i);
                if (enemyMovement.Waypoint == null)
                {
                    enemyMovement.Waypoint = waypoint.Waypoints[waypoint.Index];
                }

                enemyMovement.NavMeshAgent.destination = enemyMovement.Waypoint.position;
                enemyMovement.NavMeshAgent.speed = enemyMovement.EnemyMovementData.SpeedForward;
                enemyMovement.NavMeshAgent.isStopped = false;
                if ((enemyMovement.NavMeshAgent.remainingDistance <= enemyMovement.NavMeshAgent.stoppingDistance &&
                     !enemyMovement.NavMeshAgent.pathPending))
                {
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
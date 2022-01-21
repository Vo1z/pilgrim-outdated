using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.ECS
{
    public sealed class WaypointSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyMovementComponent,WaypointComponent,WaypointNextTag> _waypointsNext;
        private readonly EcsFilter<EnemyMovementComponent,WaypointComponent,WaypointGetTag> _waypointsGet;
        public void Run()
        {
            //NextTag
            foreach (var i in _waypointsNext)
            {
                ref var waypointEntity = ref _waypointsNext.GetEntity(i);
                ref var enemyMovement = ref _waypointsNext.Get1(i);
                ref var waypoints = ref _waypointsNext.Get2(i);
                //Reverse
                if (waypointEntity.Has<WaypointReverseComponent>())
                {
                    ref var direction = ref waypointEntity.Get<WaypointReverseComponent>();
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

                enemyMovement.Waypoint = waypoints.Waypoints[waypoints.Index];
                waypointEntity.Del<WaypointNextTag>();
            }
            //GetTag
            foreach (var i in _waypointsGet)
            {
                ref var waypointEntity = ref _waypointsGet.GetEntity(i);
                ref var enemyMovement = ref _waypointsGet.Get1(i);
                ref var waypoints = ref _waypointsGet.Get2(i);
                enemyMovement.Waypoint = waypoints.Waypoints[waypoints.Index];
                waypointEntity.Del<WaypointGetTag>();
            }
        }
    }
}
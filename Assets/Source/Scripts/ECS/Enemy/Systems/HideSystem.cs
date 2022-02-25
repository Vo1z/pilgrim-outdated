using System;
using Ingame.Enemy.State;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Ingame.Enemy.System
{
    public class HideSystem: IEcsRunSystem
    {
        private EcsFilter<EnemyMovementComponent,LocateTargetComponent,HideBehindObstacleComponent,TransformModel,HideStateTag> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var movement = ref _filter.Get1(i);
                ref var target = ref _filter.Get2(i);
                ref var obstacle = ref _filter.Get3(i);
                ref var enemy = ref entity.Get<TransformModel>();

                var distanceA = (obstacle.Obstacle.position - target.Target.position).magnitude;
                var distanceB = (obstacle.Obstacle.position - enemy.transform.position).magnitude;
                if (distanceA<distanceB || (target.Target.position-enemy.transform.position).magnitude<100)
                {
                    //find another obstacle or flee/attack
                    //todo
                    //implement inside enemy
                }

                RaycastHit ray;
                bool result = (Physics.Linecast(target.Target.position,enemy.transform.position,out ray));
                if (ray.collider.CompareTag("Terrain"))
                {
                    enemy.transform.LookAt(target.Target);
                   return;
                }

                if (NavMesh.SamplePosition(obstacle.Obstacle.position + (obstacle.Obstacle.position-target.Target.position).normalized ,out NavMeshHit hit,1f,NavMesh.AllAreas))
                {
                    movement.NavMeshAgent.destination = hit.position;
                }
                
                movement.NavMeshAgent.autoRepath = true;
                movement.NavMeshAgent.isStopped = false;
            }
        }
    }
}
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
        private EcsFilter<EnemyMovementComponent,LocateTargetComponent,HideModel,TransformModel,HideStateTag> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var movement = ref _filter.Get1(i);
                ref var target = ref _filter.Get2(i);
                ref var obstacle = ref _filter.Get3(i);
                ref var enemy = ref entity.Get<TransformModel>();
                
                bool result = (Physics.Linecast(target.Target.position,enemy.transform.position,out RaycastHit ray));
                //Hidden
                if (ray.collider.CompareTag("Terrain") && result)
                {
                    enemy.transform.LookAt(target.Target);
                    entity.Del<HideInProgressTag>();
                    entity.Get<HideBlockComponent>();
                   continue;
                }

                entity.Get<HideInProgressTag>();
                    //Find cover
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
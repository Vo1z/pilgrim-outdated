using System;
using Ingame.Enemy.State;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Ingame.Enemy.System
{
    public class FleeSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyMovementComponent,LocateTargetComponent,TransformModel, VisionModel,FleeStateTag> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var movement = ref _filter.Get1(i);
                ref var target = ref _filter.Get2(i);
                ref var enemy = ref _filter.Get3(i);
                ref var vision = ref entity.Get<VisionModel>();
                if (target.Target == null)
                {
                    if (vision.Opponents.Count > 0)
                    {
                        target.Target = vision.Opponents[0];
                    }
                  
                }
                var pos = enemy.transform.position - target.Target.position;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(pos + enemy.transform.position,out hit,movement.EnemyMovementData.MaxDistanceFromDestinationPoint,NavMesh.AllAreas))
                {
                    movement.NavMeshAgent.destination = hit.position;
                    if (Vector3.Distance(enemy.transform.position,target.Target.position)>movement.EnemyMovementData.MaxFleeDistance)
                    {
                        movement.NavMeshAgent.isStopped = true;
                    }
                    else
                    {
                        movement.NavMeshAgent.isStopped = false;
                    }
                }
            }
        }
    }
}
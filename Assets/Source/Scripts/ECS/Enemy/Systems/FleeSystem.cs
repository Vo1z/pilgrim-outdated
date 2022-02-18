using Ingame.Enemy.State;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Ingame.Enemy.System
{
    public class FleeSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyMovementComponent,LocateTargetComponent,TransformModel, FleeStateTag> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var movement = ref _filter.Get1(i);
                ref var target = ref _filter.Get2(i);
                ref var enemy = ref _filter.Get3(i);

                var pos = enemy.transform.position - target.Target.position;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(pos + enemy.transform.position,out hit,5f,NavMesh.AllAreas))
                {
                    movement.NavMeshAgent.destination = hit.position;
                    Debug.Log(hit.position);
                    if (Vector3.Distance(enemy.transform.position,target.Target.position)>100)
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
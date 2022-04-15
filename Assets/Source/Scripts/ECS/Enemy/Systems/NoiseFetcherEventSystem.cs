using Ingame.Enemy.State;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.System {
    public sealed class NoiseFetcherEventSystem : IEcsRunSystem
    {
        private readonly EcsFilter<NoiseGeneratorEvent> _filter;
        private readonly EcsFilter<EnemyBehaviourTag,VisionBinderComponent,EnemyMovementComponent,PatrolStateTag> _enemyFilter;
        void IEcsRunSystem.Run () {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var position = ref entity.Get<TransformModel>();
                var pos = position.transform.position;
                var point = new Vector3(pos.x,pos.y,pos.z);
                
                foreach (var j in _enemyFilter)
                {
                    ref var enemyEntity = ref _enemyFilter.GetEntity(j);
                    ref var enemyPosition = ref enemyEntity.Get<TransformModel>();

                    ref var visionBinder = ref _enemyFilter.Get2(j);
                    if (!visionBinder.FarRange.TryGetComponent(out EntityReference farEntityRef))
                    {
                        continue;
                    }
                    ref var farEntity = ref farEntityRef.Entity;
                    ref var vision = ref farEntity.Get<VisionModel>();
                    if (vision.Vision.MaxDistance>Vector3.Distance(enemyPosition.transform.position,pos))
                    {
                        continue;
                    }
                    
                    enemyPosition.transform.LookAt(point);
                    
                    if (enemyEntity.Has<EnemyMovementComponent>())
                    {
                        ref var enemyMovement = ref _enemyFilter.Get3(j);
                        enemyMovement.Waypoint = point;
                        enemyMovement.NavMeshAgent.destination = point;
                        enemyMovement.NavMeshAgent.isStopped = false;
                    }
                }
            }
        }
    }
}
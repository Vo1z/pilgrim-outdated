using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.System {
    sealed class NoiseFetcherEventSystem : IEcsRunSystem
    {
        private readonly EcsFilter<NoiseGeneratorEvent> _filter;
        private readonly EcsFilter<EnemyBehaviourTag,VisionModel> _enemyFilter;
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
                    ref var enemyVision = ref _enemyFilter.Get2(j);
                    if (enemyVision.Vision.MaxDistance>Vector3.Distance(enemyPosition.transform.position,pos))
                    {
                        continue;
                    }
                    enemyPosition.transform.LookAt(point);
                    
                    if (enemyEntity.Has<EnemyMovementComponent>())
                    {
                        ref var enemyMovement = ref entity.Get<EnemyMovementComponent>();
                        enemyMovement.NavMeshAgent.destination = point;
                        enemyMovement.NavMeshAgent.isStopped = false;
                    }
                }
            }
        }
    }
}
using Ingame.Enemy;
using LeoEcsPhysics;
using Leopotam.Ecs;

namespace Ingame.Systems
{
    public class EnemyObstacleDetectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<OnTriggerEnterEvent> _enterFilter;
        private readonly EcsFilter<OnTriggerExitEvent> _exitFilter;
        
        public void Run()
        {
            foreach (var i in _enterFilter)
            {
                ref var enter = ref _enterFilter.Get1(i);
                //has entity Reference
                if (!enter.senderGameObject.TryGetComponent<EntityReference>(out var entityReference))
                {
                    continue;
                }
                //Entity Has PointerToGameObject
                if(!entityReference.Entity.Has<PointerToParentGameObjectComponent>())
                    continue;
                
                ref var parent = ref entityReference.Entity.Get<PointerToParentGameObjectComponent>().Parent;
                if (!parent.TryGetComponent<EntityReference>(out var entityParentReference))
                {
                    continue;
                }

                if (!entityParentReference.Entity.Has<EnemyStateModel>())
                {
                    continue;
                }

                ref var enemyStateModel = ref entityParentReference.Entity.Get<EnemyStateModel>();
                
                //enter.collider;
                // enemyStateModel.Co
            }
        }
    }
}
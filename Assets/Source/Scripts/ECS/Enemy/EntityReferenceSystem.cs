using Ingame.Enemy.ECS;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame {
    sealed class EntityReferenceSystem : IEcsRunSystem
    {
        private EcsFilter<EntityReferenceRequest> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {

                ref var entity = ref _filter.GetEntity(i);
                ref var reference = ref _filter.Get1(i);
                reference.EntityReference.Entity = entity;
                entity.Del<EntityReferenceRequest>();
             
            }
        }
    }
}
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<VelocityComponent, TransformModel> _velocityFilter;
        
        public void Run()
        {
            foreach (var i in _velocityFilter)
            {
                ref var velocityComponent = ref _velocityFilter.Get1(i);
                ref var transformModel = ref _velocityFilter.Get2(i);
                var deltaMovement = velocityComponent.velocity * Time.fixedDeltaTime;
                
                transformModel.transform.position += deltaMovement;
            }
        }
    }
}
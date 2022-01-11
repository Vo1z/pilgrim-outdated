using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<VelocityComponent, CharacterControllerComponent> _movingObjectsFilter;
        
        public void Run()
        {
            foreach (var i in _movingObjectsFilter)
            {
                ref var velocityComp = ref _movingObjectsFilter.Get1(i);
                ref var characterControllerComp = ref _movingObjectsFilter.Get2(i);

                var velocity = velocityComp.velocity;
                var characterController = characterControllerComp.characterController;

                characterController.Move(velocity * Time.fixedDeltaTime);
            }
        }
    }
}
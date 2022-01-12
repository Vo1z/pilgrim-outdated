using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class PlayerFrictionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerModel, VelocityComponent> _playerFilter;
        
        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                ref var playerModel = ref _playerFilter.Get1(i);
                ref var playerVelocityComponent = ref _playerFilter.Get2(i);
                var playerData = playerModel.playerData;
                
                var velocityCopy = playerVelocityComponent.velocity;
                float playerMovementFriction = playerData.MovementFriction * Time.fixedDeltaTime;

                playerVelocityComponent.velocity = Vector3.Lerp(velocityCopy, Vector3.zero, playerMovementFriction);
                playerVelocityComponent.velocity = new Vector3
                {
                    x = playerVelocityComponent.velocity.x,
                    y = velocityCopy.y,
                    z = playerVelocityComponent.velocity.z
                };
            }
        }
    }
}
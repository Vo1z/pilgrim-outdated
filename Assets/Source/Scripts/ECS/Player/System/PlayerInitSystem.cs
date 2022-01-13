using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsFilter<PlayerModel> _playerFilter;

        public void Init()
        {
            //todo move to settings
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Application.targetFrameRate = 240;
            foreach (var i in _playerFilter)
            {
                ref var playerEntity = ref _playerFilter.GetEntity(i);
                ref var playerModel = ref _playerFilter.Get1(i);
                ref var playerGravityComponent = ref playerEntity.Get<GravityComponent>();
                ref var playerCharacterControllerModel = ref playerEntity.Get<CharacterControllerModel>();
                ref var playerFrictionComp = ref playerEntity.Get<FrictionComponent>();
                playerEntity.Get<VelocityComponent>();
                playerEntity.Get<TimerComponent>();

                var playerData = playerModel.playerData;

                playerGravityComponent.gravityAcceleration = playerData.GravityAcceleration;
                playerGravityComponent.maximalGravitationalForce = playerData.MaximumGravitationForce;
                playerCharacterControllerModel.slidingForceModifier = playerData.SlidingForceModifier;
                playerFrictionComp.frictionPower = playerData.MovementFriction;
            }
        }
    }
}
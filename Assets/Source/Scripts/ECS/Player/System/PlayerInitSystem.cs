using Leopotam.Ecs;

namespace Ingame
{
    public sealed class PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsFilter<PlayerModel> _playerFilter;

        public void Init()
        {
            foreach (var i in _playerFilter)
            {
                ref var playerEntity = ref _playerFilter.GetEntity(i);
                ref var playerModel = ref _playerFilter.Get1(i);
                ref var playerGravityComponent = ref playerEntity.Get<GravityComponent>();
                playerEntity.Get<VelocityComponent>();
                playerEntity.Get<TimerComponent>();

                var playerData = playerModel.playerData;

                playerGravityComponent.gravityAcceleration = playerData.GravityAcceleration;
                playerGravityComponent.maximalGravitationalForce = playerData.MaximumGravitationForce;
            }
        }
    }
}
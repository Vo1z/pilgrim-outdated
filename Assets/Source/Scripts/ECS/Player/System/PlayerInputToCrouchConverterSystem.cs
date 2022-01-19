using Leopotam.Ecs;

namespace Ingame
{
    public sealed class PlayerInputToCrouchConverterSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerModel, CharacterControllerModel> _playerFilter;
        private readonly EcsFilter<CrouchInputEvent> _crouchInputFilter;

        public void Run()
        {
            if(_crouchInputFilter.IsEmpty())
                return;

            foreach (var i in _playerFilter)
            {
                ref var playerEntity = ref _playerFilter.GetEntity(i);
                ref var playerModel = ref _playerFilter.Get1(i);
                ref var playerCharacterControllerModel = ref _playerFilter.Get2(i);
                ref var playerCrouchRequest = ref playerEntity.Get<CrouchRequest>();
                var playerData = playerModel.playerData;
                playerModel.isCrouching = !playerModel.isCrouching;
                
                var targetCharacterControllerHeight = playerModel.isCrouching
                    ? playerCharacterControllerModel.initialHeight / 2
                    : playerCharacterControllerModel.initialHeight;

                playerCrouchRequest.height = targetCharacterControllerHeight;
                playerCrouchRequest.changeHeightSpeed = playerData.EnterCrouchStateSpeed;

                playerModel.currentSpeed = playerModel.isCrouching ? playerData.CrouchWalkSpeed : playerData.WalkSpeed;
            }
        }
    }
}
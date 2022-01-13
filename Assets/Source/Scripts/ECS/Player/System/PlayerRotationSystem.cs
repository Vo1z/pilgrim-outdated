using Ingame.PlayerLegacy;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class PlayerRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerModel, CharacterControllerModel> _playerFilter;
        private readonly EcsFilter<PlayerHudModel, TransformModel> _hudFilter;
        private readonly EcsFilter<RotateRequest> _rotationFilter;

        public void Run()
        {
            if(_playerFilter.IsEmpty())
                return;

            ref var rotationRequest = ref _rotationFilter.Get1(0);
            
            var rotationInput = rotationRequest.rotationInput;
            PlayerData playerData = null;
            
            foreach (var i in _playerFilter)
            {
                ref var playerModel = ref _playerFilter.Get1(i);
                ref var playerCharacterController = ref _playerFilter.Get2(i);
                playerData = playerModel.playerData;
                var playerTransform = playerCharacterController.CharacterController.transform;
                
                var xRotation = rotationInput.x * playerData.Sensitivity * Time.fixedDeltaTime;

                playerTransform.Rotate(Vector3.up, xRotation);
            }

            if(playerData == null)
                return;

            foreach (var i in _hudFilter)
            {
                ref var hudModel = ref _hudFilter.Get1(i);
                ref var hudTransformModel = ref _hudFilter.Get2(i);
                var hudTransform = hudTransformModel.transform;
                
                var yRotation = rotationInput.y * playerData.Sensitivity * Time.fixedDeltaTime;
                hudModel.hudLocalRotationX -= yRotation;
                hudModel.hudLocalRotationX = Mathf.Clamp(hudModel.hudLocalRotationX, -90, 90);
                
                hudTransform.localRotation = Quaternion.Euler(hudModel.hudLocalRotationX, 0, 0);
            }
        }
    }
}
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class PlayerHudInputToRotationConverterSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerModel> _playerFilter;
        private readonly EcsFilter<HudModel, TransformModel> _hudFilter;
        private readonly EcsFilter<RotateInputRequest> _rotateRequestFilter;

        public void Run()
        {
            if (_rotateRequestFilter.IsEmpty() || _playerFilter.IsEmpty())
                return;

            var rotationInput = _rotateRequestFilter.Get1(0).rotationInput;
            var playerData = _playerFilter.Get1(0).playerData;
            
            foreach (var i in _hudFilter)
            {
                ref var hudModel = ref _hudFilter.Get1(i);
                ref var hudTransformModel = ref _hudFilter.Get2(i);

                var yRotation = rotationInput.y * playerData.Sensitivity * Time.fixedDeltaTime;
                hudModel.hudLocalRotationX -= yRotation;
                hudModel.hudLocalRotationX = Mathf.Clamp(hudModel.hudLocalRotationX, -90, 90);

                hudTransformModel.transform.localRotation = Quaternion.Euler(hudModel.hudLocalRotationX, 0, 0);
            }
        }
    }
}
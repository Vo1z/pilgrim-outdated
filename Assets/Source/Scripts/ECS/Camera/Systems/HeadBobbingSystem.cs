using Ingame.Movement;
using Ingame.Player;
using Ingame.Utils;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.CameraWork
{
    public class HeadBobbingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerModel, DeltaMovementComponent> _playerModelFilter;
        private readonly EcsFilter<CameraModel, TransformModel, CameraBobbingComponent, MainCameraTag> _mainCameraFilter;

        public void Run()
        {
            if (_playerModelFilter.IsEmpty() || _mainCameraFilter.IsEmpty())
                return;

            ref var playerModel = ref _playerModelFilter.Get1(0);
            ref var playerDeltaMovementComp = ref _playerModelFilter.Get2(0);
            ref var mainCameraTransformModel = ref _mainCameraFilter.Get2(0);
            ref var mainCameraBobbingComp = ref _mainCameraFilter.Get3(0);

            var playerHudData = playerModel.playerHudData;            
            var mainCameraTransform = mainCameraTransformModel.transform;
            
            float bobbingOffset = Mathf.Sin(mainCameraBobbingComp.timeSpentTraveling);
            float deltaMovementMagnitude = playerDeltaMovementComp.deltaMovement.magnitude * playerHudData.HeadBobbingSpeedModifier;
            
            if (deltaMovementMagnitude > .01f)
            {
                mainCameraTransform.localPosition = new Vector3
                (
                    mainCameraTransformModel.initialLocalPos.x + bobbingOffset * playerHudData.HeadBobbingStrengthX,
                    mainCameraTransformModel.initialLocalPos.y + bobbingOffset * playerHudData.HeadBobbingStrengthY,
                    mainCameraTransformModel.initialLocalPos.z + bobbingOffset * playerHudData.HeadBobbingStrengthZ
                );

                mainCameraBobbingComp.timeSpentTraveling += deltaMovementMagnitude;
            }
            else
            {
                mainCameraBobbingComp.timeSpentTraveling = 0;
                mainCameraTransform.localPosition = Vector3.Lerp(mainCameraTransform.localPosition, mainCameraTransformModel.initialLocalPos, playerHudData.HeadBobbingLerpingSpeed * Time.deltaTime);
            }
        }
    }
}
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
            ref var mainCameraModel = ref _mainCameraFilter.Get1(0);
            ref var mainCameraTransformModel = ref _mainCameraFilter.Get2(0);
            ref var mainCameraBobbingComp = ref _mainCameraFilter.Get3(0);

            var playerHudData = playerModel.playerHudData;            
            var mainCameraTransform = mainCameraTransformModel.transform;
            var mainCameraLocalPos = mainCameraTransform.localPosition;
            float bobbingOffset = Mathf.Sin(mainCameraBobbingComp.timeSpentTraveling);
            float deltaMovementMagnitude = playerDeltaMovementComp.deltaMovement.magnitude * playerHudData.HeadBobbingSpeedModifier;
            
            if (deltaMovementMagnitude > .01f)
            {
                mainCameraTransform.localPosition = new Vector3
                (
                    mainCameraLocalPos.x, 
                    mainCameraTransformModel.initialLocalPos.y + bobbingOffset * playerHudData.HeadBobbingStrength,
                    mainCameraLocalPos.z
                );
                mainCameraBobbingComp.timeSpentTraveling += deltaMovementMagnitude;
            }
            else
            {
                mainCameraBobbingComp.timeSpentTraveling = 0;
                mainCameraTransform.localPosition = Vector3.Lerp(mainCameraTransform.localPosition, mainCameraTransformModel.initialLocalPos, 5 * Time.deltaTime);
            }
        }
    }
}
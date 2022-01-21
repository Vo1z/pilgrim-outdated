using Ingame.Guns;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class HudItemMoverDueSurfaceDetectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SurfaceDetectorModel, HudItemModel, TransformModel, InHandsTag> _itemFilter;

        public void Run()
        {
            foreach (var i in _itemFilter)
            {
                ref var hudItemModel = ref _itemFilter.Get2(i);
                
                if(hudItemModel.isAiming)
                    continue;

                ref var transformModel = ref _itemFilter.Get3(i);
                var surfaceDetector = _itemFilter.Get1(i).surfaceDetector;
                var hudItemData = hudItemModel.itemData;
                var hudItemTransform = transformModel.transform;
                var initialItemLocalPos = transformModel.initialLocalPos;
                
                var gunSurfaceDetectionResult = surfaceDetector.SurfaceDetectionType;
                
                if(gunSurfaceDetectionResult == SurfaceDetectionType.SameSpot)
                    return;

                var clippingSpeed = hudItemData.ClippingMovementSpeed * Time.deltaTime;
                var movementDirectionZ = gunSurfaceDetectionResult == SurfaceDetectionType.Detection ? -hudItemData.MaximumClippingOffset : 0;
                var nextGunLocalPos = initialItemLocalPos + Vector3.forward * movementDirectionZ;

                hudItemTransform.localPosition = Vector3.Lerp(hudItemTransform.localPosition, nextGunLocalPos, clippingSpeed);
            }
        }
    }
}
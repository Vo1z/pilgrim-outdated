using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class HudItemRotatorDueDeltaRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HudItemDataModel, TransformModel, HudInHandsItemTag> _inHandItemFilter;
        private readonly EcsFilter<RotateInputRequest> _rotateInputFilter;

        private const float ANGLE_FOR_ONE_SCREEN_PIXEL = .1f;
        private const float INPUT_ANGLE_VARIETY = 10f;
        
        public void Run()
        {
            var deltaRotation = _rotateInputFilter.IsEmpty()? Vector2.zero : _rotateInputFilter.Get1(0).rotationInput;
            
            foreach (var i in _inHandItemFilter)
            {
                ref var hudItemDataModel = ref _inHandItemFilter.Get1(i);
                ref var hudItemTransformModel = ref _inHandItemFilter.Get2(i);
                var itemData = hudItemDataModel.hudItemData;
                var itemTransform = hudItemTransformModel.transform;
                var itemLocalRotation = itemTransform.localRotation;
                
                var targetRotation = hudItemTransformModel.initialLocalRotation * GetHudRotationDueToDeltaRotation(deltaRotation, itemData);
                var rotationSpeed = itemData.RotationSpeed * Time.deltaTime;
                
                itemLocalRotation = Quaternion.Slerp(itemLocalRotation, targetRotation, rotationSpeed);
                itemTransform.localRotation = itemLocalRotation;
            }
        }
        
        private Quaternion GetHudRotationDueToDeltaRotation(Vector2 deltaRotation, HudItemData hudItemData)
        {
            var deltaRotationInputInAngle = deltaRotation * ANGLE_FOR_ONE_SCREEN_PIXEL;
            deltaRotationInputInAngle.x = Mathf.Clamp(deltaRotationInputInAngle.x, -INPUT_ANGLE_VARIETY, INPUT_ANGLE_VARIETY);
            deltaRotationInputInAngle.y = Mathf.Clamp(deltaRotationInputInAngle.y, -INPUT_ANGLE_VARIETY, INPUT_ANGLE_VARIETY);
            
            var xRotationAngle = hudItemData.RotationAngleMultiplierX * deltaRotationInputInAngle.y;
            xRotationAngle = Mathf.Clamp(xRotationAngle, hudItemData.MinMaxRotationAngleX.x, hudItemData.MinMaxRotationAngleX.y);
            xRotationAngle *= hudItemData.InverseRotationX;

            var yRotationAngle = hudItemData.RotationAngleMultiplierY * deltaRotationInputInAngle.x;
            yRotationAngle = Mathf.Clamp(yRotationAngle, hudItemData.MinMaxRotationAngleY.x, hudItemData.MinMaxRotationAngleY.y);
            yRotationAngle *= hudItemData.InverseRotationY;

            var zRotationAngle = hudItemData.RotationAngleMultiplierZ * deltaRotationInputInAngle.x;
            zRotationAngle = Mathf.Clamp(zRotationAngle, hudItemData.MinMaxRotationAngleZ.x, hudItemData.MinMaxRotationAngleZ.y);
            zRotationAngle *= hudItemData.InverseRotationZ;

            var resultRotation = Quaternion.AngleAxis(xRotationAngle, Vector3.right) *
                                 Quaternion.AngleAxis(yRotationAngle, Vector3.up) *
                                 Quaternion.AngleAxis(zRotationAngle, Vector3.forward);

            return resultRotation;
        }
    }
}
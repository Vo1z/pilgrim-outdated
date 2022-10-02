using System.Runtime.CompilerServices;
using Ingame.Data.Hud;
using Ingame.Input;
using Ingame.Movement;
using Ingame.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Hud
{
    public sealed class HudItemMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<HudItemModel, TransformModel, InInventoryTag, HudIsInHandsTag> _itemFilter;
        private readonly EcsFilter<RotateInputRequest> _rotateInputFilter;
        private readonly EcsFilter<PlayerModel> _playerFilter;
        
        private const float ANGLE_FOR_ONE_SCREEN_PIXEL = .01f;

        public void Init()
        {
            foreach (var i in _itemFilter)
            {
                ref var itemEntity = ref _itemFilter.GetEntity(i);

                if(!itemEntity.Has<HudItemInstabilityComponent>())
                    return;

                ref var instabilityComponent = ref itemEntity.Get<HudItemInstabilityComponent>();
                var itemData = _itemFilter.Get1(i).itemData;

                instabilityComponent.currentInstability = itemData.InitialInstability;
                instabilityComponent.currentMovementDirection = Vector3.zero;
                instabilityComponent.timeLeftMoving = 0f;
            }
        }

        public void Run()
        {
            
            foreach (var i in _itemFilter)
            {
                ref var itemEntity = ref _itemFilter.GetEntity(i);
                ref var hudItemModel = ref _itemFilter.Get1(i);
                ref var transformModel = ref _itemFilter.Get2(i);
                
                var itemData = hudItemModel.itemData;
                var itemTransform = transformModel.transform;
                
                var initialLocalPosX = transformModel.initialLocalPos.x;
                var initialLocalPosY = transformModel.initialLocalPos.y;
                Vector3 nextLocalPos = Vector3.zero;

                //Movement due to player rotation
                if (!_rotateInputFilter.IsEmpty() && itemData.IsItemMovedDueToRotation && !itemEntity.Has<HudIsAimingTag>())
                {
                    var deltaRotation = _rotateInputFilter.Get1(0).rotationInput;
                    if (!_playerFilter.IsEmpty())
                        deltaRotation *= _playerFilter.Get1(0).playerMovementData.Sensitivity * ANGLE_FOR_ONE_SCREEN_PIXEL;
                    
                    nextLocalPos += GetLocalPositionOffsetDueToPlayerRotation(itemData, deltaRotation);
                }
                
                //Movement due to instability
                if (itemEntity.Has<HudItemInstabilityComponent>())
                {
                    ref var instabilityComponent = ref itemEntity.Get<HudItemInstabilityComponent>();
                    nextLocalPos += GetLocalPositionOffsetDueToItemInstability(itemData, ref instabilityComponent);
                }
                
                nextLocalPos *= Time.deltaTime;
                nextLocalPos += itemTransform.localPosition;
                
                nextLocalPos.x = Mathf.Clamp(nextLocalPos.x, initialLocalPosX + itemData.MinMaxMovementOffsetX.x, initialLocalPosX + itemData.MinMaxMovementOffsetX.y);
                nextLocalPos.y = Mathf.Clamp(nextLocalPos.y, initialLocalPosY + itemData.MinMaxMovementOffsetY.x, initialLocalPosY + itemData.MinMaxMovementOffsetY.y);

                itemTransform.localPosition = nextLocalPos;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector3 GetLocalPositionOffsetDueToPlayerRotation(HudItemData itemData, in Vector2 deltaRotation)
        {
            Vector3 positionOffset = Vector3.zero;
            positionOffset.x += -deltaRotation.x * itemData.MoveSpeed;

            return positionOffset;
        }

        private Vector3 GetLocalPositionOffsetDueToInitialPositionMovement(HudItemData itemData, in Vector3 initialLocalPos, in Vector3 currentLocalPos)
        {
            Vector3 positionOffset = Vector3.zero;
            positionOffset.x = initialLocalPos.x - currentLocalPos.x * itemData.MoveToInitialPosSpeed;

            return positionOffset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector3 GetLocalPositionOffsetDueToItemInstability(HudItemData itemData, ref HudItemInstabilityComponent instabilityComponent)
        {
            if (instabilityComponent.timeLeftMoving <= 0f)
            {
                float timeLeftForMoving = instabilityComponent.currentInstability * itemData.DefaultInstabilityMovementTime / itemData.InitialInstability;
                float movementSpeed = instabilityComponent.currentInstability * itemData.DefaultInstabilityMovingSpeed / itemData.InitialInstability;
                var movementDirection = Random.insideUnitCircle.normalized;

                instabilityComponent.timeLeftMoving = timeLeftForMoving;
                instabilityComponent.currentMovementSpeed = movementSpeed;
                instabilityComponent.currentMovementDirection = movementDirection;
            }

            instabilityComponent.timeLeftMoving -= Time.deltaTime;
            var movementOffset = instabilityComponent.currentMovementDirection * instabilityComponent.currentMovementSpeed * Mathf.Sin(Time.time);

            return movementOffset;
        }
    }
}
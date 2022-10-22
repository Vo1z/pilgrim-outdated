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

                nextLocalPos *= Time.deltaTime;
                nextLocalPos += itemTransform.localPosition;
                
                nextLocalPos.x = Mathf.Clamp(nextLocalPos.x, initialLocalPosX + itemData.MinMaxMovementOffsetX.x, initialLocalPosX + itemData.MinMaxMovementOffsetX.y);
                nextLocalPos.y = Mathf.Clamp(nextLocalPos.y, initialLocalPosY + itemData.MinMaxMovementOffsetY.x, initialLocalPosY + itemData.MinMaxMovementOffsetY.y);

                if(itemData.IsItemMovedBackToInitialPosition)
                    nextLocalPos = Vector3.Lerp(nextLocalPos, transformModel.initialLocalPos, itemData.MoveToInitialPosSpeed * Time.deltaTime);
                
                itemTransform.localPosition = nextLocalPos;
                
                //Movement due to instability
                if (itemEntity.Has<HudItemInstabilityComponent>())
                {

                    nextLocalPos = Vector3.zero;

                    ref var instabilityComponent = ref itemEntity.Get<HudItemInstabilityComponent>();
                    nextLocalPos += GetLocalPositionOffsetDueToItemInstability(itemData, ref instabilityComponent);

                    nextLocalPos *= Time.deltaTime;
                    nextLocalPos += itemTransform.localPosition;

                    nextLocalPos.x = Mathf.Clamp(nextLocalPos.x, initialLocalPosX + itemData.MinMaxInstabilityOffsetX.x, initialLocalPosX + itemData.MinMaxInstabilityOffsetX.y);
                    nextLocalPos.y = Mathf.Clamp(nextLocalPos.y, initialLocalPosY + itemData.MinMaxInstabilityOffsetY.x, initialLocalPosY + itemData.MinMaxInstabilityOffsetY.y);

                    itemTransform.localPosition = nextLocalPos;
                }

                if(itemEntity.Has<HudItemRecoilComponent>())
                {
                    ref var recoilComp = ref itemEntity.Get<HudItemRecoilComponent>();
                    float targetPosZ = transformModel.initialLocalPos.z - recoilComp.currentRecoilPosOffsetZ;

                    nextLocalPos = itemTransform.localPosition;
                    nextLocalPos.z = targetPosZ;

                    itemTransform.localPosition = nextLocalPos;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector3 GetLocalPositionOffsetDueToPlayerRotation(HudItemData itemData, in Vector2 deltaRotation)
        {
            Vector3 positionOffset = Vector3.zero;
            positionOffset.x += -deltaRotation.x * itemData.MoveSpeed;

            if (itemData.IsItemMovedBackToInitialPosition) 
                positionOffset.x = Mathf.Lerp(positionOffset.x, 0, itemData.MoveToInitialPosSpeed * Time.deltaTime);

            return positionOffset;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector3 GetLocalPositionOffsetDueToItemInstability(HudItemData itemData, ref HudItemInstabilityComponent instabilityComponent)
        {
            var targetBobbingSpeed = instabilityComponent.currentInstability * itemData.InstabilityBobbingSpeed / itemData.InitialInstability;

            var positionOffset = Vector3.right * Mathf.Sin(instabilityComponent.horizontalSinTime) * itemData.InstabilityMovementOffset;
            positionOffset += Vector3.up * Mathf.Sin(instabilityComponent.verticalSinTime) * itemData.InstabilityMovementOffset;

            instabilityComponent.verticalSinTime += Time.deltaTime * Random.Range(.5f, 1f) * targetBobbingSpeed;
            instabilityComponent.horizontalSinTime += Time.deltaTime * Random.value * targetBobbingSpeed;

            return positionOffset;
        }
    }
}
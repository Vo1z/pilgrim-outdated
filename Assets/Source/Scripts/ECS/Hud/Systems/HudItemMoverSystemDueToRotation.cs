using Ingame.Input;
using Ingame.Movement;
using Ingame.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Hud
{
    public sealed class HudItemMoverSystemDueToRotation : IEcsRunSystem
    {
        private readonly EcsFilter<HudItemModel, TransformModel, InInventoryTag, HudIsInHandsTag> _itemFilter;
        private readonly EcsFilter<RotateInputRequest> _rotateInputFilter;
        private readonly EcsFilter<PlayerModel> _playerFilter;
        
        private const float ANGLE_FOR_ONE_SCREEN_PIXEL = .01f;
        
        public void Run()
        {
            if(_rotateInputFilter.IsEmpty())
                return;

            var deltaRotation = _rotateInputFilter.Get1(0).rotationInput;
            
            if (!_playerFilter.IsEmpty())
                deltaRotation *= _playerFilter.Get1(0).playerMovementData.Sensitivity * ANGLE_FOR_ONE_SCREEN_PIXEL;
            
            foreach (var i in _itemFilter)
            {
                ref var itemEntity = ref _itemFilter.GetEntity(i);
                ref var hudItemModel = ref _itemFilter.Get1(i);
                ref var transformModel = ref _itemFilter.Get2(i);
                
                var itemData = hudItemModel.itemData;
                var itemTransform = transformModel.transform;
                
                if(!itemData.IsItemMovedDueToRotation || itemEntity.Has<HudIsAimingTag>())
                    continue;

                var initialLocalPosX = transformModel.initialLocalPos.x; 
                var nextLocalPos = itemTransform.localPosition + Vector3.left * deltaRotation.x * itemData.MoveSpeed * Time.deltaTime;
                nextLocalPos.x = Mathf.Clamp(nextLocalPos.x, initialLocalPosX + itemData.MinMaxMovementOffsetY.x, initialLocalPosX + itemData.MinMaxMovementOffsetY.y);

                itemTransform.localPosition = nextLocalPos;
            }
        }
    }
}
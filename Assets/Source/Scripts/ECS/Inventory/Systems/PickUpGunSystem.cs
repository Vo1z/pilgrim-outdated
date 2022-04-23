using Ingame.Gunplay;
using Ingame.Hud;
using Ingame.Interaction.Common;
using Ingame.Movement;
using Leopotam.Ecs;
using Support.Extensions;
using UnityEngine;

namespace Ingame.Inventory
{
    public class PickUpGunSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GunLootComponent, TransformModel, PerformInteractionTag>.Exclude<InHandsTag> _lootGunFilter;
        private readonly EcsFilter<HudModel, TransformModel> _playerHudFilter;
        private readonly EcsFilter<BackpackModel, TransformModel> _backpackFilter;

        private readonly EcsFilter<HudItemModel, FirstHudItemSlotTag> _firstSlotFilter;
        private readonly EcsFilter<HudItemModel, SecondHudItemSlotTag> _secondSlotFilter;

        public void Run()
        {
            if(_playerHudFilter.IsEmpty())
                return;

            ref var playerHudTransformModel = ref _playerHudFilter.Get2(0);
            
            foreach (var i in _lootGunFilter)
            {
                ref var gunLootEntity = ref _lootGunFilter.GetEntity(i);
                ref var gunLootComponent = ref _lootGunFilter.Get1(i);
                ref var gunLootTransform = ref _lootGunFilter.Get2(i);

                ref var gunEntity = ref gunLootComponent.gunEntityReference.Entity;

                bool isThereAnyAvailableSlots = _firstSlotFilter.IsEmpty() || _secondSlotFilter.IsEmpty();
                
                if(!isThereAnyAvailableSlots)
                    continue;

                ProcessLootData(ref gunLootEntity, ref gunLootTransform);
                ProcessGun(ref gunEntity, ref playerHudTransformModel);
            }
        }

        private void ProcessLootData(ref EcsEntity gunLootEntity, ref TransformModel gunLootTransform)
        {
            gunLootEntity.Del<PerformInteractionTag>();
            gunLootTransform.transform.SetGameObjectInactive();
        }

        private void ProcessGun(ref EcsEntity gunEntity, ref TransformModel playerHudTransformModel)
        {
            ref var gunTransformModel = ref gunEntity.Get<TransformModel>();
            ref var gunModel = ref gunEntity.Get<GunModel>();

            var gunTransform = gunTransformModel.transform;
            var handsTransform = gunModel.handsTransform;

            gunTransformModel.initialLocalPos = gunModel.localPositionInsideHud;
            gunTransformModel.initialLocalRotation = gunModel.localRotationInsideHud;
            
            gunTransform.SetParent(playerHudTransformModel.transform);
            gunTransform.localPosition = gunModel.localPositionInsideHud;
            gunTransform.localRotation = gunModel.localRotationInsideHud;
            gunTransform.gameObject.SetLayerToAllChildren(LayerMask.NameToLayer("Weapon"));
            
            handsTransform.SetGameObjectActive();
            handsTransform.gameObject.SetLayerToAllChildren(LayerMask.NameToLayer("HUD"));

            if (!_firstSlotFilter.IsEmpty())
                gunEntity.Get<FirstHudItemSlotTag>();
            else
                gunEntity.Get<SecondHudItemSlotTag>();

            gunEntity.Get<InHandsTag>();

            _backpackFilter.Get2(0).transform.SetGameObjectInactive();
        }
    }
}
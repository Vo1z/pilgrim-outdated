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
                bool hasSlotTag = gunEntity.Has<FirstHudItemSlotTag>() || gunEntity.Has<SecondHudItemSlotTag>(); 
                
                gunLootEntity.Del<PerformInteractionTag>();
                
                if(!isThereAnyAvailableSlots || hasSlotTag)
                    continue;
                
                gunLootTransform.transform.SetGameObjectInactive();
                
                PickUpGun(ref gunEntity, ref playerHudTransformModel);
                
                if (gunEntity.Has<ColliderModel>() && gunEntity.Has<RigidbodyModel>())
                {
                    var gunCollider = gunEntity.Get<ColliderModel>().collider;
                    var gunRigidbody = gunEntity.Get<RigidbodyModel>().rigidbody;
                    
                    DisableGunPhysics(gunCollider, gunRigidbody);
                }
            }
        }
        
        private void PickUpGun(ref EcsEntity gunEntity, ref TransformModel playerHudTransformModel)
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
            
            if (_firstSlotFilter.IsEmpty())
                gunEntity.Get<FirstHudItemSlotTag>();
            else
                gunEntity.Get<SecondHudItemSlotTag>();
            
            if(gunEntity.Has<HudIsVisibleTag>())
                gunEntity.Del<HudIsVisibleTag>();
            
            gunEntity.Get<InHandsTag>();
            gunTransform.SetGameObjectInactive();
        }

        private void DisableGunPhysics(Collider gunCollider, Rigidbody gunRigidbody)
        {
            gunCollider.enabled = false;
            gunCollider.isTrigger = true;

            gunRigidbody.isKinematic = true;
            gunRigidbody.useGravity = false;
        }
    }
}
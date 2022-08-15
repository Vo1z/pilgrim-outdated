using Ingame.Gunplay;
using Ingame.Hud;
using Ingame.Input;
using Ingame.Movement;
using Leopotam.Ecs;
using Support.Extensions;
using UnityEngine;

namespace Ingame.Inventory
{
    public sealed class DropGunSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GunModel, TransformModel, ColliderModel, RigidbodyModel, InHandsTag, HudIsVisibleTag> _gunToDropFilter;
        private readonly EcsFilter<DropGunInputEvent> _dropGunInputEventFilter;

        public void Run()
        {
            if (_dropGunInputEventFilter.IsEmpty())
                return;

            foreach (var i in _gunToDropFilter)
            {
                ref var gunEntity = ref _gunToDropFilter.GetEntity(i);
                ref var gunModel = ref _gunToDropFilter.Get1(i);

                var gunTransform = _gunToDropFilter.Get2(i).transform;

                DeleteGunOddTags(gunEntity);
                SetProperConfigurationForGunGameObject(gunModel, gunTransform);

                if (gunEntity.Has<HudItemModel>())
                {
                    var gunCollider = _gunToDropFilter.Get3(i).collider;
                    var gunRigidbody = _gunToDropFilter.Get4(i).rigidbody;
                    var animator = gunEntity.Get<HudItemModel>().itemAnimator;
                        
                    EnableGunPhysics(gunCollider, gunRigidbody, animator);
                }
            }
        }

        private void DeleteGunOddTags(EcsEntity gunEntity)
        {
            gunEntity.Del<InHandsTag>();
            gunEntity.Del<HudIsVisibleTag>();
                
            if(gunEntity.Has<FirstHudItemSlotTag>())
                gunEntity.Del<FirstHudItemSlotTag>();
                
            if(gunEntity.Has<SecondHudItemSlotTag>())
                gunEntity.Del<SecondHudItemSlotTag>();
        }

        private void SetProperConfigurationForGunGameObject(GunModel gunModel, Transform gunTransform)
        {
            int targetLayer = LayerMask.NameToLayer("IgnoreCollisionWithPlayer");
            
            gunModel.lootDataTransform.SetGameObjectActive();
            gunModel.handsTransform.SetGameObjectInactive();
            
            gunModel.handsTransform.gameObject.SetLayerToAllChildren(targetLayer);
            gunTransform.gameObject.SetLayerToAllChildren(targetLayer);
            
            gunTransform.SetParent(null);
        }

        private void EnableGunPhysics(Collider gunCollider, Rigidbody gunRigidbody, Animator gunAnimator)
        {
            gunCollider.enabled = true;
            gunCollider.isTrigger = false;

            gunRigidbody.isKinematic = false;
            gunRigidbody.useGravity = true;
            
            gunAnimator.enabled = false;
        }
    }
}
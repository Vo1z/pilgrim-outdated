using System.Runtime.CompilerServices;
using Ingame.Health;
using Leopotam.Ecs;
using Support;
using UnityEngine;

namespace Ingame.Gunplay
{
    public sealed class PerformShotSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FirearmComponent, AwaitingShotTag> _shootingFirearmFilter;

        public void Run()
        {
            foreach (var i in _shootingFirearmFilter)
            {
                ref var firearmEntity = ref _shootingFirearmFilter.GetEntity(i);
                ref var firearmComponent = ref _shootingFirearmFilter.Get1(i);
                
                firearmEntity.Del<AwaitingShotTag>();

                TemplateUtils.SafeDebug("Shot");
                
                if (!TryPerformRaycast(firearmComponent.barrelOrigin.position, firearmComponent.barrelOrigin.forward, out RaycastHit hit))
                    return;
                
                if(!TryApplyDamage(hit.collider.gameObject, firearmComponent.damage))
                    return;
                
                //TODO - Viktor: add decal effects
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryPerformRaycast(in Vector3 originPos, in Vector3 direction, out RaycastHit hit)
        {
            var ray = new Ray(originPos, direction);
            int layerMask = ~LayerMask.GetMask("Ignore Raycast", "PlayerStatic", "HUD", "Weapon");

            return Physics.Raycast(ray, out hit, layerMask);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryApplyDamage(GameObject gameObject, float damage)
        {
            damage = Mathf.Max(0, damage);
            
            if (damage <= 0) 
                TemplateUtils.SafeDebug("Damage of the shot is less then 0", LogType.Warning);

            if(!gameObject.TryGetComponent(out EntityReference entityReference))
                return false;
            
            if(!entityReference.Entity.Has<HealComponent>())
                return false;

            ref var appliedDamageComponent = ref entityReference.Entity.Get<DamageComponent>();
            appliedDamageComponent.damageToDeal = damage;

            return true;
        }
    }
}
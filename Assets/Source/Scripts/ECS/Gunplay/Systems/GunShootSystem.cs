using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class GunShootSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GunModel, ShootComponent> _gunInHandsFilter;

        public void Run()
        {
            foreach (var i in _gunInHandsFilter)
            {
                ref var gunEntity = ref _gunInHandsFilter.GetEntity(i);
                ref var gunModel = ref _gunInHandsFilter.Get1(i);

                var hitObject = GetHitObjectWithRayCast(gunModel.barrelTransform);
                
                gunEntity.Del<ShootComponent>();
            }
        }

        private GameObject GetHitObjectWithRayCast(Transform barrelTransform)
        {
            var ray = new Ray(barrelTransform.position, barrelTransform.forward);
            var layerMask = ~LayerMask.GetMask("Ignore Raycast", "HUD", "PlayerStatic", "Weapon");
            
            if (Physics.Raycast(ray, out RaycastHit hit, 200f, layerMask, QueryTriggerInteraction.Ignore))
                return hit.transform.gameObject;
            
            return null;
        }
    }
}
using Ingame.CameraWork;
using Ingame.Enemy;
using Ingame.Health;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Gunplay
{
    public sealed class GunShootSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<GunModel, AwaitingShotTag> _shootingGunFilter;

        private const float MAX_SHOOTING_DISTANCE = 200f;
        
        public void Run()
        {
            foreach (var i in _shootingGunFilter)
            {
                ref var gunEntity = ref _shootingGunFilter.GetEntity(i);
                ref var gunModel = ref _shootingGunFilter.Get1(i);
                var gunData = gunModel.gunData;
                
                ref var cameraShakeReq = ref _world.NewEntity().Get<CameraShakeRequest>();
                cameraShakeReq.duration = gunData.CameraShakeDuration;
                cameraShakeReq.amplitude = gunData.CameraShakeAmplitude;
                cameraShakeReq.frequency = gunData.CameraShakeFrequency;
                
                var hitObject = GetHitObjectWithRayCast(gunModel.barrelTransform);
                gunEntity.Del<AwaitingShotTag>();
                gunEntity.Get<NoiseGeneratorEvent>();
                if(hitObject == null)
                    return;

                if (hitObject.TryGetComponent(out EntityReference entityReference))
                {
                    ref var hitEntity = ref entityReference.Entity;

                    if(hitEntity.Has<HealthComponent>())
                        hitEntity.Get<DamageComponent>().damageToDeal = gunData.Damage;
                }
            }
        }

        private GameObject GetHitObjectWithRayCast(Transform barrelTransform)
        {
            var ray = new Ray(barrelTransform.position, barrelTransform.forward);
            var layerMask = ~LayerMask.GetMask("Ignore Raycast", "HUD", "PlayerStatic", "Weapon");
            
            if (Physics.Raycast(ray, out RaycastHit hit, MAX_SHOOTING_DISTANCE, layerMask, QueryTriggerInteraction.Ignore))
                return hit.transform.gameObject;
            
            return null;
        }
    }
}
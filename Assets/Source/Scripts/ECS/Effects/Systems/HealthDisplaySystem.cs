using Ingame.Health;
using Ingame.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Effects
{
    public sealed class HealthDisplaySystem : IEcsRunSystem
    {
        private const float EFFECTS_LERP_SPEED = 4f;
        
        private readonly EcsFilter<PostProcessingVignetteModel> _vignetteFilter;
        private readonly EcsFilter<PlayerModel, HealthComponent> _playerHealthFilter;

        public void Run()
        {
            if (_playerHealthFilter.IsEmpty() || _vignetteFilter.IsEmpty())
                return;
            
            ref var playerHealth = ref _playerHealthFilter.Get2(0);
            
            foreach (var i in _vignetteFilter)
            {
                ref var vignetteComp = ref _vignetteFilter.Get1(i);
                float vignetteValue = 1 + vignetteComp.initialIntensity - Mathf.InverseLerp(0, playerHealth.initialHealth, playerHealth.currentHealth);
                
                vignetteComp.vignette.intensity.value = Mathf.Lerp(vignetteComp.vignette.intensity.value, vignetteValue, EFFECTS_LERP_SPEED * Time.deltaTime);
            }
        }
    }
}
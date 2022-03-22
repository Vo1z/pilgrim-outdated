using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Health
{
    public sealed class GasChokeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, GasChokeComponent>.Exclude<DeathTag> _gasChokeFilter;

        public void Run()
        {
            foreach (var i in _gasChokeFilter)
            {
                ref var healthComp = ref _gasChokeFilter.Get1(i);
                ref var gasChokeComp = ref _gasChokeFilter.Get2(i);
                
                gasChokeComp.timePassedFromLastGasReleaseFromLungs += Time.deltaTime;

                if(gasChokeComp.timePassedFromLastGasReleaseFromLungs < 1)
                    continue;
                
                gasChokeComp.timePassedFromLastGasReleaseFromLungs = 0;
                
                healthComp.currentHealth -= gasChokeComp.gasDamagePerSecond * gasChokeComp.gasAmountInLungs;
                gasChokeComp.gasAmountInLungs -= gasChokeComp.gasReleasedFromLungsPerSecond;
            }
        }
    }
}
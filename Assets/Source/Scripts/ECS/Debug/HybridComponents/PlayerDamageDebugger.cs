#if UNITY_EDITOR
using Ingame.Health;
using Ingame.Player;
using Leopotam.Ecs;
using NaughtyAttributes;
using UnityEngine;
using Zenject;
#endif

namespace Ingame.Debug.HybridComponents
{
    public sealed class PlayerDamageDebugger : MonoBehaviour
    {
#if UNITY_EDITOR
        [BoxGroup("Damage")] 
        [SerializeField] [Range(0, 100)] private float appliedDamage = 10f;
        [BoxGroup("Bleeding")] 
        [SerializeField] [Range(0, 100)] private float healthTakenPerSecondFromBleeding = 10f;
        
        [Inject] private readonly EcsWorld _world;
        
        [Button]
        private void DealDamage()
        {
            ref var playerEntity = ref _world.GetFilter(typeof(EcsFilter<PlayerModel, HealthComponent>)).GetEntity(0);
            ref var bleedingComponent = ref playerEntity.Get<DamageComponent>();

            bleedingComponent.damageToDeal = appliedDamage;
        }

        [Button]
        private void AddBleeding()
        {
            ref var playerEntity = ref _world.GetFilter(typeof(EcsFilter<PlayerModel, HealthComponent>)).GetEntity(0);
            ref var bleedingComponent = ref playerEntity.Get<BleedingComponent>();

            bleedingComponent.healthTakenPerSecond = healthTakenPerSecondFromBleeding;
        }
#endif
    }
}
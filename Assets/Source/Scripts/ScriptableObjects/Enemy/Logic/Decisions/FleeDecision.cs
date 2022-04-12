using Ingame.Enemy.State;
using Ingame.Health;
using Leopotam.Ecs;
using NaughtyAttributes;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/Flee", fileName = "FleeDecision")]
    public sealed class FleeDecision : DecisionBase
    {
        [SerializeField][Min(0)][MaxValue(1)]
        private float healthThreshold = .15f;
        public override bool Decide(ref EcsEntity entity)
        {
            if (!entity.Has<HealthComponent>())
            {
                return false;
            }

            ref var health = ref entity.Get<HealthComponent>();
            if (health.currentHealth/health.initialHealth < healthThreshold)
            {
                entity.Get<FleeStateTag>();
                return true;
            }

            return false;
        }
    }
}
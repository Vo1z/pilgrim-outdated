using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/ContinuousAttack", fileName = "ContinuousAttack")]
    public sealed class ContinuousAttackDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            entity.Get<ContinuousAttackTimerRequest>();
            entity.Get<AttackStateTag>();
            return true;
        }
    }
}
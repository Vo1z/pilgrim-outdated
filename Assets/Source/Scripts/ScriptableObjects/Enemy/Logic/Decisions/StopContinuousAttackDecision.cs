using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/StopContinuousAttack", fileName = "StopContinuousAttack")]
    public sealed class StopContinuousAttackDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            return !entity.Has<ContinuousAttackTimerRequest>();
        }
    }
}
using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;
 

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/AttackOnPeek", fileName = "AttackOnPeek")]
    public sealed class AttackOnPeekDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            entity.Get<AttackStateTag>();
            entity.Get<EnemyLeanTag>();
            return true;
        }
    }
}
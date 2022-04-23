using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/State/AttackOnPeek", fileName = "AttackOnPeek")]
    public class AttackOnPeekState : StateBase
    {
        protected override void DeleteCurrentStateTag(ref EcsEntity entity)
        {
            entity.Del<AttackStateTag>();
            if (entity.Has<EnemyLeanTag>())
            {
                entity.Del<EnemyLeanTag>();
            }
        }
    }
}
using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/State/Attack", fileName = "AttackState")]
    public sealed class AttackState : StateBase
    {
        protected override void DeleteCurrentStateTag(ref EcsEntity entity)
        {
            entity.Del<AttackStateTag>();
            if (entity.Has<PeekStateTag>())
            {
                entity.Del<PeekStateTag>();
            }
        }
    }
}
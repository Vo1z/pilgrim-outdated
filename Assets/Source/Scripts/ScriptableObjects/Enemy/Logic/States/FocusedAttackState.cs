using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/State/FocusedAttack", fileName = "FocusedAttack")]
    public sealed class FocusedAttackState : StateBase
    {
        protected override void DeleteCurrentStateTag(ref EcsEntity entity)
        {
            entity.Del<AttackStateTag>();
        }
    }
}
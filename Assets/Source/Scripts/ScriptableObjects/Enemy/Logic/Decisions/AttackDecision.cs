
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/Attack", fileName = "AttackDecision")]
    public sealed class AttackDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {

            return false;
        }
    }
}
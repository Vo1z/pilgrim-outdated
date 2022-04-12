
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/Patrol", fileName = "PatrolDecision")]
    public sealed class PatrolDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            
            return false;
        }
    }
}
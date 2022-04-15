using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/Reposition", fileName = "Reposition")]
    public sealed class RepositionDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            entity.Get<RepositionStateTag>();
            return true;
        }
    }
}
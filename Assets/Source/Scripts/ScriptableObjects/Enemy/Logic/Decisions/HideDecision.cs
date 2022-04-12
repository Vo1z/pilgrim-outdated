
using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/Hide", fileName = "HideDecision")]
    public sealed class HideDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            if(!entity.Has<HideBlockOnPeekComponent>())
            {
                entity.Get<HideBlockOnTakeCoverComponent>();
                entity.Get<HideStateTag>();
                return true;
            }

            return false;
        }
    }
}
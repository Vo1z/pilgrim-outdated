using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/PeekFromCover", fileName = "PeekFromCover")]
    public sealed class PeekFromCoverDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            if(!entity.Has<HideBlockOnTakeCoverComponent>())
            {
                entity.Get<HideBlockOnPeekComponent>();
                entity.Get<PeekStateTag>();
                
                return true;
            }

            return false;
        }
    }
}
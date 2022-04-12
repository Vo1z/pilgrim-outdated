using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/State/PeekFromCover", fileName = "PeekFromCover")]
    public sealed class PeekFromCoverState : StateBase
    {
        protected override void DeleteCurrentStateTag(ref EcsEntity entity)
        {
            entity.Del<PeekStateTag>();
        }
        
    }
}
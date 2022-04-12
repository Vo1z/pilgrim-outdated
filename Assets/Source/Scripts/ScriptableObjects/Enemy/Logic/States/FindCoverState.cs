using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/State/FindCover", fileName = "FindCover")]
    public sealed class FindCoverState : StateBase
    {
        protected override void DeleteCurrentStateTag(ref EcsEntity entity)
        {
            entity.Del<FindCoverStateTag>();
        }

        protected override bool IsNotBlocked(ref EcsEntity entity)
        {
            return !entity.Has<HideBlockTag>();
        }
    }
}
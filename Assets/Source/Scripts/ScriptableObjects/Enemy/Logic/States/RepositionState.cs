using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/State/Reposition", fileName = "Reposition")]
    public sealed class RepositionState : StateBase
    {
        protected override void DeleteCurrentStateTag(ref EcsEntity entity)
        {
            entity.Del<RepositionStateTag>();
        }
    }
}
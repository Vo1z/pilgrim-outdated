using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/State/Hide", fileName = "HideState")]
    public sealed class HideState : StateBase
    {
        protected override void DeleteCurrentStateTag(ref EcsEntity entity)
        {
            entity.Del<HideStateTag>();
        }
    }
}
using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/State/Hide", fileName = "HideState")]
    public class HideState : StateBase
    {
        protected override void DeleteCurrentStateTag(ref EcsEntity entity)
        {
            entity.Del<HideStateTag>();
        }
        
        protected override bool IsNotBlocked(ref EcsEntity entity)
        {
            return !entity.Has<HideInProgressTag>();
        }
    }
}
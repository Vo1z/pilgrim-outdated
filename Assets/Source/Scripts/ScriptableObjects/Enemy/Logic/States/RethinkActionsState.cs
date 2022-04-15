using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/State/Rethink", fileName = "Rethink")]
    public sealed class RethinkActionsState : StateBase
    {

        protected override void DeleteCurrentStateTag(ref EcsEntity entity)
        {
        }
    }
}
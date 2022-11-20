using Ingame.Behaviour;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy
{
    public class IsTargetVisibleNode : ActionNode
    {
        [SerializeField] private int thresholdOfVisibility;
        protected override void ActOnStart()
        {
            Entity.Get<EnemyUseCameraRequest>();
        }

        protected override void ActOnStop()
        {
            
        }

        protected override State ActOnTick()
        {
            if (Entity.Has<EnemyUseCameraRequest>())
            {
                return State.Running;
            }

            ref var enemyModel = ref Entity.Get<EnemyStateModel>();
            return thresholdOfVisibility <= enemyModel.VisibleTagretPixels ? State.Success : State.Failure;
        }
    }
}
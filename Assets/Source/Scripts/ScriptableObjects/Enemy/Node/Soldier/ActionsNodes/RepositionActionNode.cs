using Ingame.Behaviour;
using Leopotam.Ecs;

namespace Ingame.Enemy
{
    public class RepositionActionNode : ActionNode
    {
        protected override void ActOnStart()
        {
            /*Entity.Get<EnemyStateModel>().Cover;
            Entity.Ge*/
        }

        protected override void ActOnStop()
        {
          
        }

        protected override State ActOnTick()
        {
            return State.Success;
        }
    }
}
using Ingame.Behaviour;

namespace Ingame.Enemy
{
    public class RunAwayActionNode : ActionNode

    {
        protected override void ActOnStart()
        {
             
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
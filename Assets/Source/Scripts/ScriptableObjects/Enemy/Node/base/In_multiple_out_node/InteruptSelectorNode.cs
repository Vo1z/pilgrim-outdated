namespace Ingame.Behaviour
{
    public class InteruptSelectorNode : SelectorNode
    {

        protected State OnUpdate()
        {
            var previous = currentIndex;
            base.ActOnStart();
            var status = base.Tick();
            if (previous == currentIndex) return status;
            
            if (Children[previous].CurrentState == State.Running)
            {
                Children[previous].Abort();
            }

            return status;
        }
    }
}
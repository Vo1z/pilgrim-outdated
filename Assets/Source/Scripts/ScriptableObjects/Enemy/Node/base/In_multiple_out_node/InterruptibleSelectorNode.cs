namespace Ingame.Behaviour
{
    public class InterruptibleSelectorNode : SelectorNode
    {
        protected override State ActOnTick()
        {
            int last = currentIndex;
            base.ActOnStart();
            var state = base.ActOnTick();
            //keep going
            if (last == currentIndex) return state;
            //abort the last actions
            if (Children[last].CurrentState == State.Running)
            {
                Children[last].Abort();
            }

            return state;
        }
    }
}
using System;
using UnityEngine;

namespace Ingame.Behaviour
{
 
    public class SequenceNode : CompositeNode
    {
        private int _currentNodeIndex = 0;
        protected override void ActOnStart()
        {
            _currentNodeIndex = 0;
        }

        protected override void ActOnStop()
        {
             
        }

        protected override State ActOnTick()
        {
            var currentNode = Children[_currentNodeIndex];
            switch (currentNode.Tick())
            {
                case State.Running:
                    return State.Running;
                case State.Failure:
                    return State.Failure;
                case State.Success:
                    _currentNodeIndex++;
                    break;
                case State.Abandon:
                    return State.Abandon;
            }

            return _currentNodeIndex == Children.Count?State.Success: State.Running;
        }
    }
}
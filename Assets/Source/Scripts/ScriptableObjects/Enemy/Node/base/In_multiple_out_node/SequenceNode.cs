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
            if (_currentNodeIndex == Children.Count)
            {
                return State.Success;
            }
            var currentNode = Children[_currentNodeIndex];
            switch (currentNode.Tick())
            {
                case State.Running:
                    return State.Running;
                case State.Failure:
                    return State.Failure;
                case State.Success:
                    _currentNodeIndex++;
                    return ActOnTick();
                case State.Abandon:
                    return State.Abandon;
            }

            return /*_currentNodeIndex == Children.Count?State.Success:*/ State.Running; 
            /*for (var i = _currentNodeIndex; i < Children.Count; i++) {
                _currentNodeIndex = i;
                var child = Children[i];

                switch (child.Tick()) {
                    case State.Running:
                        return State.Running;
                    case State.Success:
                        return State.Success;
                    case State.Abandon:
                        return State.Abandon;
                    case State.Failure:
                        continue;
                }
            }
            return State.Failure;*/
        }
    }
}
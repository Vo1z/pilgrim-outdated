using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Behaviour
{
    public class RepeatNode : DecoratorNode
    {
        private enum TypeOfRepetition
        {
            Infinite, 
            Fixed,
            TillStateChange
        }

        [SerializeField] private TypeOfRepetition type;

        [ShowIf("type == TypeOfRepetition.Fixed")]
        [SerializeField] 
        private int loops = 1;
        
        protected override void ActOnStart()
        {
            
        }

        protected override void ActOnStop()
        {
             
        }

        protected override State ActOnTick()
        {
           Child.Tick();
           return State.Running;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Ingame.Behaviour;
using UnityEngine;

namespace Ingame.Behaviour.Test
{
    public class DebugActionNode : ActionNode
    {
        protected override void ActOnStart()
        {
           UnityEngine.Debug.Log($"Started");
        }

        protected override void ActOnStop()
        {
            UnityEngine.Debug.Log($"Stopped");
        }

        protected override State ActOnTick()
        {
            UnityEngine.Debug.Log($"Tick");
            return State.Success;
        }
    }
    
}
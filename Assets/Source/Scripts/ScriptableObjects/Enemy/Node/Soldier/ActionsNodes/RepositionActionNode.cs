using Ingame.Behaviour;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Ingame.Enemy
{
    public class RepositionActionNode : ActionNode
    {
        private NavMeshAgent _agent;
        protected override void ActOnStart()
        {
            _agent = Entity.Get<NavMeshAgentModel>().Agent;
            _agent.isStopped = false;
        }

        protected override void ActOnStop()
        {
            _agent.isStopped = true;
        }

        protected override State ActOnTick()
        {
            #if UNITY_EDITOR
                UnityEngine.Debug.DrawLine(_agent.transform.position,_agent.destination,Color.green);
            #endif
            if (_agent.pathPending)
            {
                return State.Running; 
            }
            
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                return State.Success;
            }
            
            return _agent.pathStatus == NavMeshPathStatus.PathInvalid ? State.Failure : State.Running;
        }
    }
}
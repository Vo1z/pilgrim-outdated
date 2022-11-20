using System.Collections.Generic;
using Ingame.Behaviour;
using Ingame.Behaviour.Test;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Ingame.Enemy
{
    public class FlankActionNode : ActionNode
    {
        [SerializeField] 
        private TypeOfCover typeOfCover;

        [SerializeField] 
        [Min(0)] 
        private float safeDistance;
        
        [SerializeField] 
        [Min(0)] 
        private float minDistance;

        [SerializeField]
        [Min(0)]
        private float maxDistance;
        protected override void ActOnStart()
        {
            
        
        }

        protected override void ActOnStop()
        {
           
        }

        protected override State ActOnTick()
        {     
            ref var navAgent = ref Entity.Get<NavMeshAgentModel>();
            ref var enemy = ref Entity.Get<EnemyStateModel>();
            
            Transform targetPoint = null;
            var path = new NavMeshPath();
            var backPoint = enemy.Target.position - enemy.Target.forward * safeDistance;
            //trace is not valid
            if (!NavMesh.CalculatePath(navAgent.Agent.transform.position, backPoint, navAgent.Agent.areaMask, path))
            {
                return State.Failure;
            }
            var fullDistance = CalculateRealDistance(navAgent.Agent.transform.position, path.corners);
            
            var colliders = Physics.OverlapSphere(navAgent.Agent.transform.position, maxDistance );
            for (int i = 0; i < colliders.Length; i++)
            {
                var coverPosition = colliders[i];
                if (!coverPosition.transform.gameObject.CompareTag("CoverPoint") || 
                    Vector3.Distance(coverPosition.transform.position,enemy.Target.position)<minDistance ||   
                    Vector3.Distance(coverPosition.transform.position,navAgent.Agent.transform.position)<minDistance )
                {
                    continue;
                }
                
                path = new NavMeshPath();
                if (!NavMesh.CalculatePath(coverPosition.transform.position, backPoint, navAgent.Agent.areaMask, path))
                {
                    continue;
                }
                
                var distance = CalculateRealDistance(coverPosition.transform.position, path.corners);
                if (distance >= fullDistance)
                {
                    continue;
                }
                
                fullDistance = distance;
                targetPoint = coverPosition.transform;
            }
            
            
            if (colliders.Length <=0 || targetPoint == null)
            {
                //todo better destination tracking
                var v = (backPoint);
                navAgent.Agent.destination = v;
                return State.Success;
            }
            navAgent.Agent.destination = targetPoint.position + (targetPoint.position-enemy.Target.position).normalized;
            return State.Success;
        }

        private float CalculateRealDistance(Vector3 position, Vector3[] corners)
        {
            float dist = Vector3.Distance(position, corners[0]);
            for (var j = 1; j < corners.Length; j++)
            {
                dist += Vector3.Distance(corners[j - 1], corners[j]);
            }
            return dist;
        }
    }
}
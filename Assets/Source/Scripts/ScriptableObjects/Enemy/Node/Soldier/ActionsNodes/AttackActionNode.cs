﻿using Ingame.Behaviour;
using Ingame.Health;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ingame.Enemy
{
    public class AttackActionNode : ActionNode
    {

        [SerializeField] 
        private float damageOnHit;
        [SerializeField] 
        [Range(0, 1)] 
        private float chanceToHit = 0.9f;
        [SerializeField] 
        private float shootIntervalTime = 1.5f;
        [SerializeField] 
        private LayerMask ignoredLayers;

        private float _currentIntervalTime;
        protected override void ActOnStart()
        {
            _currentIntervalTime = shootIntervalTime;
        }

        protected override void ActOnStop()
        {
          
        }

        /// <summary>
        /// Try to attack after [shootIntervalTime] time
        /// </summary>
        /// <returns>Return Success if hit target, Failure if not and Running if it's still on a cooldown</returns>
        protected override State ActOnTick()
        {
            //cooldown
            if (_currentIntervalTime>0)
            {
                _currentIntervalTime -= Time.deltaTime;
                return State.Running;
            }
            
            //hit chance - do miss
            if (chanceToHit < Random.Range(0f, 1f))
            {
                return State.Failure;
            }
            
            ref var enemyModel = ref Entity.Get<EnemyStateModel>();
            ref var transform = ref Entity.Get<TransformModel>();

            //shoot
            if (!Physics.Linecast(transform.transform.position, enemyModel.Target.position, out RaycastHit hit, ignoredLayers, QueryTriggerInteraction.Ignore))
            {
                return State.Failure;
            }

            if (!hit.transform.root.CompareTag("Player")) return State.Failure;
            
            hit.transform.root.TryGetComponent(out EntityReference reference);
            reference.Entity.Get<HealthComponent>().currentHealth -= damageOnHit;
            return State.Success;

        }
    }
}
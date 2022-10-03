using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ingame.Behaviour;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy
{
    public class DetectNoiseActionNode : ActionNode
    {
        [SerializeField] private float noiseDetectionRange = 10;
        private Transform _position;
        protected override void ActOnStart()
        {
             _position = Entity.Get<TransformModel>().transform;
        }

        protected override void ActOnStop()
        {
            
        }

        protected override State ActOnTick()
        {
            ref var enemy = ref Entity.Get<EnemyStateModel>();
            //noise is not detected yet
            if (enemy.NoisePosition == null)
            {
                (Vector3, float) p;
                enemy.LastRememberedNoises ??= new List<Vector3>();
               
                foreach (var i in enemy.LastRememberedNoises)
                {
                    var dist = Vector3.Distance(i, _position.position);
                    if ( dist< noiseDetectionRange)
                    {
                        p = (i, dist);
                    }
                }
            }
           
            //Noise Detection
            return State.Running;
        }
    }
}
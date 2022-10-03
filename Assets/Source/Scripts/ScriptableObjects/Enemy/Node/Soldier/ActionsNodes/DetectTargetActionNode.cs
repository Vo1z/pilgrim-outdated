using Ingame.Behaviour;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy
{
    public class DetectTargetActionNode : ActionNode
    {
        [SerializeField] private float detectionRange = 45f;
        [SerializeField] private float detectionAngle = 55;
        //[SerializeField] private float timeToBeDetected = 1.5f;
        [SerializeField] private LayerMask ignoredLayers;
        
        private Transform _transform;
        private Transform _target;
        private float _time;
        protected override void ActOnStart()
        { 
            _transform =  Entity.Get<TransformModel>().transform;
            ref var enemyModel = ref Entity.Get<EnemyStateModel>();
            _target =  enemyModel.Target;
            
            //_time = timeToBeDetected;
        }

        protected override void ActOnStop()
        {
           
        }
    
        protected override State ActOnTick()
        {
            Entity.Get<EnemyUseCameraRequest>();
            return State.Running;
            /*if (Entity.Get<EnemyStateModel>().IsTargetDetected)
            {
                return State.Success;
            }
            
            //range
            if ((_target.position - _transform.position).sqrMagnitude > detectionRange * detectionRange)
            {
                return State.Failure;
            }

            //Angle
            var dir = _target.position - _transform.position;
            dir.y = 0;
            var deltaAngle = Vector3.Angle(dir, _transform.forward);
            if (deltaAngle >= detectionAngle || deltaAngle < 0)
            {
                return State.Failure;
            }

            /#1#/ not covered
            if (!Physics.Linecast(_transform.position, _target.position, out RaycastHit hit,ignoredLayers,QueryTriggerInteraction.Ignore))
                return State.Failure;#1#

            /*
            if (_time>0)
            {
                _time -= Time.deltaTime;
                return State.Running;
            }#1#
            if ( Entity.Has<EnemyUseCameraRequest>())
            {
                return State.Running;
            }
            Entity.Get<EnemyUseCameraRequest>();
            return State.Running;*/
        }
    }
}
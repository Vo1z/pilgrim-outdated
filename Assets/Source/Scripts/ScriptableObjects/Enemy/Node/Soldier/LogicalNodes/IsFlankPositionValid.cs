using Ingame.Behaviour;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy
{
    public sealed class IsFlankPositionValid : ActionNode
    {
        [SerializeField] private float playerVisionAngle = 90;
        [SerializeField] private float  playerBackRangeAngle = 40;
        protected override void ActOnStart()
        {
             
        }

        protected override void ActOnStop()
        {
             
        }

        protected override State ActOnTick()
        {
            ref var transformModel  = ref Entity.Get<TransformModel>();
            ref var enemyModel = ref Entity.Get<EnemyStateModel>();
         
            //
            var dir = enemyModel.Target.position - transformModel.transform.position;
            dir.y = 0;
            dir = dir.normalized;
            
            //angle between enemy and back of player - player sees
            var deltaAngle = Vector3.Angle(dir, enemyModel.Target.forward);
            if (deltaAngle >= playerBackRangeAngle || deltaAngle < 0)
            {
                return State.Failure;
            }
            //angle between enemy and back of player - player can not see 
            /*deltaAngle = Vector3.Angle(dir, -enemyModel.Target.forward);
            if (deltaAngle >= playerBackRangeAngle || deltaAngle < 0)
            {
                return State.Success;
            }*/
            return State.Success;
        }
    }
}
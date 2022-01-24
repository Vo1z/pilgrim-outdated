using System.Collections;
using System.Collections.Generic;
using Ingame.Enemy.ECS;
using Ingame.Enemy.ECS.Ingame.Enemy.ECS;
using Ingame.Enemy.Logic;
using LeoEcsPhysics;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Decision/Follow", fileName = "EnemyDecisionFollow")]
    public class DecisionFollow : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {

            /*if (!entity.Has<TransformModel>() || !entity.Has<VisionComponent>())
                return false;
            ref var position = ref entity.Get<TransformModel>();
            ref var vision = ref entity.Get<VisionComponent>();
            var colliders = Physics.OverlapSphere(position.transform.position,vision.Range);
            
            foreach (var i in colliders)
            {
               
                if (i.TryGetComponent(out TargetableTagProvider target)&& i.TryGetComponent(out TransformModelProvider trans))
                {
                    ref var locate = ref entity.Get<LocateTargetComponent>();
                    locate.Target = trans.transform;
                    return true;
                }
            }*/

            return false;
        }
    }
}

using System;
using Ingame.Enemy.State;
using Ingame.Enemy.System;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/Hide", fileName = "HideDecision")]
    public class HideDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            ref var hideModel = ref entity.Get<HideModel>();
            ref var target = ref entity.Get<LocateTargetComponent>();
            ref var enemy = ref entity.Get<TransformModel>();
            ref var vision = ref entity.Get<VisionModel>();
            if (target.Target == null)
            {
                return false;
            }
            if (Vector3.Distance(target.Target.position,enemy.transform.position)<hideModel.HideData.SafeDistance && !entity.Has<HideBlockComponent>())
            {
 
                if (vision.Covers.Count == 0)
                {
                    return false;
                }
                var obstacle = vision.Covers[0];
                //simplify a shortest distance finder 
                var shortestDist = Mathf.Abs( vision.Covers[0].position.z-enemy.transform.position.z) + Mathf.Abs(vision.Covers[0].position.x-enemy.transform.position.x);
                foreach (var i in vision.Covers)
                {
                    var dist =  Mathf.Abs( i.position.z-enemy.transform.position.z) + Mathf.Abs(i.position.x-enemy.transform.position.x);
                    if (dist<shortestDist)
                    {
                        shortestDist = dist;
                        obstacle = i;
                    }
                } 
                hideModel.Obstacle = obstacle;
                entity.Get<HideStateTag>();
                return true;
            }
            return false;
        }
    }
}
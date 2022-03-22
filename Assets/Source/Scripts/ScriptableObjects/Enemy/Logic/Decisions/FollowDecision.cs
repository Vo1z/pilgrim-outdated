using System.Numerics;
using Ingame.Enemy.Extensions;
using Ingame.Enemy.State;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/Follow", fileName = "FollowDecision")]
    public class FollowDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            ref var vision = ref entity.Get<VisionModel>();
     
            if (entity.CanDetectTarget(0, vision.Vision.MaxDistance))
            {
                ref var position = ref entity.Get<TransformModel>();
                ref var target = ref entity.Get<LocateTargetComponent>();
                ref var hideModel = ref entity.Get<HideModel>();
                if (Physics.Linecast(position.transform.position,target.Target.position, out RaycastHit hit))
                {
                    Debug.Log(hit.collider.tag);
                   var cover= hit.collider.gameObject.transform.position;
                   if ((Vector3.Distance(cover, position.transform.position) < hideModel.HideData.MaxDistanceBetweenThisAndCover))
                   {
                       return false;
                   }
                }
                entity.Get<FollowStateTag>();
                return true;
            }
            return false;
        }
    }
    
}
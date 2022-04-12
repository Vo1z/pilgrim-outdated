 
using Ingame.Enemy.State;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/Follow", fileName = "FollowDecision")]
    public sealed class FollowDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {

            if (!entity.Has<LocateTargetComponent>() || !entity.Has<TransformModel>()|| !entity.Has<VisionBinderComponent>())
            {
                return false;
            }
            ref var binder = ref entity.Get<VisionBinderComponent>();

            if (!binder.FarRange.TryGetComponent(out EntityReference farEntityReference))
            {
                return false;
            }
            ref var target = ref entity.Get<LocateTargetComponent>();
            ref var transformModel = ref entity.Get<TransformModel>();
            ref var shortRef = ref farEntityReference.Entity;
            ref var vision = ref shortRef.Get<VisionModel>();
            if (vision.Vision.MaxDistance<Vector3.Distance(target.Target.position,transformModel.transform.position))
            {
                entity.Get<FollowStateTag>();
                return true;
            }

            return false;
        }
    }
    
}
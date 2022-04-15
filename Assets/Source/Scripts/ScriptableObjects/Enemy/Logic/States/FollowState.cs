using Ingame.Enemy.State;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/State/Follow", fileName = "FollowState")]
    public sealed class FollowState : StateBase
    {
        protected override void DeleteCurrentStateTag(ref EcsEntity entity)
        {
            entity.Del<FollowStateTag>();
        }
        
        protected override bool IsNotBlocked(ref EcsEntity entity)
        {
            
            if (!entity.Has<LocateTargetComponent>() || !entity.Has<TransformModel>()|| !entity.Has<VisionBinderComponent>())
            {
                return true;
            }
            ref var binder = ref entity.Get<VisionBinderComponent>();

            if (!binder.FarRange.TryGetComponent(out EntityReference farEntityReference))
            {
                return true;
            }
            ref var target = ref entity.Get<LocateTargetComponent>();
            ref var transformModel = ref entity.Get<TransformModel>();
            ref var shortRef = ref farEntityReference.Entity;
            ref var vision = ref shortRef.Get<VisionModel>();
            var distance = Vector3.Distance(target.Target.position, transformModel.transform.position);
            if (vision.Vision.Distance>distance)
            {
                return true;
            }
            if (!vision.Opponents.Contains(target.Target))
            {
                return true;
            }
            return false;
        }
    }
}
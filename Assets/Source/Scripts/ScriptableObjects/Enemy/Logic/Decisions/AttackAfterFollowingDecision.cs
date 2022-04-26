using Ingame.Enemy.State;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/AttackAfterFollowingDecision", fileName = "AttackAfterFollowingDecision")]
    public class AttackAfterFollowingDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            if (!entity.Has<LocateTargetComponent>() || !entity.Has<TransformModel>()|| !entity.Has<VisionBinderComponent>())
            {
                return false;
            }
            ref var binder = ref entity.Get<VisionBinderComponent>();

            if (!binder.ShortRange.TryGetComponent(out EntityReference shortEntity))
            {
                return false;
            }
            ref var target = ref entity.Get<LocateTargetComponent>();
            ref var transformModel = ref entity.Get<TransformModel>();
            ref var shortRef = ref shortEntity.Entity;
            ref var vision = ref shortRef.Get<VisionModel>();

            if (Vector3.Distance(target.Target.position,transformModel.transform.position) > vision.Vision.Distance)
            {
                return false;
            }
            entity.Get<ContinuousAttackTimerRequest>();
            entity.Get<AttackStateTag>();
            return true;
        }
    }
}
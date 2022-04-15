
using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/Patrol", fileName = "PatrolDecision")]
    public sealed class PatrolDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            if (!entity.Has<VisionBinderComponent>())
            {
                return false;
            }

            //Get entity of Layers
            ref var binder = ref entity.Get<VisionBinderComponent>();

            if (!binder.FarRange.TryGetComponent(out EntityReference farEntityReference))
            {
                return false;
            }
            ref var farRef = ref farEntityReference.Entity;
            ref var vision = ref farRef.Get<VisionModel>();
            ref var target = ref entity.Get<LocateTargetComponent>();
            ref var movement = ref entity.Get<EnemyMovementComponent>();
            
            if (target.Target == null)
            {
                return false;
            }
            
            if (vision.Opponents.Contains(target.Target))
            {
                return false;
                
            }

            var vec = new Vector3(target.Target.position.x, target.Target.position.y, target.Target.position.z);
            movement.Waypoint = vec;
            target.Target = null;
            entity.Get<PatrolStateTag>();
            return true;
        }
    }
}
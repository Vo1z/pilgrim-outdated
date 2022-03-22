using Ingame.Enemy.State;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Extensions
{
    public static class DecisionExtensions
    {
        public static bool CanDetectTarget(ref this EcsEntity entity, float minDistance, float maxDistance)
        {
            if (!entity.Has<TransformModel>()||!entity.Has<VisionModel>()||!entity.Has<LocateTargetComponent>())
            {
                return false;
            }
            
            ref var transformModel = ref entity.Get<TransformModel>();
            ref var vision = ref entity.Get<VisionModel>();
            ref var targetComponent = ref entity.Get<LocateTargetComponent>();

            foreach (var item in vision.Opponents)
            {
                var dist = Vector3.Distance(item.transform.position, transformModel.transform.position);
                Vector3 direction = item.transform.position - transformModel.transform.position;
                direction.y = 0;
                float deltaAngle = Vector3.Angle(direction, transformModel.transform.forward);
               
                if ((deltaAngle > vision.Vision.Angle || deltaAngle < 0)||!(dist < maxDistance && dist > minDistance))
                {
                    continue;
                }
                var originY = transformModel.transform.position.y + vision.Vision.Height / 2;
                var origin = new Vector3(transformModel.transform.position.x, originY, transformModel.transform.position.z);
                var dest = new Vector3(item.transform.position.x, originY, item.transform.position.z);

                if (!Physics.Linecast(origin, dest, vision.Vision.Mask))
                {
                    if (!item.CompareTag("Player"))
                    {
                        return false;
                    }
                    targetComponent.Target = item;
                    return true;
                }
            }
            return false;
        }
    }
}
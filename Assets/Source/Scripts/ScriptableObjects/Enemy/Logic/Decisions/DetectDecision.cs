 
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/Detect", fileName = "DetectDecision")]
    public sealed class DetectDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
           
            if (!entity.Has<VisionBinderComponent>())
            {
                return false;
            }

            //Get entity of Layers
            ref var binder = ref entity.Get<VisionBinderComponent>();

            if (!binder.ShortRange.TryGetComponent(out EntityReference shortEntityReference)||!binder.FarRange.TryGetComponent(out EntityReference farEntityReference))
            {
                return false;
            }
            
            ref var shortRef = ref shortEntityReference.Entity;
            ref var farRef = ref farEntityReference.Entity;
            
            ref var transformModel = ref entity.Get<TransformModel>();
            ref var targetLock = ref entity.Get<LocateTargetComponent>();
            //Check if any target is in any ranges

            if (!shortRef.Has<VisionModel>()|| !farRef.Has<VisionModel>())
            {
                return false;
            }

            ref var vision = ref farRef.Get<VisionModel>();
            foreach (var j in vision.Opponents)
            {
                if (vision.Vision != null)
                {
                    var dist = Vector3.Distance(j.position, transformModel.transform.position);
                    Vector3 direction = j.position - transformModel.transform.position;
                    direction.y = 0;
                    float deltaAngle = Vector3.Angle(direction, transformModel.transform.forward);
                    if (deltaAngle >= vision.Vision.Angle || deltaAngle < 0)
                    {
                        continue;
                    }

                    if (dist> vision.Vision.Distance)
                    {
                        continue;
                    }
                }
                
                //not behind cover
                if (Physics.Linecast(transformModel.transform.position, j.position, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.CompareTag("Player"))
                    {
                        targetLock.Target = j.transform;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
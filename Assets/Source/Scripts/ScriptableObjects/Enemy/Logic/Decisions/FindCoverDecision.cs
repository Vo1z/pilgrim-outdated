using Leopotam.Ecs;
using Ingame.Enemy.State;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/FindCover", fileName = "FindCover")]
    public sealed class FindCoverDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            if (entity.Has<HideBlockTag>())
            {
                return false;
            }

            if (!entity.Has<VisionBinderComponent>())
            {
                return false;
            }

            //Get entity of Layers
            ref var binder = ref entity.Get<VisionBinderComponent>();
            if (!binder.ShortRange.TryGetComponent(out EntityReference shortEntityReference))
            {
                return false;
            }
            
            ref var shortRange = ref shortEntityReference.Entity;
            ref var target = ref entity.Get<LocateTargetComponent>();
            if (!shortRange.Has<VisionModel>())
            {
                return false;
            }
            ref var vision = ref shortRange.Get<VisionModel>();
            //Find the nearest cover
            foreach (var i in vision.Covers)
            {
                if (Physics.Linecast(i.position,target.Target.position,out RaycastHit hit))
                {
                    if (hit.collider.CompareTag("Terrain"))
                    {
                        ref var hideModel = ref entity.Get<HideModel>();
                        hideModel.Obstacle = i;
                        entity.Get<FindCoverStateTag>();
                        entity.Get<HideBlockTag>();
                        return true;
                    }
                }
                
            }
            return false;
        }
    }
}
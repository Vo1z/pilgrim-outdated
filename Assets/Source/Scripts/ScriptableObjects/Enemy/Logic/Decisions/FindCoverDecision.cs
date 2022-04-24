using System.Linq;
using Ingame.Cover;
using Leopotam.Ecs;
using Ingame.Enemy.State;
using Ingame.Movement;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/FindCover", fileName = "FindCover")]
    public sealed class FindCoverDecision : DecisionBase
    {
        private float _distanceRatio = 1.2f;
        private float _maxDistanceObstacle = 25;
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
            if (!entity.Has<HideModel>())
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
            ref var transformModel = ref entity.Get<TransformModel>();
            if (!shortRange.Has<VisionModel>())
            {
                return false;
            }
            ref var vision = ref shortRange.Get<VisionModel>();
            ref var hideModel = ref entity.Get<HideModel>();
            var rand = Random.Range(0, 10);
            if (rand<hideModel.HideData.ProbabilityOfChoosingCover)
            {
                return false;
            }
            //Find the nearest cover
            var model = transformModel.transform;
            var sortedCovers = vision.Covers.OrderBy(e => Vector3.Distance(e.position,model.position)).ToList();
            foreach (var i in sortedCovers)
            {
                if (Physics.Linecast(i.position,target.Target.position,out RaycastHit hit))
                {
                    if (hit.collider.CompareTag("Terrain"))
                    {
                        var dist = Vector3.Distance(i.position, transformModel.transform.position)*_distanceRatio;
                        if (
                            Vector3.Distance(target.Target.position, transformModel.transform.position) <
                            dist || dist<_maxDistanceObstacle)
                        {
                            if (!CoverInitSystem.IsPointUnOccupied(i)) continue;
                            CoverInitSystem.BookCoverPoint(i);
                            if (hideModel.Obstacle != null)
                            {
                                CoverInitSystem.GiveUpPoint(hideModel.Obstacle);
                            }
                            
                            hideModel.CoverPointType= CoverInitSystem.GetTypeOfPointCover(i);
                            hideModel.Obstacle = i;
                            entity.Get<FindCoverStateTag>();
                            entity.Get<HideBlockTag>();
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
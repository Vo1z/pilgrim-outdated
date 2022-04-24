using DG.Tweening;
using Ingame.Cover;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.System
{
    public sealed class EnemyLeanSystem :IEcsRunSystem
    {
        private EcsFilter<EnemyLeanTag> _filter;
        private const float MAX_DISTANCE_TRESHHOLD = 1.2f;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var hideModel = ref entity.Get<HideModel>();
                ref var transformModel = ref entity.Get<TransformModel>();
                var deltaAngle = Vector3.Angle(transformModel.transform.position-hideModel.Obstacle.forward,
                    transformModel.transform.forward);
                var coverType = hideModel.CoverPointType;
                if (!Physics.Raycast(transformModel.transform.position, transformModel.transform.forward,out RaycastHit hit,MAX_DISTANCE_TRESHHOLD))
                {
                    continue;
                }

                switch (coverType)
                {
                    case CoverPointType.LeftLean:
                        PeekLeft(ref entity);
                        break;
                    case CoverPointType.RightLean:
                        PeekRight(ref entity);
                        break;
                }
            }
        }
        private void PeekLeft(ref EcsEntity entity)
        {
            PeekOnSide(ref entity, false);
        }
        private void PeekRight(ref EcsEntity entity)
        {
            PeekOnSide(ref entity, true);
        }

        private void PeekOnSide(ref EcsEntity entity, bool isRightLean)
        {

            entity.Get<EnemyLeanTag>();
            ref var hideModel = ref entity.Get<HideModel>();
            ref var transformModel = ref entity.Get<TransformModel>();

            var leanAngle = hideModel.HideData.LeanAngle;
             transformModel.transform.DORotate(new Vector3(0, 0, leanAngle * (isRightLean ? -1 : 1)), 0f);
           
        }
    }
}
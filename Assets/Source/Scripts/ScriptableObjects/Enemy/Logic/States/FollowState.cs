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
            ref var position = ref entity.Get<TransformModel>();
            ref var target = ref entity.Get<LocateTargetComponent>();
            ref var hideModel = ref entity.Get<HideModel>();
            if (Physics.Linecast(position.transform.position,target.Target.position, out RaycastHit hit))
            {
                var cover= hit.collider.gameObject.transform.position;
                if ((Vector3.Distance(cover, target.Target.position) < hideModel.HideData.MaxDistanceBetweenThisAndCover))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
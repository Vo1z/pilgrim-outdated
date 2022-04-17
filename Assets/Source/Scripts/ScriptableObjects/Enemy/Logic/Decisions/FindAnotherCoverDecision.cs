
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/FindAnotherCover", fileName = "FindAnotherCover")]
    public class FindAnotherCoverDecision : DecisionBase

    {
        public override bool Decide(ref EcsEntity entity)
        {
            if (entity.Has<DynamicHideCooldownComponent>())
            {
                return false;
            }
            ref var target = ref entity.Get<LocateTargetComponent>();
            ref var transformModel = ref entity.Get<TransformModel>();

            if (Physics.Linecast(target.Target.position, transformModel.transform.position, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    entity.Get<DynamicHideCooldownComponent>();
                    return true;
                }
            }
            return false;
        }
    }
}
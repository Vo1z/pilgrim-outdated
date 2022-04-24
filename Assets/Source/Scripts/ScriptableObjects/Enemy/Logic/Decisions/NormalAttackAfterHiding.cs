using Ingame.Enemy.State;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/AttackAfterHiding", fileName = "AttackAfterHiding")]
    public sealed class NormalAttackAfterHiding : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            ref var target = ref entity.Get<LocateTargetComponent>();
            ref var transformModel = ref entity.Get<TransformModel>();
            
            if (Physics.Linecast(transformModel.transform.position, target.Target.position, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    entity.Get<AttackStateTag>();
                    return true;
                }    
            }
            return false;
        }
    }
}
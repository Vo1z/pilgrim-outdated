using Ingame.Enemy.Extensions;
using Ingame.Enemy.State;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/Attack", fileName = "AttackDecision")]
    public class AttackDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            ref var vision = ref entity.Get<VisionModel>();
            if (entity.CanDetectTarget(0,vision.Vision.Distance))
            {
                entity.Get<AttackStateTag>();
                return true;
            }
            return false;
        }
    }
}
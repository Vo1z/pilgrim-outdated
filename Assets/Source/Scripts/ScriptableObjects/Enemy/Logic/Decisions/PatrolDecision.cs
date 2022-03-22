using System;
using Leopotam.Ecs;
using UnityEngine;
using Ingame.Enemy.Extensions;
using Ingame.Enemy.State;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/Patrol", fileName = "PatrolDecision")]
    public class PatrolDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            ref var vision = ref entity.Get<VisionModel>();
            if (!entity.CanDetectTarget(0,vision.Vision.MaxDistance))
            {
                entity.Get<PatrolStateTag>();
                return true;
            }

            return false;
        }
    }
}
﻿using Ingame.Enemy.State;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/EnterNormalAttackAfterFocusedAttack", fileName = "EnterNormalAttackAfterFocused")]
    public sealed class EnterNormalAttackOnFocusedAttackDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
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
            ref var target = ref entity.Get<LocateTargetComponent>();
            ref var shortRef = ref shortEntityReference.Entity;
            ref var vision = ref shortRef.Get<VisionModel>();

            if (vision.Opponents.Contains(target.Target)) return false;
            entity.Get<AttackStateTag>();
            return false;
        }
    }
}
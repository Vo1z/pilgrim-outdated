using System.Collections;
using System.Collections.Generic;
using Ingame.Enemy.ECS;
using Ingame.Enemy.Logic;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/State/Follow", fileName = "EnemyFollow")]
    public class FollowState : StateBase
    {
        public override void OnEnter(ref EcsEntity entity)
        {
            entity.Get<FollowStateTag>();

        }
        public override void OnExit(ref EcsEntity entity)
        {
            entity.Del<FollowStateTag>();
        }
    }
}
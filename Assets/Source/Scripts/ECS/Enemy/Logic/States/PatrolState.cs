using System;
using System.Collections;
using System.Collections.Generic;
using Ingame.Enemy.ECS;
using Ingame.Enemy.Logic;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/State/Patrol", fileName = "EnemyPatrol")]
    public class PatrolState : StateBase
    {
        public override void OnEnter(ref EcsEntity entity)
        {
            entity.Get<PatrolStateTag>();
      
        }
        
        public override void OnExit(ref EcsEntity entity)
        {
            entity.Del<PatrolStateTag>();
        }
    }
}
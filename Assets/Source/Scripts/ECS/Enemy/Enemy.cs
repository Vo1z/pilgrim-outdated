using System;
using System.Collections;
using System.Collections.Generic;
using Ingame.Enemy.ECS;
using Leopotam.Ecs;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private TriggerArea detectionArea;
    private EntityReference _entityReference;
    private void Update()
    {
        var result = TryGetComponent(out _entityReference);
        if (!result)
        {
            return;
        }

        if (_entityReference.Entity == EcsEntity.Null)
        {
            return;
        }
        ref var entity = ref  _entityReference.Entity;
        MainLogic(ref entity);
    }



    private void MainLogic(ref EcsEntity entity)
    {
        PatrolLogic(ref entity);
       // FollowLogic(ref entity);
       // AttackLogic(ref entity);
    }
    
    private void PatrolLogic(ref EcsEntity entity)
    {
        if (!entity.Has<PatrolStateTag>())
        {
            return;
        }

        if (detectionArea.GetValue())
        {
            //entity.Get<FollowStateTag>();
            entity.Get<AttackStateTag>();
            entity.Del<PatrolStateTag>();
        }
        
    }
    
    private void FollowLogic(ref EcsEntity entity)
    {
        if (!entity.Has<FollowStateTag>())
            return;
        
    }
    private void AttackLogic(ref EcsEntity entity)
    {
        if (!entity.Has<AttackStateTag>())
            return;
        
    }
}

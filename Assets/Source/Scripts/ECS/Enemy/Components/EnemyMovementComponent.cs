using System;
using System.Collections;
using System.Collections.Generic;
using Ingame.Enemy.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Ingame.Enemy.ECS
{
    [Serializable]
    public struct EnemyMovementComponent 
    {
        public EnemyMovementData EnemyMovementData;
        public Transform Waypoint;
        public NavMeshAgent NavMeshAgent;
    }
}

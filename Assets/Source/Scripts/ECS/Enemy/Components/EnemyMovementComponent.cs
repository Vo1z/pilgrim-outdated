using System;
using Ingame.Enemy.Data;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

namespace Ingame.Enemy
{
    [Serializable]
    public struct EnemyMovementComponent 
    {
        [Expandable]
        public EnemyMovementData EnemyMovementData;
        [HideInInspector]
        public Vector3 Waypoint;
        [HideInInspector]
        public NavMeshAgent NavMeshAgent;
    }
}

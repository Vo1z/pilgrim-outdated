using System;
using UnityEngine;

namespace Ingame.Enemy
{
    [Serializable]
    public struct HideBehindObstacleComponent
    {
        public Transform Obstacle;
        public LayerMask Mask;
    }
}
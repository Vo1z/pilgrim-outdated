using System;
using UnityEngine;

namespace Ingame.Enemy.ECS
{
    [Serializable]
    public struct ShootingModel
    {
        [Min(0)]
        public float Timer;
        [Min(0)]
        public float Accuracy;

    }
}
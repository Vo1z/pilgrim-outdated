using System;
using UnityEngine;
using NaughtyAttributes;
namespace Ingame.Enemy 
{
    [Serializable]
    public struct ShootingModel
    {
        [Expandable]
        public EnemyShootingData ShootingData;
    }
}
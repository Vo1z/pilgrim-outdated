using System;
using Ingame.Cover;
using Ingame.Enemy.Data;
using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Enemy
{
    [Serializable]
    public struct HideModel
    {
        public Transform Obstacle;
        public CoverPointType CoverPointType; 
        public LayerMask Mask;
        [Expandable] public EnemyHideData HideData;
    }
}
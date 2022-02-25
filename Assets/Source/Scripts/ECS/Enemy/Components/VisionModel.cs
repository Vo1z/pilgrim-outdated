using System;
using System.Collections.Generic;
using Ingame.Enemy.Data;
using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Enemy
{
    [Serializable]
    public struct VisionModel
    {
        [Expandable]
        public EnemyVisionOfDetectionData Vision;
        public List<Transform> Covers;
        public List<Transform> Opponents;
    }
}
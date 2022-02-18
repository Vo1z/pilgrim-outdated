using System;
using System.Collections.Generic;
using Ingame.Enemy.Data;
using UnityEngine;

namespace Ingame.Enemy
{
    [Serializable]
    public struct VisionModel
    {
        public EnemyVisionOfDetectionData Vision;
        public List<Transform> Covers;
        public List<Transform> Opponents;
    }
}
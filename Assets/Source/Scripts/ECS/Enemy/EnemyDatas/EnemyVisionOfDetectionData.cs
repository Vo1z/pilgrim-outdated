using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Enemy.Data
{ 
    public class EnemyVisionOfDetectionData : ScriptableObject
    {
        [BoxGroup("Vision")] [Min(0)]
        public float VisionRange = 3;
    }
}

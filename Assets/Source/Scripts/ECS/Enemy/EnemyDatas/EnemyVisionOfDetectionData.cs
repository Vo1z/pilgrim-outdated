using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Enemy.Data
{  
    [CreateAssetMenu(menuName = "Ingame/Enemy/Data/Vision", fileName = "EnemyVisionData")]
    public class EnemyVisionOfDetectionData : ScriptableObject
    {
        [BoxGroup("Vision")] [Min(0)]
        public float Angle;
        [BoxGroup("Vision")] [Min(0)]
        public float Distance;
        [BoxGroup("Vision")] [Min(0)]
        public float MaxDistance;
        [BoxGroup("Vision")] [Min(0)]
        public float Height;
        public LayerMask Mask;
    }
}

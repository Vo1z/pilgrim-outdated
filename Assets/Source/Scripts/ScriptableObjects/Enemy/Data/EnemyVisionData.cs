using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Enemy.Data
{  
    [CreateAssetMenu(menuName = "Ingame/Enemy/Data/Vision", fileName = "EnemyVisionData")]
    public class EnemyVisionData : ScriptableObject
    {
        [SerializeField]
        [BoxGroup("Vision")] [Min(0)]
        private float angle;
        
        [SerializeField]
        [BoxGroup("Vision")] [Min(0)]
        private float distance;
        
        [SerializeField]
        [BoxGroup("Vision")] [Min(0)]
        private float maxDistance;

        [SerializeField]
        [BoxGroup("Vision")] [Min(0)]
        private float height;
        
        [SerializeField]
        private LayerMask mask;
        
        public float Angle => angle;
        public float Distance => distance;
        public float MaxDistance => maxDistance;
        public float Height => height;
        public LayerMask Mask => mask;
    }
}

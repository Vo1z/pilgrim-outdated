using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Enemy.Data
{  
    [CreateAssetMenu(menuName = "Ingame/Enemy/Data/Vision", fileName = "EnemyVisionData")]
    public sealed class EnemyVisionData : ScriptableObject
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

        
        
        [SerializeField]
        private float headPositionAccordingToTransformPosition;

            public float Angle => angle;
        public float Distance => distance;
        public float MaxDistance => maxDistance;
        public float Height => height;
        public LayerMask Mask => mask;
        /*<summary>Position of head of a enemy</summary>*/
        public float HeadPosition => headPositionAccordingToTransformPosition;
    }
}
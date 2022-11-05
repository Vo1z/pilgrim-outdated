using NaughtyAttributes;
using UnityEngine;
namespace Ingame.Enemy.Data{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Data/Movement", fileName = "EnemyMovementData")]
    public sealed class EnemyMovementData : ScriptableObject
    {
        [SerializeField][BoxGroup("Movement")][Min(0)]
        private float delayAfterAchievingWaypoint = 2.5f;
        [SerializeField][BoxGroup("Movement")][Min(0)]
        private float speedForward = 1.5f;

        [SerializeField][BoxGroup("Movement")][Min(0)]
        private  float speedOnReposition = 0.9f;

        [SerializeField][BoxGroup("Movement")][Min(0)]
        private  float speedBack = 0.7f;

        [SerializeField] [BoxGroup("Movement")] [Min(0)]
        private float maxDistanceFromDestinationPoint = 5;
        
        [SerializeField] [BoxGroup("Movement")] [Min(0)]
        private float maxFleeDistance= 200;
        
        
        [SerializeField] [BoxGroup("Movement")] [Min(0)]
        private float minRangeOfOneStep = .5f;
        
        [SerializeField] [BoxGroup("Movement")] [Min(0)]
        private float maxRangeOfOneStep = 3.5f;
        
        
        public float SpeedForward => speedForward;
        public float SpeedOnReposition => speedOnReposition;
        public float SpeedBack => speedBack;
        public float MaxFleeDistance => maxFleeDistance;
        public float MaxDistanceFromDestinationPoint => maxDistanceFromDestinationPoint;
        public float DelayAfterAchievingWaypoint  =>delayAfterAchievingWaypoint;
        public float MaxRangeOfOneStep => maxRangeOfOneStep;
        public float MinRangeOfOneStep => minRangeOfOneStep;
    }
}
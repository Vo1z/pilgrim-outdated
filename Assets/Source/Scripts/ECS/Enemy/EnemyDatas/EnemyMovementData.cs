using NaughtyAttributes;
using UnityEngine;
namespace Ingame.Enemy.Data{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Data/Movement", fileName = "EnemyMovementData")]
    public class EnemyMovementData : ScriptableObject
    {
        [BoxGroup("Movement")][Min(0)]
        public float SpeedForward = 1.5f;

        [BoxGroup("Movement")][Min(0)]
        public float SpeedSide = 0.9f;

        [BoxGroup("Movement")][Min(0)]
        public float SpeedBack = 0.7f;
    }
}

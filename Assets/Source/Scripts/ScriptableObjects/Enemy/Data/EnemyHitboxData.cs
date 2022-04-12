using UnityEngine;

namespace Ingame.Enemy.Data
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Data/Hitbox", fileName = "EnemyHitboxData")]
    public sealed class EnemyHitboxData : ScriptableObject
    {
        [SerializeField] [Min(0)] private float _normalHeight;
        [SerializeField] [Min(0)] private float _crouchHeight;
        public float NormalHeight => _normalHeight;
        public float CrouchHeight => _crouchHeight;
    }
}
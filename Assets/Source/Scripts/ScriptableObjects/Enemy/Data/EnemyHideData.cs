using UnityEngine;

namespace Ingame.Enemy.Data
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Data/Hide", fileName = "EnemyHideData")]
    public class EnemyHideData : ScriptableObject
    {
        [SerializeField] [Min(0)] private float safeDistance;
        [SerializeField] [Min(0)] private float coolDown;
        [SerializeField] [Min(0)] private float maxDistanceBetweenThisAndCover;
        public float SafeDistance => safeDistance;
        /// <summary>
        /// returns a time factor between performing next action after previous one.
        /// </summary>
        public float CoolDown => coolDown;
        public float MaxDistanceBetweenThisAndCover => maxDistanceBetweenThisAndCover;

    }
}
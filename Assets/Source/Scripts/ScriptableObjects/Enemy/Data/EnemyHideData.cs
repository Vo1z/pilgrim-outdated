using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Enemy.Data
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Data/Hide", fileName = "EnemyHideData")]
    public sealed class EnemyHideData : ScriptableObject
    {
        [SerializeField] [Min(0)] private float coolDownOnPeek;
        [SerializeField] [Min(0)] private float coolDownOnTakeCover;
        [SerializeField] [Min(0)] private float leanAngle;
        [SerializeField] [Min(0)] [MaxValue(10)]private float probabilityOfChoosingCover;
        /// <summary>
        /// returns a time factor between performing next action after previous one.
        /// </summary>
        public float CoolDownOnPeek => coolDownOnPeek;
        public float  CoolDownOnTakeCover =>  coolDownOnTakeCover;
        public float LeanAngle=> leanAngle;
        public float ProbabilityOfChoosingCover => probabilityOfChoosingCover;

    }
}
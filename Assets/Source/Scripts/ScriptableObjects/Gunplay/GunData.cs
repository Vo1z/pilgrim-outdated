using UnityEngine;

namespace Ingame.Guns
{
    [CreateAssetMenu(menuName = "Ingame/Gun data", fileName = "NewGunData")]
    public sealed class GunData : ScriptableObject
    {
        [SerializeField] [Min(0)] private float pauseBetweenShots = .2f;

        public float PauseBetweenShots => pauseBetweenShots;
    }

    public enum FireMode
    {
        Safe,
        Single,
        Automatic,
        Burst
    }
}
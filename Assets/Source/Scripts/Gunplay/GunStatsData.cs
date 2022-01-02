using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Guns
{
    [CreateAssetMenu(menuName = "Ingame/Gun data", fileName = "NewGunData")]
    public sealed class GunStatsData : ScriptableObject
    {
        [BoxGroup("Hud stats")]
        [SerializeField] [Range(0, 10)] private float rotationSpeed = 5;
        [BoxGroup("Gun stats")]
        [SerializeField] private FireMode[] fireModes;
        
        public const float MAX_ROTATION_ANGLE = 20f;

        public float RotationSpeed => rotationSpeed;
        
        public FireMode[] FireModes => fireModes;
    }

    public enum FireMode
    {
        Safe,
        Single,
        Automatic,
        Burst
    }
}
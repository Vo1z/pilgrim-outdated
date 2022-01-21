using UnityEngine;

namespace Ingame.Guns
{
    [CreateAssetMenu(menuName = "Ingame/Gun data", fileName = "NewGunData")]
    public sealed class GunData : ScriptableObject
    {
        [SerializeField] [Min(0)] private float pauseBetweenShots = .2f;
        
        [SerializeField] [Range(0, 20)] private float instability = 1f;
        [SerializeField] [Range(0, 50)] private float verticalRecoilForce = 1f;
        [SerializeField] [Range(0, 50)] private float frontRecoilForce = 1f;
        [SerializeField] [Range(0, 5)] private float minMaxRecoilPositionOffset = 1f;
        
        public float Instability => instability;
        public float PauseBetweenShots => pauseBetweenShots;
        public float VerticalRecoilForce => verticalRecoilForce;
        public float FrontRecoilForce => frontRecoilForce;
        public float MinMaxRecoilPositionOffset => minMaxRecoilPositionOffset;
    }

    public enum FireMode
    {
        Safe,
        Single,
        Automatic,
        Burst
    }
}
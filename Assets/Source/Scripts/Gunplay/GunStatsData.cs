using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Guns
{
    [CreateAssetMenu(menuName = "Ingame/Gun data", fileName = "NewGunData")]
    public sealed class GunStatsData : ScriptableObject
    {
        [BoxGroup("Hud stats")]
        [SerializeField] [Range(0, 10)] private float rotationSpeed = 5;
        
        [BoxGroup("Hud stats"), Space]
        [SerializeField] [Range(0, 50)] private float maxRotationAngleX = 20;
        [BoxGroup("Hud stats")]                                           
        [SerializeField] [Range(0, 20)] private float rotationAngleMultiplierX = 20;
        [BoxGroup("Hud stats")] 
        [SerializeField] private bool inverseRotationX = false;
        
        [BoxGroup("Hud stats"), Space]
        [SerializeField] [Range(0, 50)] private float maxRotationAngleY = 20;
        [BoxGroup("Hud stats")]                                                      
        [SerializeField] [Range(0, 40)] private float rotationAngleMultiplierY = 20;
        [BoxGroup("Hud stats")] 
        [SerializeField] private bool inverseRotationY = false;

        [BoxGroup("Hud stats"), Space]
        [SerializeField] [Range(0, 50)] private float maxRotationAngleZ = 20;
        [BoxGroup("Hud stats")]                                                      
        [SerializeField] [Range(0, 40)] private float rotationAngleMultiplierZ = 20; 
        [BoxGroup("Hud stats")] 
        [SerializeField] private bool inverseRotationZ = false;
        
        [BoxGroup("Gun stats")]
        [SerializeField] private FireMode[] fireModes;
        
        public float RotationSpeed => rotationSpeed;
        public float MaxRotationAngleX => maxRotationAngleX;
        public float RotationAngleMultiplierX => rotationAngleMultiplierX;
        public float InverseRotationX => inverseRotationX ? -1: 1;
        
        public float MaxRotationAngleY => maxRotationAngleY;
        public float RotationAngleMultiplierY => rotationAngleMultiplierY;
        public float InverseRotationY => inverseRotationY ? -1: 1;
        
        public float MaxRotationAngleZ => maxRotationAngleZ;
        public float RotationAngleMultiplierZ => rotationAngleMultiplierZ;
        public float InverseRotationZ => inverseRotationZ ? -1: 1;
        
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
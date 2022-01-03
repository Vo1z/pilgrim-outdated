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
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxRotationAngleX;
        [BoxGroup("Hud stats")]                                           
        [SerializeField] [Range(0, 20)] private float rotationAngleMultiplierX = 20;
        [BoxGroup("Hud stats")] 
        [SerializeField] private bool inverseRotationX = false;
        
        [BoxGroup("Hud stats"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxRotationAngleY;
        [BoxGroup("Hud stats")]                                                      
        [SerializeField] [Range(0, 40)] private float rotationAngleMultiplierY = 20;
        [BoxGroup("Hud stats")] 
        [SerializeField] private bool inverseRotationY = false;

        [BoxGroup("Hud stats"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxRotationAngleZ;
        [BoxGroup("Hud stats")]                                                      
        [SerializeField] [Range(0, 40)] private float rotationAngleMultiplierZ = 20; 
        [BoxGroup("Hud stats")] 
        [SerializeField] private bool inverseRotationZ = false;
        
        [BoxGroup("Gun stats")]
        [SerializeField] private FireMode[] fireModes;
        
        public float RotationSpeed => rotationSpeed;
        
        public Vector2 MinMaxRotationAngleX => minMaxRotationAngleX;
        public float RotationAngleMultiplierX => rotationAngleMultiplierX;
        public float InverseRotationX => inverseRotationX ? -1: 1;
        
        public Vector2 MinMaxRotationAngleY => minMaxRotationAngleY;
        public float RotationAngleMultiplierY => rotationAngleMultiplierY;
        public float InverseRotationY => inverseRotationY ? -1: 1;
        
        public Vector2 MinMaxRotationAngleZ => minMaxRotationAngleZ;
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
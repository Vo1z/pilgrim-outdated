using NaughtyAttributes;
using UnityEngine;

namespace Ingame
{
    [CreateAssetMenu(menuName = "Ingame/HudItemData", fileName = "newHudItemData")]
    public class HudItemData : ScriptableObject
    {
        [BoxGroup("Hud stats (Rotation)"),]
        [SerializeField] [Range(0, 10)] private float rotationSpeed = 5;
        
        [BoxGroup("Hud stats (Rotation)"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxRotationAngleX;
        [BoxGroup("Hud stats (Rotation)")]                                           
        [SerializeField] [Range(0, 20)] private float rotationAngleMultiplierX = 20;
        [BoxGroup("Hud stats (Rotation)")] 
        [SerializeField] private bool inverseRotationX = false;
        
        [BoxGroup("Hud stats (Rotation)"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxRotationAngleY;
        [BoxGroup("Hud stats (Rotation)")]                                                      
        [SerializeField] [Range(0, 40)] private float rotationAngleMultiplierY = 20;
        [BoxGroup("Hud stats (Rotation)")] 
        [SerializeField] private bool inverseRotationY = false;

        [BoxGroup("Hud stats (Rotation)"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxRotationAngleZ;
        [BoxGroup("Hud stats (Rotation)")]                                                      
        [SerializeField] [Range(0, 40)] private float rotationAngleMultiplierZ = 20; 
        [BoxGroup("Hud stats (Rotation)")] 
        [SerializeField] private bool inverseRotationZ = false;
        
        
        
        [BoxGroup("Hud stats (Aim Rotation)"),]
        [SerializeField] [Range(0, 10)] private float aimRotationSpeed = 5;
        
        [BoxGroup("Hud stats (Aim Rotation)"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxAimRotationAngleX;
        [BoxGroup("Hud stats (Aim Rotation)")]                                           
        [SerializeField] [Range(0, 20)] private float aimRotationAngleMultiplierX = 20;
        [BoxGroup("Hud stats (Aim Rotation)")] 
        [SerializeField] private bool inverseAimRotationX = false;
        
        [BoxGroup("Hud stats (Aim Rotation)"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxAimRotationAngleY;
        [BoxGroup("Hud stats (Aim Rotation)")]                                                      
        [SerializeField] [Range(0, 40)] private float aimRotationAngleMultiplierY = 20;
        [BoxGroup("Hud stats (Aim Rotation)")] 
        [SerializeField] private bool inverseAimRotationY = false;

        [BoxGroup("Hud stats (Aim Rotation)"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxAimRotationAngleZ;
        [BoxGroup("Hud stats (Aim Rotation)")]                                                      
        [SerializeField] [Range(0, 40)] private float aimRotationAngleMultiplierZ = 20; 
        [BoxGroup("Hud stats (Aim Rotation)")] 
        [SerializeField] private bool inverseAimRotationZ = false;

        
        
        [BoxGroup("Hud stats (Movement)"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxRotationMovementAngleX;
        [BoxGroup("Hud stats (Movement)")]
        [SerializeField] [Range(0, 40)] private float rotationMovementAngleMultiplierX = 20;
        [BoxGroup("Hud stats (Movement)")]
        [SerializeField] private bool inverseRotationMovementX = false;
        
        [BoxGroup("Hud stats (Movement)"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxRotationMovementAngleZ;
        [BoxGroup("Hud stats (Movement)")]
        [SerializeField] [Range(0, 40)] private float rotationMovementAngleMultiplierZ = 20;
        [BoxGroup("Hud stats (Movement)")]
        [SerializeField] private bool inverseRotationMovementZ = false;

        
        
        [BoxGroup("Hud stats (Aim Movement)"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxAimRotationMovementAngleX;
        [BoxGroup("Hud stats (Aim Movement)")]
        [SerializeField] [Range(0, 40)] private float aimRotationMovementAngleMultiplierX = 20;
        [BoxGroup("Hud stats (Aim Movement)")]
        [SerializeField] private bool inverseAimRotationMovementX = false;
        
        [BoxGroup("Hud stats (Aim Movement)"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxAimRotationMovementAngleZ;
        [BoxGroup("Hud stats (Aim Movement)")]
        [SerializeField] [Range(0, 40)] private float aimRotationMovementAngleMultiplierZ = 20;
        [BoxGroup("Hud stats (Aim Movement)")]
        [SerializeField] private bool inverseAimRotationMovementZ = false;
        
        
        
        [BoxGroup("Hud stats (Clipping)"), Space] 
        [SerializeField] [Range(0, 2f)] private float maximumClippingOffset = 1.5f;
        [BoxGroup("Hud stats (Clipping)")]
        [SerializeField] [Range(0, 10f)] private float clippingMovementSpeed = 3f;
        
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

        
        
        public float AimRotationSpeed => aimRotationSpeed;
        
        public Vector2 MinMaxAimRotationAngleX => minMaxAimRotationAngleX;
        public float AimRotationAngleMultiplierX => aimRotationAngleMultiplierX;
        public float InverseAimRotationX => inverseAimRotationX ? -1: 1;
        
        public Vector2 MinMaxAimRotationAngleY => minMaxAimRotationAngleY;
        public float AimRotationAngleMultiplierY => aimRotationAngleMultiplierY;
        public float InverseAimRotationY => inverseAimRotationY ? -1: 1;
        
        public Vector2 MinMaxAimRotationAngleZ => minMaxAimRotationAngleZ;
        public float AimRotationAngleMultiplierZ => aimRotationAngleMultiplierZ;
        public float InverseAimRotationZ => inverseAimRotationZ ? -1: 1;
        
        
        
        public Vector2 MinMaxRotationMovementAngleX => minMaxRotationMovementAngleX;
        public float RotationMovementAngleMultiplierX => rotationMovementAngleMultiplierX;
        public float InverseRotationMovementX => inverseRotationMovementX ? -1: 1;
        
        public Vector2 MinMaxRotationMovementAngleZ => minMaxRotationMovementAngleZ;
        public float RotationMovementAngleMultiplierZ => rotationMovementAngleMultiplierZ;
        public float InverseRotationMovementZ => inverseRotationMovementZ ? -1: 1;
        
        
        public Vector2 MinMaxAimRotationMovementAngleX => minMaxAimRotationMovementAngleX;
        public float AimRotationMovementAngleMultiplierX => aimRotationMovementAngleMultiplierX;
        public float InverseAimRotationMovementX => inverseAimRotationMovementX ? -1: 1;
        
        public Vector2 MinMaxAimRotationMovementAngleZ => minMaxAimRotationMovementAngleZ;
        public float AimRotationMovementAngleMultiplierZ => aimRotationMovementAngleMultiplierZ;
        public float InverseAimRotationMovementZ => inverseAimRotationMovementZ ? -1: 1;

        
        public float MaximumClippingOffset => maximumClippingOffset;
        public float ClippingMovementSpeed => clippingMovementSpeed;
    }
}
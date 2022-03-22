using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Data.Hud
{
    [CreateAssetMenu(menuName = "Ingame/HudItemData", fileName = "newHudItemData")]
    public class HudItemData : ScriptableObject
    {
        [BoxGroup("Hud stats (Common)")]
        [SerializeField] private bool canBeUsedAsAim = true;

        [BoxGroup("Hud stats (Rotation)")]
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

        [BoxGroup("Hud stats (Rotation)"), Space]
        [SerializeField] private bool isItemMovedDueToRotation = false;
        [BoxGroup("Hud stats (Rotation)"), ShowIf("isItemMovedDueToRotation")] 
        [SerializeField] [MinMaxSlider(-2, 2)] private Vector2 minMaxMovementOffsetY = new(0, 0);
        [BoxGroup("Hud stats (Rotation)"), ShowIf("isItemMovedDueToRotation")]
        [SerializeField] [Range(0, 10)] private float moveSpeed = 5f;


        [BoxGroup("Hud stats (Aim Rotation)"), ShowIf("canBeUsedAsAim")]
        [SerializeField] [Range(0, 10)] private float aimRotationSpeed = 5;
        
        [BoxGroup("Hud stats (Aim Rotation)"), ShowIf("canBeUsedAsAim"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxAimRotationAngleX;
        [BoxGroup("Hud stats (Aim Rotation)"), ShowIf("canBeUsedAsAim")]                                           
        [SerializeField] [Range(0, 20)] private float aimRotationAngleMultiplierX = 20;
        [BoxGroup("Hud stats (Aim Rotation)"), ShowIf("canBeUsedAsAim")] 
        [SerializeField] private bool inverseAimRotationX = false;
        
        [BoxGroup("Hud stats (Aim Rotation)"), ShowIf("canBeUsedAsAim"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxAimRotationAngleY;
        [BoxGroup("Hud stats (Aim Rotation)"), ShowIf("canBeUsedAsAim")]                                                      
        [SerializeField] [Range(0, 40)] private float aimRotationAngleMultiplierY = 20;
        [BoxGroup("Hud stats (Aim Rotation)"), ShowIf("canBeUsedAsAim")] 
        [SerializeField] private bool inverseAimRotationY = false;

        [BoxGroup("Hud stats (Aim Rotation)"), ShowIf("canBeUsedAsAim"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxAimRotationAngleZ;
        [BoxGroup("Hud stats (Aim Rotation)"), ShowIf("canBeUsedAsAim")]                                                      
        [SerializeField] [Range(0, 40)] private float aimRotationAngleMultiplierZ = 20; 
        [BoxGroup("Hud stats (Aim Rotation)"), ShowIf("canBeUsedAsAim")] 
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

        
        
        [BoxGroup("Hud stats (Aim Movement)"), ShowIf("canBeUsedAsAim"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxAimRotationMovementAngleX;
        [BoxGroup("Hud stats (Aim Movement)"), ShowIf("canBeUsedAsAim")]
        [SerializeField] [Range(0, 40)] private float aimRotationMovementAngleMultiplierX = 20;
        [BoxGroup("Hud stats (Aim Movement)"), ShowIf("canBeUsedAsAim")]
        [SerializeField] private bool inverseAimRotationMovementX = false;
        
        [BoxGroup("Hud stats (Aim Movement)"), ShowIf("canBeUsedAsAim"), Space]
        [SerializeField] [MinMaxSlider(-50f, 50f)] private Vector2 minMaxAimRotationMovementAngleY;
        [BoxGroup("Hud stats (Aim Movement)"), ShowIf("canBeUsedAsAim")]
        [SerializeField] [Range(0, 40)] private float aimRotationMovementAngleMultiplierY = 20;
        [BoxGroup("Hud stats (Aim Movement)"), ShowIf("canBeUsedAsAim")]
        [SerializeField] private bool inverseAimRotationMovementY = false;
        
        
        
        [BoxGroup("Hud stats (Clipping)"), Space] 
        [SerializeField] [Range(0, 2f)] private float maximumClippingOffset = 1.5f;
        [BoxGroup("Hud stats (Clipping)")] 
        [SerializeField] [Range(0, 2f)] private float maximumAimClippingOffset = .3f;
        [BoxGroup("Hud stats (Clipping)")]
        [SerializeField] [Range(0, 10f)] private float clippingMovementSpeed = 3f;

        public bool CanBeUsedAsAim => canBeUsedAsAim;
        
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

        public bool IsItemMovedDueToRotation => isItemMovedDueToRotation;
        public Vector2 MinMaxMovementOffsetY => minMaxMovementOffsetY;
        public float MoveSpeed => moveSpeed;
        
        
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
        
        public Vector2 MINMaxAimRotationMovementAngleY => minMaxAimRotationMovementAngleY;
        public float AimRotationMovementAngleMultiplierY => aimRotationMovementAngleMultiplierY;
        public float InverseAimRotationMovementY => inverseAimRotationMovementY ? -1: 1;

        
        public float MaximumClippingOffset => maximumClippingOffset;
        public float MaximumAimClippingOffset => maximumAimClippingOffset;
        public float ClippingMovementSpeed => clippingMovementSpeed;
    }
}
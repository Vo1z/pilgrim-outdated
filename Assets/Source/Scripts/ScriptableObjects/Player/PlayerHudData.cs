using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Player
{
    [CreateAssetMenu(menuName = "Ingame/PlayerHudData", fileName = "Ingame/NewPlayerHudData")]
    public class PlayerHudData : ScriptableObject
    {
        [BoxGroup("Head Bobbing")]
        [SerializeField, Range(0, 10f)] private float headBobbingLerpingSpeed = 5f;
        [BoxGroup("Head Bobbing")]
        [SerializeField, Range(0, 1f)] private float headBobbingSpeedModifier = .4f;
        [BoxGroup("Head Bobbing")]
        [SerializeField, Range(0, 0.1f)] private float headBobbingStrengthX = .01f;
        [BoxGroup("Head Bobbing")]
        [SerializeField, Range(0, 0.1f)] private float headBobbingStrengthY = .01f; 
        [BoxGroup("Head Bobbing")]
        [SerializeField, Range(0, 0.1f)] private float headBobbingStrengthZ = .01f;

        public float HeadBobbingLerpingSpeed => headBobbingLerpingSpeed;
        public float HeadBobbingStrengthX => headBobbingStrengthX;
        public float HeadBobbingStrengthY => headBobbingStrengthY;
        public float HeadBobbingStrengthZ => headBobbingStrengthZ;
        public float HeadBobbingSpeedModifier => headBobbingSpeedModifier;
    }
}   
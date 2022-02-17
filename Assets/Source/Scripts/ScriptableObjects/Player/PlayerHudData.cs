using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Player
{
    [CreateAssetMenu(menuName = "Ingame/PlayerHudData", fileName = "Ingame/NewPlayerHudData")]
    public class PlayerHudData : ScriptableObject
    {
        [BoxGroup("Head Bobbing")]
        [SerializeField, Range(0, 0.1f)] private float headBobbingStrength = 0.01f; 
        [BoxGroup("Head Bobbing")]
        [SerializeField, Range(0, 1f)] private float headBobbingSpeedModifier = 0.01f; 
        
        public float HeadBobbingStrength => headBobbingStrength;
        public float HeadBobbingSpeedModifier => headBobbingSpeedModifier;
    }
}   
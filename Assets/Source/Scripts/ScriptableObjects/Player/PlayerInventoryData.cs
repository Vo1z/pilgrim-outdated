using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Data.Player
{
    [CreateAssetMenu(menuName = "Ingame/PlayerInventoryData", fileName = "Ingame/NewPlayerInventoryData")]
    public class PlayerInventoryData : ScriptableObject
    {
        [BoxGroup("Common")]
        [SerializeField] [Min(0)] private float maximumWeight = 20f;
        
        [BoxGroup("Medecine")]
        [SerializeField] [Min(0)] private int maximumNumberOfMorphine = 20;
        [BoxGroup("Medecine")]
        [SerializeField] [Min(0)] private int maximumNumberOfBandages = 20;

        public float MaximumWeight => maximumWeight;
        public int MaximumNumberOfMorphine => maximumNumberOfMorphine;
        public int MaximumNumberOfBandages => maximumNumberOfBandages;
    }
}
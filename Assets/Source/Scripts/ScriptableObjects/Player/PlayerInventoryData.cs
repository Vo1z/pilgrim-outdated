using System;
using System.Collections.Generic;
using Ingame.Data.Gunplay;
using NaughtyAttributes;
using Support.Extensions;
using UnityEngine;

namespace Ingame.Data.Player
{
    [CreateAssetMenu(menuName = "Ingame/PlayerInventoryData", fileName = "Ingame/NewPlayerInventoryData")]
    public class PlayerInventoryData : ScriptableObject
    {
        [BoxGroup("Medecine")]
        [SerializeField] [Min(0)] private int maximumNumberOfMorphine = 20;
        [BoxGroup("Medecine")]
        [SerializeField] [Min(0)] private int maximumNumberOfBandages = 20;
        [BoxGroup("Medecine")]
        [SerializeField] [Min(0)] private int maximumNumberOfInhalators = 20;
        [BoxGroup("Medecine")]
        [SerializeField] [Min(0)] private int maximumNumberOfEnergyDrinks = 20;
        
        [BoxGroup("Combat")]
        [SerializeField] [Min(0)] private int maximumNumberOfMagazines = 6;
        [BoxGroup("Combat")]
        [SerializeField] private List<AmmoTypeAmount> ammoTypeAmounts;

        public int MaximumNumberOfMorphine => maximumNumberOfMorphine;
        public int MaximumNumberOfBandages => maximumNumberOfBandages;
        public int MaximumNumberOfInhalators => maximumNumberOfInhalators;
        public int MaximumNumberOfEnergyDrinks => maximumNumberOfEnergyDrinks;

        public int MaximumNumberOfMagazines => maximumNumberOfMagazines;
        public List<AmmoTypeAmount> AmmoTypeAmounts => ammoTypeAmounts;

        public int GetMaximumAmountOfAmmo(AmmoType ammoType)
        {
            var ammoTypeAmount = AmmoTypeAmounts.SafeFirst(p => p.ammoType == ammoType);

            return ammoTypeAmount?.maximumAmount ?? 0;
        }
    }

    [Serializable]
    public class AmmoTypeAmount
    {
        public AmmoType ammoType;
        public int maximumAmount;
    }
}
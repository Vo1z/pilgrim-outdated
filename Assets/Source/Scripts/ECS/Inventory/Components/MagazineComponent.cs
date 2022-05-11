using System;
using Ingame.Data.Gunplay;
using NaughtyAttributes;

namespace Ingame.Inventory
{
    [Serializable]
    public struct MagazineComponent
    {
        public int currentAmountOfAmmoInMagazine;
        
        [AllowNesting]
        [Expandable]
        public MagazineData magazineData;
    }
}
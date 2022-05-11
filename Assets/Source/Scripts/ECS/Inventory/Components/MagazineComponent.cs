using System;
using Ingame.Data.Gunplay;

namespace Ingame.Inventory
{
    [Serializable]
    public struct MagazineComponent
    {
        public int currentAmountOfAmmoInMagazine;
        public MagazineData magazineData;
    }
}
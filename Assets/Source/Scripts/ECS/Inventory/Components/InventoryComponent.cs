using System.Collections.Generic;
using Ingame.Data.Gunplay;

namespace Ingame.Inventory
{
    public struct InventoryComponent
    {
        public int currentNumberOfEnergyDrinks;
        public int currentNumberOfCreamTubes;
        public int currentNumberOfInhalators;
        public int currentNumberOfMorphine;
        public int currentNumberOfAdrenaline;
        public int currentNumberOfBandages;
        
        public int currentNumberOfMagazines;

        public Dictionary<AmmoType, int> ammo;
    }
}
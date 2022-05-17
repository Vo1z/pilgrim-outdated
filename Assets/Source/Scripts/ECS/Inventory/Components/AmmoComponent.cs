using System;
using Ingame.Data.Gunplay;

namespace Ingame.Inventory
{
    [Serializable]
    public struct AmmoComponent
    {
        public int amountOfBullets;
        public AmmoType ammoType;
    }
}
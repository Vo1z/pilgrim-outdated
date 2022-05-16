using System;
using Ingame.Data.Gunplay;
using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Inventory
{
    [Serializable]
    public struct MagazineComponent
    {
        public int currentAmountOfAmmoInMagazine;
        /// <summary>Object that will be turned on when magazine is full of ammo</summary>
        public Transform fullLoadedMagIdentifier;

        [AllowNesting]
        [Expandable]
        public MagazineData magazineData;
    }
}
using System;
using UnityEngine;

namespace Ingame.Inventory.Items
{
    [Serializable]
    public struct BackpackModel
    {
        public Transform[] morphineInsideBackpack;
        public Transform[] bandagesInsideBackpack;
        public Transform[] inhalatorsInsideBackpack;
        public Transform[] energyDrinksInsideBackpack;
        public Transform[] creamTubesInsideBackpack;
    }
}
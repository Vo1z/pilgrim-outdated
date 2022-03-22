using System;
using Ingame.Data.Inventory;
using NaughtyAttributes;

namespace Ingame.Inventory.Items
{
    [Serializable]
    public struct InventoryItemModel
    {
        [AllowNesting]
        [Required, Expandable]
        public InventoryItemData InventoryItemData;
    }
}
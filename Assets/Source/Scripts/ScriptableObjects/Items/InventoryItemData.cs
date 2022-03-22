using UnityEngine;

namespace Ingame.Data.Inventory
{
    [CreateAssetMenu(menuName = "Ingame/InventoryItemData", fileName = "NewInventoryItemData")]
    public class InventoryItemData : ScriptableObject
    {
        [SerializeField] private bool stopsBleeding = false;
        [SerializeField] private bool stopsGasChoke = false;

        public bool StopsBleeding => stopsBleeding;
        public bool StopsGasChoke => stopsGasChoke;
    }
}
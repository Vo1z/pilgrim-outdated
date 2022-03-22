using Ingame.Health;
using Ingame.Interaction.Common;
using Ingame.Player;
using Leopotam.Ecs;

namespace Ingame.Inventory.Items
{
    public sealed class InventoryItemUsingSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<InventoryItemModel, PerformInteractionTag, InteractiveTag> _inventoryItemFilter;
        private readonly EcsFilter<PlayerModel, HealthComponent, InventoryComponent> _playerFilter;

        public void Run()
        {
            if(_playerFilter.IsEmpty())
                return;

            ref var playerEntity = ref _playerFilter.GetEntity(0);
            ref var playerInventory = ref _playerFilter.Get3(0);
            
            foreach (var i in _inventoryItemFilter)
            {
                ref var itemEntity = ref _inventoryItemFilter.GetEntity(i);
                ref var inventoryItemModel = ref _inventoryItemFilter.Get1(i);

                var itemData = inventoryItemModel.InventoryItemData;

                if (itemData.StopsBleeding)
                {
                    playerEntity.Get<StopBleedingTag>();
                    
                    if(itemEntity.Has<BandageTag>())
                        playerInventory.currentNumberOfBandages -= 1;
                }

                if (itemData.StopsGasChoke)
                {
                    playerEntity.Get<StopGasChokeTag>();
                    
                    if(itemEntity.Has<SprayTag>())
                        playerInventory.currentNumberOfSprays -= 1;
                }

                _world.NewEntity().Get<UpdateInventoryAppearanceEvent>();
                itemEntity.Del<PerformInteractionTag>();
            }
        }
    }
}
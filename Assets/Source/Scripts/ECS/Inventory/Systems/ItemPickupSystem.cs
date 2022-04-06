using Ingame.Data.Player;
using Ingame.Interaction.Common;
using Ingame.Inventory.Items;
using Ingame.Movement;
using Ingame.Player;
using Leopotam.Ecs;
using Support.Extensions;

namespace Ingame.Inventory
{
    public class ItemPickupSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<ItemTag, InteractiveTag, PerformInteractionTag, LootItemTag> _pickupItemsFilter;
        private readonly EcsFilter<PlayerModel, InventoryComponent> _playerFilter;

        public void Run()
        {
            if(_playerFilter.IsEmpty())
                return;

            ref var playerModel = ref _playerFilter.Get1(0);
            ref var playerInventory = ref _playerFilter.Get2(0);
            var playerInventoryData = playerModel.playerInventoryData;

            foreach (var i in _pickupItemsFilter)
            {
                ref var itemEntity = ref _pickupItemsFilter.GetEntity(i);

                TryAddItem(ref itemEntity, ref playerInventory, playerInventoryData);
            }
        }

        private void TryAddItem(ref EcsEntity itemEntity, ref InventoryComponent playersInventory, PlayerInventoryData playerInventoryData)
        {
            if (itemEntity.Has<MorphineTag>())
                if (playersInventory.currentNumberOfMorphine + 1 <= playerInventoryData.MaximumNumberOfMorphine)
                    playersInventory.currentNumberOfMorphine++;
                else
                    return;

            if (itemEntity.Has<BandageTag>())
                if (playersInventory.currentNumberOfBandages + 1 <= playerInventoryData.MaximumNumberOfBandages)
                    playersInventory.currentNumberOfBandages++;
                else
                    return;
            
            if (itemEntity.Has<InhalatorTag>())
                if (playersInventory.currentNumberOfInhalators + 1 <= playerInventoryData.MaximumNumberOfInhalators)
                    playersInventory.currentNumberOfInhalators++;
                else
                    return;
            
            if (itemEntity.Has<EnergyDrinkTag>())
                if (playersInventory.currentNumberOfEnergyDrinks + 1 <= playerInventoryData.MaximumNumberOfEnergyDrinks)
                    playersInventory.currentNumberOfEnergyDrinks++;
                else
                    return;
            
            PlaceItemInPlayerInventory(ref itemEntity);
        }

        private void PlaceItemInPlayerInventory(ref EcsEntity itemEntity)
        {
            itemEntity.Get<InPlayersInventoryTag>();
            itemEntity.Del<PerformInteractionTag>();
                
            if(itemEntity.Has<TransformModel>())
                itemEntity.Get<TransformModel>().transform.SetGameObjectInactive();
            
            _world.NewEntity().Get<UpdateInventoryAppearanceEvent>();
        }
    }
}
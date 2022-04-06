using Ingame.Player;
using Leopotam.Ecs;
using Support.Extensions;

namespace Ingame.Inventory.Items
{
    public sealed class ItemsInInventoryDisplaySystem : IEcsRunSystem
    {
        private readonly EcsFilter<BackpackModel> _backpackFilter;
        private readonly EcsFilter<PlayerModel, InventoryComponent> _playerFilter;
        private readonly EcsFilter<UpdateInventoryAppearanceEvent> _updateBackpackEventFilter;

        public void Run()
        {
            if(_updateBackpackEventFilter.IsEmpty() || _playerFilter.IsEmpty())
                return;

            ref var updateInventoryEventEntity = ref _updateBackpackEventFilter.GetEntity(0);
            ref var playerInventory = ref _playerFilter.Get2(0);

            updateInventoryEventEntity.Del<UpdateInventoryAppearanceEvent>();
            
            foreach (var i in _backpackFilter)
            {
                ref var backpackModel = ref _backpackFilter.Get1(i);
               
                var morphineInsideBackpack = backpackModel.morphineInsideBackpack;
                var bandagesInsideBackpack = backpackModel.bandagesInsideBackpack;
                var inhalatorsInsideBackpack = backpackModel.inhalatorsInsideBackpack;
                var energyDrinksInsideBackpack = backpackModel.energyDrinksInsideBackpack;

                //todo optimize. Make everything in one loop
                for (var morphineIndex = 0; morphineIndex < morphineInsideBackpack.Length; morphineIndex++)
                {
                    if (morphineIndex < playerInventory.currentNumberOfMorphine)
                        morphineInsideBackpack[morphineIndex].SetGameObjectActive();
                    else
                        morphineInsideBackpack[morphineIndex].SetGameObjectInactive();
                }

                for (var bandageIndex = 0; bandageIndex < bandagesInsideBackpack.Length; bandageIndex++)
                {
                    if(bandageIndex < playerInventory.currentNumberOfBandages)
                        bandagesInsideBackpack[bandageIndex].SetGameObjectActive();
                    else
                        bandagesInsideBackpack[bandageIndex].SetGameObjectInactive();
                }
                
                for (var inhalatorIndex = 0; inhalatorIndex < inhalatorsInsideBackpack.Length; inhalatorIndex++)
                {
                    if(inhalatorIndex < playerInventory.currentNumberOfInhalators)
                        inhalatorsInsideBackpack[inhalatorIndex].SetGameObjectActive();
                    else
                        inhalatorsInsideBackpack[inhalatorIndex].SetGameObjectInactive();
                }
                
                for (var energyDrinkIndex = 0; energyDrinkIndex < energyDrinksInsideBackpack.Length; energyDrinkIndex++)
                {
                    if(energyDrinkIndex < playerInventory.currentNumberOfEnergyDrinks)
                        energyDrinksInsideBackpack[energyDrinkIndex].SetGameObjectActive();
                    else
                        energyDrinksInsideBackpack[energyDrinkIndex].SetGameObjectInactive();
                }
            }
        }
    }
}
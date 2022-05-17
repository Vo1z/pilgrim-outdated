using Ingame.Movement;
using Ingame.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Inventory
{
    public sealed class UpdateBackpackMagazinesAppearanceSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MagazineComponent, TransformModel, MagazineIsInInventoryTag> _magazinesInInventoryFilter;
        private readonly EcsFilter<PlayerModel, InventoryComponent> _playerInventoryFilter;
        private readonly EcsFilter<BackpackModel> _backpackFilter;
        private readonly EcsFilter<UpdateBackpackAppearanceEvent> _updateBackpackAppearanceEventFilter;

        public void Run()
        {
            if(_updateBackpackAppearanceEventFilter.IsEmpty() || _playerInventoryFilter.IsEmpty() || _backpackFilter.IsEmpty())
                return;

            ref var playerModel = ref _playerInventoryFilter.Get1(0);
            ref var playerInventory = ref _playerInventoryFilter.Get2(0);
            ref var backpack = ref _backpackFilter.Get1(0);

            if(playerInventory.currentNumberOfMagazines >= backpack.MaxAmountOfMagazines || playerInventory.currentNumberOfMagazines >= playerModel.playerInventoryData.MaximumNumberOfMagazines)
                return;

            int lastFilledTransformIndex = 0;
            foreach (var i in _magazinesInInventoryFilter)
            {
                ref var magazineTransform = ref _magazinesInInventoryFilter.Get2(i).transform;
                var targetTransform = backpack.magazinesTransform[lastFilledTransformIndex];
                
                magazineTransform.SetParent(targetTransform);
                magazineTransform.localPosition = Vector3.zero;
                magazineTransform.localEulerAngles = Vector3.zero;

                lastFilledTransformIndex++;
            }
        }
    }
}
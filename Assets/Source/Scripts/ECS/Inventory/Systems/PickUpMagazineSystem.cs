using Ingame.Interaction.Common;
using Ingame.Movement;
using Ingame.Player;
using Leopotam.Ecs;
using Support.Extensions;
using UnityEngine;

namespace Ingame.Inventory
{
    public sealed class PickUpMagazineSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<MagazineComponent, TransformModel, PerformInteractionTag>.Exclude<MagazineIsInInventoryTag> _pickUpMagazineFilter;
        private readonly EcsFilter<PlayerModel, InventoryComponent> _playerInventoryFilter;
        private readonly EcsFilter<BackpackModel> _backpackFilter;
        
        public void Run()
        {
            if(_playerInventoryFilter.IsEmpty() || _backpackFilter.IsEmpty())
                return;

            ref var playerInventoryComp = ref _playerInventoryFilter.Get2(0);
            ref var backpackModel = ref _backpackFilter.Get1(0);
            var playerData = _playerInventoryFilter.Get1(0).playerInventoryData;

            int maxAmountOfMagazines = Mathf.Min(playerData.MaximumNumberOfMagazines, backpackModel.MaxAmountOfMagazines);
            
            foreach (var i in _pickUpMagazineFilter)
            {
                ref var magazineEntity = ref _pickUpMagazineFilter.GetEntity(i);
                ref var magazineTransformModel = ref _pickUpMagazineFilter.Get2(i);
        
                magazineEntity.Del<PerformInteractionTag>();
                
                if (playerInventoryComp.currentNumberOfMagazines >= maxAmountOfMagazines)
                    continue;

                int hudLayer = LayerMask.NameToLayer("HUD");
                magazineEntity.Get<MagazineIsInInventoryTag>();
                magazineTransformModel.transform.gameObject.layer = hudLayer;
                magazineTransformModel.transform.gameObject.SetLayerToAllChildren(hudLayer);
                _world.NewEntity().Get<UpdateBackpackAppearanceEvent>();
            }
        }
    }
}
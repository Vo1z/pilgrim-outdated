using Ingame.Interaction.Common;
using Ingame.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Inventory
{
    public sealed class InteractWithInventoryMagazineSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<MagazineComponent, MagazineIsInInventoryTag, PerformInteractionTag> _refillMagazineFilter;
        private readonly EcsFilter<PlayerModel, InventoryComponent> _playerInventoryFilter;

        public void Run()
        {
            if(_playerInventoryFilter.IsEmpty())
                return;

            ref var playerInventory = ref _playerInventoryFilter.Get2(0);

            foreach (var i in _refillMagazineFilter)
            {
                ref var magazineEntity = ref _refillMagazineFilter.GetEntity(i);
                ref var magazineComponent = ref _refillMagazineFilter.Get1(i);
                var magazineData = magazineComponent.magazineData;

                int bulletsOfSameTypeAvailableInInventory = playerInventory.ammo[magazineData.AmmoType];
                bool isAtLeastOneBulletPresentInInventory = bulletsOfSameTypeAvailableInInventory > 0;
                bool isThereFreeSpaceForBulletsInMagazine = magazineComponent.currentAmountOfAmmoInMagazine < magazineData.AmountOfAmmoInMagazine;
                
                magazineEntity.Del<PerformInteractionTag>();
                
                if (!isAtLeastOneBulletPresentInInventory || !isThereFreeSpaceForBulletsInMagazine)
                    continue;
                
                int availableAmountOfAmmoToRefillInMagazine = magazineData.AmountOfAmmoInMagazine - magazineComponent.currentAmountOfAmmoInMagazine;
                int bulletsToRefill = Mathf.Min(bulletsOfSameTypeAvailableInInventory, availableAmountOfAmmoToRefillInMagazine);

                magazineComponent.currentAmountOfAmmoInMagazine += bulletsToRefill;
                playerInventory.ammo[magazineData.AmmoType] = bulletsOfSameTypeAvailableInInventory - bulletsToRefill;

                _world.NewEntity().Get<UpdateMagazineAppearanceEvent>();
            }
        }
    }
}
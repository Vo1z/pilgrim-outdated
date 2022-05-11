using Ingame.Interaction.Common;
using Ingame.Player;
using Leopotam.Ecs;

namespace Ingame.Inventory
{
    public sealed class InteractWithInventoryMagazineSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MagazineComponent, MagazineIsInInventoryTag, PerformInteractionTag>.Exclude<RefillMagazineTag> _refillMagazineFilter;
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

                bool isAtLeastOneBulletPresentInInventory = playerInventory.ammo[magazineData.AmmoType] > 0;
                bool isThereFreeSpaceForBulletsInMagazine = magazineComponent.currentAmountOfAmmoInMagazine < magazineData.AmountOfAmmoInMagazine;
                
                if (!isAtLeastOneBulletPresentInInventory || !isThereFreeSpaceForBulletsInMagazine)
                {
                    magazineEntity.Del<PerformInteractionTag>();
                    continue;
                }
            }
        }
    }
}
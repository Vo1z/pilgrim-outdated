using Ingame.Interaction.Common;
using Ingame.Movement;
using Ingame.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Inventory
{
    public sealed class PickUpAmmoSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<AmmoComponent, TransformModel, PerformInteractionTag> _ammoFilter;
        private readonly EcsFilter<PlayerModel, InventoryComponent> _playerInventoryFilter;

        public void Run()
        {
            if(_playerInventoryFilter.IsEmpty())
                return;

            ref var playerModel = ref _playerInventoryFilter.Get1(0);
            ref var playerInventory = ref _playerInventoryFilter.Get2(0);

            foreach (var i in _ammoFilter)
            {
                ref var ammoEntity = ref _ammoFilter.GetEntity(i);
                ref var ammoComp = ref _ammoFilter.Get1(i);
                ref var ammoTransformModel = ref _ammoFilter.Get2(i);
                var ammoToAdd = Mathf.Max(0, ammoComp.amountOfBullets);
                var maxAmountOfAmmoInInventory = playerModel.playerInventoryData.GetMaximumAmountOfAmmo(ammoComp.ammoType);

                if (ammoToAdd < 1)
                {
                    ammoEntity.Del<PerformInteractionTag>();
                    continue;
                }

                playerInventory.ammo[ammoComp.ammoType] = 
                    Mathf.Min(playerInventory.ammo[ammoComp.ammoType] + ammoToAdd, maxAmountOfAmmoInInventory);

                Object.Destroy(ammoTransformModel.transform.gameObject);
                ammoEntity.Destroy();

                _world.NewEntity().Get<UpdateBackpackAppearanceEvent>();
            }
        }
    }
}
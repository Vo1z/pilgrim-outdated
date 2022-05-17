using Ingame.Inventory;
using Ingame.Player;
using Leopotam.Ecs;

namespace Ingame.UI
{
    public sealed class DisplayAmountOfAmmoSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TmpTextModel, UiAmmoCountTextComponent> _ammoBoxTextFilter;
        private readonly EcsFilter<PlayerModel, InventoryComponent> _playerInventoryFilter;
        private readonly EcsFilter<UpdateBackpackAppearanceEvent> _updateBackpackAppearanceEventFilter;

        public void Run()
        {
            if(_updateBackpackAppearanceEventFilter.IsEmpty() || _playerInventoryFilter.IsEmpty())
                return;

            ref var playerInventoryData = ref _playerInventoryFilter.Get1(0).playerInventoryData;
            ref var playerInventory = ref _playerInventoryFilter.Get2(0);

            foreach (var i in _ammoBoxTextFilter)
            {
                ref var ammoBoxTmpText = ref _ammoBoxTextFilter.Get1(i).tmpText;
                ref var ammoType = ref _ammoBoxTextFilter.Get2(i).ammoType;

                var maximumAmountOfAmmo = playerInventoryData.GetMaximumAmountOfAmmo(ammoType);
                ammoBoxTmpText.SetText($"{playerInventory.ammo[ammoType]}/{maximumAmountOfAmmo}");
            }
        }
    }
}
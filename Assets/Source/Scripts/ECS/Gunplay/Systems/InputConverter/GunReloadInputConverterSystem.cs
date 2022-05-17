using Ingame.Data.Gunplay;
using Ingame.Hud;
using Ingame.Input;
using Ingame.Inventory;
using Leopotam.Ecs;

namespace Ingame.Gunplay
{
    public sealed class GunReloadInputConverterSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        
        private readonly EcsFilter<GunModel, InHandsTag, HudIsVisibleTag> _gunsFilter;
        private readonly EcsFilter<ReloadInputEvent> _reloadInputEvent;
        private readonly EcsFilter<MagazineComponent, MagazineIsInInventoryTag> _inventoryMagazineFilter;

        public void Run()
        {
            if(_reloadInputEvent.IsEmpty() || _inventoryMagazineFilter.IsEmpty())
                return;

            foreach (var i in _gunsFilter)
            {
                ref var gunEntity = ref _gunsFilter.GetEntity(i);
                var gunAmmoType = _gunsFilter.Get1(i).gunData.AmmoType;

                bool reloadingCanBePerformed = !gunEntity.Has<AwaitingReloadTag>();

                if (reloadingCanBePerformed)
                {
                    gunEntity.Get<AwaitingReloadTag>();
                    _world.NewEntity().Get<HudReloadAnimationTriggerEvent>();
                }
            }
        }

        private bool IsThereAvailableMagazine(AmmoType ammoType)
        {
            foreach (var i in _inventoryMagazineFilter)
            {
                ref var magazineComp = ref _inventoryMagazineFilter.Get1(i);
                
                if(magazineComp.magazineData.AmmoType != ammoType)
                    continue;

                if (magazineComp.currentAmountOfAmmoInMagazine > 0)
                    return true;
            }

            return false;
        }
    }
}
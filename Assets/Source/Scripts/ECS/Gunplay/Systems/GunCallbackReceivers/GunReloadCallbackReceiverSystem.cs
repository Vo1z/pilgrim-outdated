using Ingame.Hud;
using Ingame.Inventory;
using Ingame.Movement;
using Leopotam.Ecs;

namespace Ingame.Gunplay
{
    public sealed class GunReloadCallbackReceiverSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GunModel, InHandsTag, AwaitingReloadTag> _gunsFilter;
        private readonly EcsFilter<ReloadPerformedCallbackEvent> _reloadCallbackFilter;
        private readonly EcsFilter<MagazineComponent, TransformModel, MagazineIsInInventoryTag, MagazineNextToUseInReloadTag> _inventoryMagazineFilter;
        
        public void Run()
        {
            if (_reloadCallbackFilter.IsEmpty() || _inventoryMagazineFilter.IsEmpty())
                return;

            foreach (var i in _gunsFilter)
            {
                ref var gunEntity = ref _gunsFilter.GetEntity(i);
                var gunAmmoType = _gunsFilter.Get1(i).gunData.AmmoType;

                gunEntity.Del<AwaitingReloadTag>();
                gunEntity.Get<GunMagazineComponent>().amountOfBullets = 30;
            }
        }
    }
}
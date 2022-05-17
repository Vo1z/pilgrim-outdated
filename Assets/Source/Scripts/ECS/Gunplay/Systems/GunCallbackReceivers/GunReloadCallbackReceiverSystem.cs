using Ingame.Hud;
using Leopotam.Ecs;

namespace Ingame.Gunplay
{
    public sealed class GunReloadCallbackReceiverSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GunModel, InHandsTag, AwaitingReloadTag> _gunsFilter;
        private readonly EcsFilter<ReloadPerformedCallbackEvent> _reloadCallbackFilter;

        public void Run()
        {
            if (_reloadCallbackFilter.IsEmpty())
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
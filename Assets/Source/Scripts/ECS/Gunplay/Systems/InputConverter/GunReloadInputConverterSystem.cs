using Ingame.Hud;
using Ingame.Input;
using Ingame.Inventory;
using Ingame.Player;
using Leopotam.Ecs;

namespace Ingame.Gunplay
{
    public sealed class GunReloadInputConverterSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        
        private readonly EcsFilter<GunModel, InHandsTag, HudIsVisibleTag> _gunsFilter;
        private readonly EcsFilter<ReloadInputEvent> _reloadInputEvent;
        private readonly EcsFilter<PlayerModel, InventoryComponent> _playerInventoryFilter;

        public void Run()
        {
            if(_reloadInputEvent.IsEmpty() || _playerInventoryFilter.IsEmpty())
                return;

            ref var playerInventory = ref _playerInventoryFilter.Get2(0);

            foreach (var i in _gunsFilter)
            {
                ref var gunEntity = ref _gunsFilter.GetEntity(i);
                ref var gunData = ref _gunsFilter.Get1(i).gunData;
                
                bool reloadingCanBePerformed = !gunEntity.Has<AwaitingReloadTag>();

                if (reloadingCanBePerformed)
                {
                    gunEntity.Get<AwaitingReloadTag>();
                    _world.NewEntity().Get<HudReloadAnimationTriggerEvent>();
                }
            }
        }
    }
}
using Ingame.Hud;
using Ingame.Input;
using Leopotam.Ecs;

namespace Ingame.Gunplay
{
    public sealed class GunReloadInputConverterSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        
        private readonly EcsFilter<GunModel, InHandsTag, HudIsVisibleTag> _gunsFilter;
        private readonly EcsFilter<ReloadInputEvent> _reloadInputEvent;

        public void Run()
        {
            if(_reloadInputEvent.IsEmpty())
                return;

            foreach (var i in _gunsFilter)
            {
                ref var gunEntity = ref _gunsFilter.GetEntity(i);
                
                //Todo extend condition with inventory logic
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
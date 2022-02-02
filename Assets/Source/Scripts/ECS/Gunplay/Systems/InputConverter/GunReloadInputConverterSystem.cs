using Leopotam.Ecs;

namespace Ingame
{
    public sealed class GunReloadInputConverterSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        
        private readonly EcsFilter<GunModel, InHandsTag> _gunsFilter;
        private readonly EcsFilter<ReloadInputEvent> _reloadInputEvent;

        public void Run()
        {
            foreach (var i in _gunsFilter)
            {
                ref var gunEntity = ref _gunsFilter.GetEntity(i);

                if (!_reloadInputEvent.IsEmpty())
                {
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
}
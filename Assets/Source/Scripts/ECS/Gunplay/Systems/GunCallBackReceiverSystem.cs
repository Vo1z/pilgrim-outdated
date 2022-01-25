using Leopotam.Ecs;

namespace Ingame
{
    public sealed class GunCallBackReceiverSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GunModel, InHandsTag> _gunsFilter;

        private readonly EcsFilter<ReloadPerformedCallbackEvent> _reloadCallbackFilter;
        private readonly EcsFilter<ShutterDistortionPerformedCallbackEvent> _shutterDistortionCallbackFilter;

        public void Run()
        {
            foreach (var i in _gunsFilter)
            {
                ref var gunEntity = ref _gunsFilter.GetEntity(i);

                if (!_reloadCallbackFilter.IsEmpty())
                    gunEntity.Del<AwaitingReloadTag>();
                
                if (!_shutterDistortionCallbackFilter.IsEmpty())
                    gunEntity.Del<AwaitingShutterDistortionTag>();
            }
        }
    }
}
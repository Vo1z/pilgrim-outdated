using Ingame.Gunplay;
using Leopotam.Ecs;

namespace Ingame.Utils
{
    public sealed class ExternalEventsRemoverSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ReloadPerformedCallbackEvent> _reloadCallbackEventFilter;
        private readonly EcsFilter<ShutterDistortionPerformedCallbackEvent> _shutterDistortionCallbackEventFilter;

        public void Run()
        {
            foreach (var i in _reloadCallbackEventFilter)
            {
                ref var eventEntity = ref _reloadCallbackEventFilter.GetEntity(i);
                eventEntity.Del<ReloadPerformedCallbackEvent>();
            }
            
            foreach (var i in _shutterDistortionCallbackEventFilter)
            {
                ref var eventEntity = ref _shutterDistortionCallbackEventFilter.GetEntity(i);
                eventEntity.Del<ShutterDistortionPerformedCallbackEvent>();
            }
        }
    }
}
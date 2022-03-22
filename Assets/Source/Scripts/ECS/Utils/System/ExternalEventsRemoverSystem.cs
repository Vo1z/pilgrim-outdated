using Ingame.Gunplay;
using LeoEcsPhysics;
using Leopotam.Ecs;

namespace Ingame.Utils
{
    public sealed class ExternalEventsRemoverSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ReloadPerformedCallbackEvent> _reloadCallbackEventFilter;
        private readonly EcsFilter<ShutterDistortionPerformedCallbackEvent> _shutterDistortionCallbackEventFilter;
        private readonly EcsFilter<OnTriggerEnterEvent> _filterEnter;
        private readonly EcsFilter<OnTriggerExitEvent> _filterExit;
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
            
            foreach (var i in _filterEnter)
            {
                ref var eventEntity = ref _filterEnter.GetEntity(i);
                eventEntity.Del<OnTriggerEnterEvent>();
            }

            foreach (var i in _filterExit)
            {
                ref var eventEntity = ref _filterExit.GetEntity(i);
                eventEntity.Del<OnTriggerExitEvent>();
            }

        }
    }
}
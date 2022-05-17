using Ingame.Gunplay;
using Ingame.Inventory;
using LeoEcsPhysics;
using Leopotam.Ecs;

namespace Ingame.Utils
{
    public sealed class ExternalEventsRemoverSystem : IEcsRunSystem
    {
        //Gun-play
        private readonly EcsFilter<ReloadPerformedCallbackEvent> _reloadCallbackEventFilter;
        private readonly EcsFilter<ShutterDistortionPerformedCallbackEvent> _shutterDistortionCallbackEventFilter;
        //Inventory
        private readonly EcsFilter<UpdateBackpackAppearanceEvent> _updateInventoryEventFilter;
        private readonly EcsFilter<UpdateMagazineAppearanceEvent> _updateMagazineAppearanceEventFilter;
        //Physics
        private readonly EcsFilter<OnTriggerEnterEvent> _filterEnter;
        private readonly EcsFilter<OnTriggerStayEvent> _onTriggerStayEventFilter;
        private readonly EcsFilter<OnTriggerExitEvent> _filterExit;
        private readonly EcsFilter<OnCollisionEnterEvent> _collisionEnterEventFilter;
        private readonly EcsFilter<OnCollisionExitEvent> _collisionExitEventFilter;

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

            foreach (var i in _updateInventoryEventFilter)
            {
                ref var eventEntity = ref _updateInventoryEventFilter.GetEntity(i); 
                eventEntity.Del<UpdateBackpackAppearanceEvent>();
            }

            foreach (var i in _updateMagazineAppearanceEventFilter)
            {
                ref var eventEntity = ref _updateInventoryEventFilter.GetEntity(i); 
                
                if(eventEntity.IsAlive())
                    eventEntity.Del<UpdateMagazineAppearanceEvent>();
            }

            foreach (var i in _filterEnter)
            {
                ref var eventEntity = ref _filterEnter.GetEntity(i);
                eventEntity.Del<OnTriggerEnterEvent>();
            }
            
            foreach (var i in _onTriggerStayEventFilter)
            {
                ref var eventEntity = ref _onTriggerStayEventFilter.GetEntity(i);
                eventEntity.Del<OnTriggerStayEvent>();
            }

            foreach (var i in _filterExit)
            {
                ref var eventEntity = ref _filterExit.GetEntity(i);
                eventEntity.Del<OnTriggerExitEvent>();
            }

            foreach (var i in _collisionEnterEventFilter)
            {
                ref var eventEntity = ref _collisionEnterEventFilter.GetEntity(i);
                eventEntity.Del<OnCollisionEnterEvent>();
            }
            
            foreach (var i in _collisionExitEventFilter)
            {
                ref var eventEntity = ref _collisionExitEventFilter.GetEntity(i);
                eventEntity.Del<OnCollisionExitEvent>();
            }
        }
    }
}
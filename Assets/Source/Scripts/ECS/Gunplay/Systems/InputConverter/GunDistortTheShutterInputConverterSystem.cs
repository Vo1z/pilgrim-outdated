using Ingame.Hud;
using Ingame.Input;
using Leopotam.Ecs;

namespace Ingame.Gunplay
{
    public sealed class GunDistortTheShutterInputConverterSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        
        private readonly EcsFilter<GunModel, ShootTimerComponent, InHandsTag, HudIsVisibleTag> _gunsFilter;
        private readonly EcsFilter<DistortTheShutterInputEvent> _distortTheShutterInputEvent;

        public void Run()
        {
            if(_distortTheShutterInputEvent.IsEmpty())
                return;

            foreach (var i in _gunsFilter)
            {
                ref var gunEntity = ref _gunsFilter.GetEntity(i);

                bool bulletCanBeAddedToTheShutter = !gunEntity.Has<AwaitingShutterDistortionTag>();

                if (bulletCanBeAddedToTheShutter)
                {
                    gunEntity.Get<AwaitingShutterDistortionTag>();
                    _world.NewEntity().Get<HudDistortTheShutterAnimationTriggerEvent>();
                }
            }
        }
    }
}
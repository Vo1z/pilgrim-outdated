using Leopotam.Ecs;

namespace Ingame
{
    public sealed class GunDistortTheShutterInputConverterSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<GunModel, ShootTimerComponent, InHandsTag> _gunsFilter;
        private readonly EcsFilter<DistortTheShutterInputEvent> _distortTheShutterInputEvent;

        public void Run()
        {
            foreach (var i in _gunsFilter)
            {
                ref var gunEntity = ref _gunsFilter.GetEntity(i);
                
                if (!_distortTheShutterInputEvent.IsEmpty())
                {
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
}
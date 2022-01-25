using Leopotam.Ecs;

namespace Ingame
{
    public sealed class GunInHandsInputConverterSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        
        private readonly EcsFilter<GunModel, ShootTimerComponent, InHandsTag> _gunsFilter;
        
        private readonly EcsFilter<ShootInputEvent> _shootInputEvent;
        private readonly EcsFilter<ReloadInputEvent> _reloadInputEvent;
        private readonly EcsFilter<DistortTheShutterInputEvent> _distortTheShutterInputEvent;

        public void Run()
        {
            foreach (var i in _gunsFilter)
            {
                ref var gunEntity = ref _gunsFilter.GetEntity(i);
                ref var gunModel = ref _gunsFilter.Get1(i);
                ref var shootTimerComponent = ref _gunsFilter.Get2(i);
                var gunData = gunModel.gunData;

                if (!_shootInputEvent.IsEmpty())
                {
                    bool shotCanBePerformed = shootTimerComponent.timePassedFromLastShot > gunData.PauseBetweenShots;
                    // isShotCanBePerformed = isShotCanBePerformed && gunEntity.Has<BulletIsInShutterTag>();

                    if (shotCanBePerformed)
                    {
                        shootTimerComponent.timePassedFromLastShot = 0;
                        gunEntity.Get<AwaitingShotTag>();
                    }
                }

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
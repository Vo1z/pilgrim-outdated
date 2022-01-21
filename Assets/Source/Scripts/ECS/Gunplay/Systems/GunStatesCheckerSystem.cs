using Leopotam.Ecs;

namespace Ingame
{
    public sealed class GunStatesCheckerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GunModel, ShootTimerComponent> _gunsFilter;
        private readonly EcsFilter<ShootInputEvent> _shootInputEvent;

        public void Run()
        {
            if(_shootInputEvent.IsEmpty())
                return;

            foreach (var i in _gunsFilter)
            {
                ref var gunEntity = ref _gunsFilter.GetEntity(i);
                ref var gunModel = ref _gunsFilter.Get1(i);
                ref var shootTimerComponent = ref _gunsFilter.Get2(i);
                var gunData = gunModel.gunData;
                
                if(shootTimerComponent.timePassedFromLastShot < gunData.PauseBetweenShots)
                    continue;

                shootTimerComponent.timePassedFromLastShot = 0;
                
                gunEntity.Get<ShootComponent>();
            }
        }
    }
}
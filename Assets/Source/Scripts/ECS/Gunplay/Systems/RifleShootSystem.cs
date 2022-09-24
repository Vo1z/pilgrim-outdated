using Ingame.Hud;
using Ingame.Input;
using Ingame.Utils;
using Leopotam.Ecs;

namespace Ingame.Gunplay
{
    public sealed class RifleShootSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FirearmComponent, RifleComponent, TimerComponent, InInventryTag, HudIsInHandsTag> _firearmShootFilter;
        private readonly EcsFilter<ShootInputEvent> _shootInputFilter;

        public void Run()
        {
            if(_shootInputFilter.IsEmpty())
                return;

            foreach (var i in _firearmShootFilter)
            {
                ref var rifleEntity = ref _firearmShootFilter.GetEntity(i);
                ref var rifleComponent = ref _firearmShootFilter.Get2(i);
                ref var timerComponent = ref _firearmShootFilter.Get3(i);

                if (timerComponent.timePassed < rifleComponent.rifleConfig.PauseBetweenShots)
                    return;

                rifleEntity.Get<AwaitingShotTag>();
                timerComponent.timePassed = 0;
            }
        }
    }
}
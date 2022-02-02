using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class TimeSystem : IEcsRunSystem
    {
        private EcsFilter<TimerComponent> _timerFilter;
        private EcsFilter<ShootTimerComponent> _shootTimerFilter;
        

        public void Run()
        {
            foreach (var i in _timerFilter)
            {
                ref var timerComponent = ref _timerFilter.Get1(i);

                timerComponent.timePassed += Time.deltaTime;
            }

            foreach (var i in _shootTimerFilter)
            {
                ref var shotTimer = ref _shootTimerFilter.Get1(i);
                
                shotTimer.timePassedFromLastShot += Time.deltaTime;
            }
        }
    }
}
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy.System
{
    public sealed class IdleSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyMovementComponent, WaitOnPointCallbackRequest> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var movement = ref _filter.Get1(i);
                ref var time = ref _filter.Get2(i);
                movement.NavMeshAgent.isStopped = true;
                time.TimeLeft += Time.deltaTime;
                
                if (movement.EnemyMovementData.DelayAfterAchievingWaypoint <= time.TimeLeft)
                {
                    entity.Del<WaitOnPointCallbackRequest>();
                }
            }
        }
    }
}
using Leopotam.Ecs;

namespace Ingame.Gunplay
{
    public sealed class GunInitSystem : IEcsInitSystem
    {
        private EcsFilter<GunModel> _gunFilter;
        
        public void Init()
        {
            foreach (var i in _gunFilter)
            {
                ref var gunEntity = ref _gunFilter.GetEntity(i);

                gunEntity.Get<ShootTimerComponent>();
            }
        }
    }
}
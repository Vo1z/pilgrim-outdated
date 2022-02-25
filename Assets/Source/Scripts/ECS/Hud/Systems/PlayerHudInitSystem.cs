using Leopotam.Ecs;

namespace Ingame.Hud
{
    public sealed class PlayerHudInitSystem : IEcsInitSystem
    {
        private readonly EcsFilter<HudModel> _hudFilter;

        public void Init()
        {
            foreach (var i in _hudFilter)
            {
                ref var hudEntity = ref _hudFilter.GetEntity(i);
            }
        }
    }
}
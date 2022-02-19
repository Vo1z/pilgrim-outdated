using Leopotam.Ecs;

namespace Ingame.Hud
{
    public sealed class HudItemModelInitializeSystem : IEcsInitSystem
    {
        private readonly EcsFilter<HudItemModel> _hudItemFilter;

        public void Init()
        {
            foreach (var i in _hudItemFilter)
            {
                ref var hudItemEntity = ref _hudItemFilter.GetEntity(i);

                hudItemEntity.Get<HudIsVisibleTag>();
            }
        }
    }
}
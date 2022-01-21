using Leopotam.Ecs;

namespace Ingame
{
    public sealed class HudInputToStatesConverterSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HudItemModel, InHandsTag> _itemModelFilter;
        private readonly EcsFilter<AimInputEvent> _aimEventFilter;

        public void Run()
        {
            foreach (var i in _itemModelFilter)
            {
                ref var hudItemModel = ref _itemModelFilter.Get1(i);

                if (!_aimEventFilter.IsEmpty())
                    hudItemModel.isAiming = !hudItemModel.isAiming;
            }
        }
    }
}
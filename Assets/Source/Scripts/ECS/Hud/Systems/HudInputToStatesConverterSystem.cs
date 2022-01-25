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
                ref var inHandsItemEntity = ref _itemModelFilter.GetEntity(i);

                if (!_aimEventFilter.IsEmpty())
                {
                    if(inHandsItemEntity.Has<HudIsAimingTag>())
                        inHandsItemEntity.Del<HudIsAimingTag>();
                    else
                        inHandsItemEntity.Get<HudIsAimingTag>();
                }
            }
        }
    }
}
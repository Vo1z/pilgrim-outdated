using Ingame.Input;
using Leopotam.Ecs;

namespace Ingame.Hud
{
    public sealed class HudInputToStatesConverterSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HudItemModel, InHandsTag> _itemModelFilter;
        private readonly EcsFilter<AimInputEvent> _aimEventFilter;
        private readonly EcsFilter<OpenInventoryInputEvent> _openInventoryInputEvent;
        
        public void Run()
        {
            foreach (var i in _itemModelFilter)
            {
                ref var inHandsItemEntity = ref _itemModelFilter.GetEntity(i);
                var itemData = _itemModelFilter.Get1(i).itemData;

                if (!_aimEventFilter.IsEmpty() && itemData.CanBeUsedAsAim)
                {
                    if (inHandsItemEntity.Has<HudIsAimingTag>())
                        inHandsItemEntity.Del<HudIsAimingTag>();
                    else
                        inHandsItemEntity.Get<HudIsAimingTag>();
                }

                if (!_openInventoryInputEvent.IsEmpty())
                {
                    if (inHandsItemEntity.Has<HudIsVisibleTag>())
                        inHandsItemEntity.Del<HudIsVisibleTag>();
                    else
                        inHandsItemEntity.Get<HudIsVisibleTag>();
                }
            }
        }
    }
}
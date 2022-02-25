using Ingame.Input;
using Leopotam.Ecs;

namespace Ingame.Hud
{
    public sealed class HudInputToStatesConverterSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HudItemModel, InHandsTag> _itemModelFilter;
        private readonly EcsFilter<AimInputEvent> _aimEventFilter;


        public void Run()
        {
            if(_aimEventFilter.IsEmpty())
                return;

            foreach (var i in _itemModelFilter)
            {
                ref var inHandsItemEntity = ref _itemModelFilter.GetEntity(i);
                
                if (inHandsItemEntity.Has<HudIsAimingTag>())
                    inHandsItemEntity.Del<HudIsAimingTag>();
                else
                    inHandsItemEntity.Get<HudIsAimingTag>();
            }
        }
    }
}